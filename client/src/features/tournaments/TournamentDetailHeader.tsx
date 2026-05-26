import { Badge } from '@/components/ui/badge'
import { formatDateRange } from '@/lib/format'
import type { TournamentDetail } from '@/api/tournaments'

interface TournamentDetailHeaderProps {
  readonly tournament: TournamentDetail
}

export function TournamentDetailHeader({ tournament }: TournamentDetailHeaderProps) {
  return (
    <div className="mb-6">
      <div className="flex flex-wrap items-center gap-3">
        <h1 className="font-heading text-2xl font-bold text-text-primary">{tournament.name}</h1>
        <Badge variant={tournament.completed ? 'neutral' : 'success'}>
          {tournament.completed ? 'Completed' : 'Active'}
        </Badge>
      </div>
      <p className="mt-1 text-sm text-text-muted">
        {formatDateRange(tournament.start, tournament.end)} &middot; {tournament.bowlingCenter}
      </p>
    </div>
  )
}
