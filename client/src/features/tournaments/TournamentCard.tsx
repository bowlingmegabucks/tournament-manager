import { useNavigate } from 'react-router-dom'
import { Badge } from '@/components/ui/badge'
import { formatDateRange } from '@/lib/format'
import type { TournamentSummary } from '@/api/tournaments'

interface TournamentCardProps {
  readonly tournament: TournamentSummary
}

export function TournamentCard({ tournament }: TournamentCardProps) {
  const navigate = useNavigate()

  return (
    <div
      role="button"
      tabIndex={0}
      className="cursor-pointer rounded border border-border bg-surface-page p-4 shadow-sm transition-shadow hover:shadow-md focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-interactive"
      onClick={() => navigate(`/tournaments/${tournament.id}`)}
      onKeyDown={(e) => {
        if (e.key === 'Enter' || e.key === ' ') navigate(`/tournaments/${tournament.id}`)
      }}
    >
      <div className="flex items-start justify-between gap-2">
        <h3 className="font-heading text-base font-bold text-text-primary">{tournament.name}</h3>
        <Badge variant={tournament.completed ? 'neutral' : 'success'}>
          {tournament.completed ? 'Completed' : 'Active'}
        </Badge>
      </div>
      <p className="mt-1 text-sm text-text-body">
        {formatDateRange(tournament.start, tournament.end)}
      </p>
      <p className="mt-0.5 text-sm text-text-muted">{tournament.bowlingCenter}</p>
    </div>
  )
}
