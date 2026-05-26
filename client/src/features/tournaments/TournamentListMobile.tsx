import { TournamentCard } from './TournamentCard'
import type { TournamentSummary } from '@/api/tournaments'

interface TournamentListMobileProps {
  readonly tournaments: TournamentSummary[]
}

export function TournamentListMobile({ tournaments }: TournamentListMobileProps) {
  return (
    <div className="flex flex-col gap-3">
      {tournaments.map((t) => (
        <TournamentCard key={t.id} tournament={t} />
      ))}
    </div>
  )
}
