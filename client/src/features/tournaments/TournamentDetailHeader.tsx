import { useNavigate } from 'react-router-dom'
import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import { formatCurrency, formatDateRange } from '@/lib/format'
import type { TournamentDetail } from '@/api/tournaments'

interface TournamentDetailHeaderProps {
  readonly tournament: TournamentDetail
}

export function TournamentDetailHeader({ tournament }: TournamentDetailHeaderProps) {
  const navigate = useNavigate()

  return (
    <div>
      <div className="flex items-center gap-3 bg-brand-black px-5 py-3 text-text-on-dark">
        <button
          onClick={() => navigate('/tournaments')}
          className="text-xl opacity-70 transition-opacity hover:opacity-100"
          aria-label="Back to tournaments"
        >
          ←
        </button>
        <h2 className="font-heading text-base font-semibold">{tournament.name}</h2>
        <Badge variant={tournament.completed ? 'neutral' : 'success'}>
          {tournament.completed ? 'Completed' : 'Active'}
        </Badge>
        <span className="flex-1" />
        <Button
          variant="outline"
          size="sm"
          onClick={() => navigate(`/tournaments/${tournament.id}/overview`)}
          className="border-white/30 bg-white/15 text-white hover:bg-white/25 hover:text-white"
        >
          Edit Details
        </Button>
      </div>

      <div className="flex flex-wrap gap-6 border-b border-border bg-surface-subtle px-5 py-2 text-xs text-text-muted">
        <span>
          <strong className="text-text-primary">{formatDateRange(tournament.start, tournament.end)}</strong>
        </span>
        <span>{tournament.bowlingCenter}</span>
        <span>Entry: <strong className="text-text-primary">{formatCurrency(tournament.entryFee)}</strong></span>
        <span>Games: <strong className="text-text-primary">{tournament.games}</strong></span>
        <span>Finals ratio: <strong className="text-text-primary">1:{tournament.finalsRatio}</strong></span>
        <span>Cash ratio: <strong className="text-text-primary">1:{tournament.cashRatio}</strong></span>
      </div>
    </div>
  )
}
