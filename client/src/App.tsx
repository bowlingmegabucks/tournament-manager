import { Routes, Route, Navigate, Link, useLocation } from 'react-router-dom'
import { TournamentsPage } from '@/pages/TournamentsPage'
import { TournamentDetailPage } from '@/pages/TournamentDetailPage'
import { TournamentOverview } from '@/features/tournaments/TournamentOverview'
import { ComingSoon } from '@/components/ComingSoon'
import { cn } from '@/lib/utils'

const NAV_LINKS = [
  { label: 'Tournaments', href: '/tournaments' },
  { label: 'Bowlers', href: '/bowlers' },
  { label: 'Squads', href: '/squads' },
  { label: 'Results', href: '/results' },
]

function NavItem({ label, href }: { readonly label: string; readonly href: string }) {
  const location = useLocation()
  const isActive = location.pathname.startsWith(href)
  return (
    <li>
      <Link
        to={href}
        className={cn(
          'block px-[15px] py-[10px] font-heading text-sm font-bold text-text-on-dark no-underline transition-colors duration-300 hover:text-interactive',
          isActive && 'text-interactive',
        )}
      >
        {label}
      </Link>
    </li>
  )
}

function App() {
  return (
    <div className="flex min-h-svh flex-col">
      <header className="sticky top-0 z-50 bg-brand-black shadow-nav">
        <div className="mx-auto flex max-w-6xl items-center gap-8 px-8">
          <Link
            to="/"
            className="py-5 font-heading text-lg font-bold text-text-on-dark no-underline hover:text-interactive"
          >
            BowlingMegaBucks
          </Link>
          <nav aria-label="Main">
            <ul className="flex list-none gap-1 p-0 m-0">
              {NAV_LINKS.map(({ label, href }) => (
                <NavItem key={href} label={label} href={href} />
              ))}
            </ul>
          </nav>
        </div>
      </header>

      <main className="mx-auto w-full max-w-6xl flex-1 px-8 py-[50px]">
        <Routes>
          <Route index element={<Navigate to="/tournaments" replace />} />
          <Route path="/tournaments" element={<TournamentsPage />} />
          <Route path="/tournaments/:id" element={<TournamentDetailPage />}>
            <Route index element={<Navigate to="overview" replace />} />
            <Route path="overview" element={<TournamentOverview />} />
            <Route path="divisions" element={<ComingSoon section="Divisions" />} />
            <Route path="squads" element={<ComingSoon section="Squads" />} />
            <Route path="registrations" element={<ComingSoon section="Registrations" />} />
            <Route path="lanes" element={<ComingSoon section="Lane Assignments" />} />
            <Route path="results" element={<ComingSoon section="Results" />} />
            <Route path="sweepers" element={<ComingSoon section="Sweepers" />} />
          </Route>
        </Routes>
      </main>

      <footer className="bg-brand-black py-8 text-center text-sm text-text-on-dark">
        &copy; {new Date().getFullYear()} BowlingMegaBucks. All rights reserved.
      </footer>
    </div>
  )
}

export default App
