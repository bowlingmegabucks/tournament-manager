import { Outlet, useParams } from 'react-router-dom'
import { ErrorBanner } from '@/components/ui/error-banner'
import { Skeleton } from '@/components/ui/skeleton'
import { TournamentDetailHeader } from './TournamentDetailHeader'
import { TournamentDetailSidebar } from './TournamentDetailSidebar'
import { useTournament } from '@/api/tournaments'

export function TournamentDetailLayout() {
  const { id } = useParams<{ id: string }>()
  const { data, isLoading, isError, error, refetch } = useTournament(id!)

  if (isLoading) {
    return (
      <div className="space-y-4">
        <Skeleton className="h-8 w-64" />
        <Skeleton className="h-4 w-48" />
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

  return (
    <div>
      <TournamentDetailHeader tournament={data} />
      <div className="flex">
        <TournamentDetailSidebar tournamentId={id!} />
        <main className="flex-1 min-w-0 p-5">
          <Outlet />
        </main>
      </div>
    </div>
  )
}
