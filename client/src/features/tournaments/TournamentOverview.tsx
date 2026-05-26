import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'
import { toast } from 'sonner'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { ErrorBanner } from '@/components/ui/error-banner'
import { Skeleton } from '@/components/ui/skeleton'
import { formatCurrency, formatDate } from '@/lib/format'
import { useTournament, useUpdateTournament } from '@/api/tournaments'
import { useParams } from 'react-router-dom'

const isoDateString = z.string().regex(/^\d{4}-\d{2}-\d{2}$/, 'Must be YYYY-MM-DD')

const schema = z
  .object({
    name: z.string().min(1, 'Name is required'),
    start: isoDateString,
    end: isoDateString,
    bowlingCenter: z.string().min(1, 'Bowling center is required'),
    entryFee: z.coerce.number().positive('Must be positive'),
    games: z.coerce.number().int().min(1, 'Must be at least 1'),
    finalsRatio: z.coerce.number().gt(1, 'Must be greater than 1'),
    cashRatio: z.coerce.number().gt(1, 'Must be greater than 1'),
    superSweeperCashRatio: z.coerce.number().gt(1, 'Must be greater than 1'),
    completed: z.boolean(),
  })
  .refine((d) => d.start <= d.end, {
    message: 'Start must be on or before end',
    path: ['start'],
  })

type FormValues = z.infer<typeof schema>

export function TournamentOverview() {
  const { id } = useParams<{ id: string }>()
  const [editing, setEditing] = useState(false)
  const { data, isLoading, isError, error, refetch } = useTournament(id!)
  const updateMutation = useUpdateTournament()

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<FormValues>({ resolver: zodResolver(schema) })

  function enterEdit() {
    if (!data) return
    reset({
      name: data.name,
      start: data.start,
      end: data.end,
      bowlingCenter: data.bowlingCenter,
      entryFee: data.entryFee,
      games: data.games,
      finalsRatio: data.finalsRatio,
      cashRatio: data.cashRatio,
      superSweeperCashRatio: data.superSweeperCashRatio,
      completed: data.completed,
    })
    setEditing(true)
  }

  function cancelEdit() {
    setEditing(false)
  }

  async function onSubmit(values: FormValues) {
    try {
      await updateMutation.mutateAsync({ id: id!, data: values })
      toast.success('Tournament saved')
      setEditing(false)
    } catch {
      toast.error('Failed to save tournament')
    }
  }

  if (isLoading) {
    return (
      <div className="space-y-3">
        <Skeleton className="h-5 w-48" />
        <Skeleton className="h-5 w-64" />
        <Skeleton className="h-5 w-40" />
      </div>
    )
  }

  if (isError) {
    return (
      <ErrorBanner
        message={error?.message ?? 'Failed to load tournament'}
        onRetry={() => refetch()}
      />
    )
  }

  if (!data) return null

  if (editing) {
    return (
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 max-w-lg" noValidate>
        <h2 className="font-heading text-lg font-bold text-text-primary">Edit Tournament</h2>

        <div className="space-y-1">
          <Label htmlFor="name">Name</Label>
          <Input id="name" {...register('name')} />
          {errors.name && <p className="text-xs text-red-600">{errors.name.message}</p>}
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div className="space-y-1">
            <Label htmlFor="start">Start Date</Label>
            <Input id="start" type="date" {...register('start')} />
            {errors.start && <p className="text-xs text-red-600">{errors.start.message}</p>}
          </div>
          <div className="space-y-1">
            <Label htmlFor="end">End Date</Label>
            <Input id="end" type="date" {...register('end')} />
            {errors.end && <p className="text-xs text-red-600">{errors.end.message}</p>}
          </div>
        </div>

        <div className="space-y-1">
          <Label htmlFor="bowlingCenter">Bowling Center</Label>
          <Input id="bowlingCenter" {...register('bowlingCenter')} />
          {errors.bowlingCenter && (
            <p className="text-xs text-red-600">{errors.bowlingCenter.message}</p>
          )}
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div className="space-y-1">
            <Label htmlFor="entryFee">Entry Fee ($)</Label>
            <Input id="entryFee" type="number" step="0.01" {...register('entryFee')} />
            {errors.entryFee && <p className="text-xs text-red-600">{errors.entryFee.message}</p>}
          </div>
          <div className="space-y-1">
            <Label htmlFor="games">Games</Label>
            <Input id="games" type="number" {...register('games')} />
            {errors.games && <p className="text-xs text-red-600">{errors.games.message}</p>}
          </div>
        </div>

        <div className="grid grid-cols-3 gap-4">
          <div className="space-y-1">
            <Label htmlFor="finalsRatio">Finals Ratio</Label>
            <Input id="finalsRatio" type="number" step="0.1" {...register('finalsRatio')} />
            {errors.finalsRatio && (
              <p className="text-xs text-red-600">{errors.finalsRatio.message}</p>
            )}
          </div>
          <div className="space-y-1">
            <Label htmlFor="cashRatio">Cash Ratio</Label>
            <Input id="cashRatio" type="number" step="0.1" {...register('cashRatio')} />
            {errors.cashRatio && (
              <p className="text-xs text-red-600">{errors.cashRatio.message}</p>
            )}
          </div>
          <div className="space-y-1">
            <Label htmlFor="superSweeperCashRatio">SS Cash Ratio</Label>
            <Input
              id="superSweeperCashRatio"
              type="number"
              step="0.1"
              {...register('superSweeperCashRatio')}
            />
            {errors.superSweeperCashRatio && (
              <p className="text-xs text-red-600">{errors.superSweeperCashRatio.message}</p>
            )}
          </div>
        </div>

        <div className="flex items-center gap-2">
          <input
            id="completed"
            type="checkbox"
            className="h-4 w-4 rounded border-border-form accent-interactive"
            {...register('completed')}
          />
          <Label htmlFor="completed">Completed</Label>
        </div>

        <div className="flex gap-3">
          <Button type="submit" disabled={updateMutation.isPending}>
            {updateMutation.isPending ? 'Saving…' : 'Save'}
          </Button>
          <Button type="button" variant="outline" onClick={cancelEdit}>
            Cancel
          </Button>
        </div>
      </form>
    )
  }

  return (
    <div className="space-y-4 max-w-lg">
      <div className="flex items-center justify-between">
        <h2 className="font-heading text-lg font-bold text-text-primary">Overview</h2>
        <Button variant="outline" size="sm" onClick={enterEdit}>
          Edit
        </Button>
      </div>

      <dl className="grid grid-cols-2 gap-x-6 gap-y-3 text-sm">
        <div>
          <dt className="font-semibold text-text-muted">Name</dt>
          <dd className="text-text-primary">{data.name}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Bowling Center</dt>
          <dd className="text-text-primary">{data.bowlingCenter}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Start</dt>
          <dd className="text-text-primary">{formatDate(data.start)}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">End</dt>
          <dd className="text-text-primary">{formatDate(data.end)}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Entry Fee</dt>
          <dd className="text-text-primary">{formatCurrency(data.entryFee)}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Games</dt>
          <dd className="text-text-primary">{data.games}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Finals Ratio</dt>
          <dd className="text-text-primary">{data.finalsRatio}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Cash Ratio</dt>
          <dd className="text-text-primary">{data.cashRatio}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Super Sweeper Cash Ratio</dt>
          <dd className="text-text-primary">{data.superSweeperCashRatio}</dd>
        </div>
        <div>
          <dt className="font-semibold text-text-muted">Status</dt>
          <dd className="text-text-primary">{data.completed ? 'Completed' : 'Active'}</dd>
        </div>
      </dl>
    </div>
  )
}
