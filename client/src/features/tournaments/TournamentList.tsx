import { ErrorBanner } from '@/components/ui/error-banner'
import { Skeleton } from '@/components/ui/skeleton'
import { TournamentListDesktop } from './TournamentListDesktop'
import { TournamentListMobile } from './TournamentListMobile'
import { useTournaments } from '@/api/tournaments'

export function TournamentList() {
  const { data, isLoading, isError, error, refetch } = useTournaments()

  if (isLoading) {
    return (
      <div className="space-y-3" data-testid="tournament-list-skeleton">
        <Skeleton className="h-10 w-full" />
        <Skeleton className="h-10 w-full" />
        <Skeleton className="h-10 w-full" />
      </div>
    )
  }

  if (isError) {
    return (
      <ErrorBanner
        message={error?.message ?? 'Failed to load tournaments'}
        onRetry={() => refetch()}
      />
    )
  }

  const tournaments = data ?? []

  return (
    <>
      {/* Desktop: md+ */}
      <div className="hidden md:block" data-testid="tournament-list-desktop">
        <TournamentListDesktop tournaments={tournaments} />
      </div>
      {/* Mobile: < md */}
      <div className="block md:hidden" data-testid="tournament-list-mobile">
        <TournamentListMobile tournaments={tournaments} />
      </div>
    </>
  )
}
