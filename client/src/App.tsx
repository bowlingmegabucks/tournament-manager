import { Button } from '@/components/ui/button'
import { Card, CardContent } from '@/components/ui/card'

const NAV_LINKS = [
  { label: 'Tournaments', href: '/tournaments' },
  { label: 'Bowlers', href: '/bowlers' },
  { label: 'Squads', href: '/squads' },
  { label: 'Results', href: '/results' },
]

function App() {
  return (
    <div className="flex min-h-svh flex-col">
      <header className="sticky top-0 z-50 bg-brand-black shadow-nav">
        <div className="mx-auto flex max-w-6xl items-center gap-8 px-8">
          <a
            href="/"
            className="py-5 font-heading text-lg font-bold text-text-on-dark no-underline hover:text-interactive"
          >
            BowlingMegaBucks
          </a>
          <nav aria-label="Main">
            <ul className="flex list-none gap-1 p-0 m-0">
              {NAV_LINKS.map(({ label, href }) => (
                <li key={href}>
                  <a
                    href={href}
                    className="block px-[15px] py-[10px] font-heading text-sm font-bold text-text-on-dark no-underline transition-colors duration-300 hover:text-interactive"
                  >
                    {label}
                  </a>
                </li>
              ))}
            </ul>
          </nav>
        </div>
      </header>

      <main className="mx-auto w-full max-w-6xl flex-1 px-8 py-[50px]">
        <Card>
          <CardContent className="p-8">
            <h1 className="mb-4">Tournament Manager</h1>
            <p className="mb-6 text-text-body">
              Welcome to the BowlingMegaBucks Tournament Manager. Select a
              tournament from the navigation to get started.
            </p>
            <div className="flex gap-3">
              <Button asChild>
                <a href="/tournaments">View Tournaments</a>
              </Button>
              <Button variant="outline" asChild>
                <a href="/bowlers">Manage Bowlers</a>
              </Button>
            </div>
          </CardContent>
        </Card>
      </main>

      <footer className="bg-brand-black py-8 text-center text-sm text-text-on-dark">
        &copy; {new Date().getFullYear()} BowlingMegaBucks. All rights reserved.
      </footer>
    </div>
  )
}

export default App
