import { TournamentList } from '@/features/tournaments/TournamentList'

export function TournamentsPage() {
  return (
    <div>
      <h1 className="mb-6 font-heading text-2xl font-bold text-text-primary">Tournaments</h1>
      <TournamentList />
    </div>
  )
}
