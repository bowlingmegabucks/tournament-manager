import { useEffect } from 'react'
import { toast } from 'sonner'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader } from '@/components/ui/card'
import { ErrorBanner } from '@/components/ui/error-banner'
import { useTempData, useTempError } from '@/api/temp'

const NAV_LINKS = [
  { label: 'Tournaments', href: '/tournaments' },
  { label: 'Bowlers', href: '/bowlers' },
  { label: 'Squads', href: '/squads' },
  { label: 'Results', href: '/results' },
]

function App() {
  const dataQuery = useTempData()
  const errorQuery = useTempError()

  useEffect(() => {
    if (dataQuery.isSuccess) {
      toast.success('Data loaded successfully')
    }
  }, [dataQuery.isSuccess, dataQuery.dataUpdatedAt])

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

      <main className="mx-auto w-full max-w-6xl flex-1 px-8 py-[50px] space-y-6">
        {/* Data + toast demo */}
        <Card>
          <CardHeader className="px-8 pt-8 pb-0">
            <h2 className="text-text-primary">API + Toast Demo</h2>
            <p className="text-sm text-text-muted mt-1">
              Fetches random bowler data and fires a success toast on load.
            </p>
          </CardHeader>
          <CardContent className="p-8">
            <Button
              onClick={() => dataQuery.refetch()}
              disabled={dataQuery.isFetching}
              className="mb-6"
            >
              {dataQuery.isFetching ? 'Loading…' : 'Load Data'}
            </Button>

            {dataQuery.isError && (
              <ErrorBanner
                message={dataQuery.error.message}
                onRetry={() => dataQuery.refetch()}
                className="mb-4"
              />
            )}

            {dataQuery.data && (
              <div className="overflow-x-auto">
                <table className="w-full text-sm">
                  <thead>
                    <tr className="bg-brand-black text-text-on-dark">
                      <th className="px-4 py-3 text-left font-heading">Rank</th>
                      <th className="px-4 py-3 text-left font-heading">Bowler</th>
                      <th className="px-4 py-3 text-left font-heading">Division</th>
                      <th className="px-4 py-3 text-left font-heading">City</th>
                      <th className="px-4 py-3 text-right font-heading">Score</th>
                    </tr>
                  </thead>
                  <tbody>
                    {dataQuery.data.map((row, i) => (
                      <tr
                        key={row.rank}
                        className={i % 2 === 0 ? 'bg-surface-page' : 'bg-surface-subtle'}
                      >
                        <td className="px-4 py-2 text-text-muted">{row.rank}</td>
                        <td className="px-4 py-2 font-semibold text-text-primary">{row.bowler}</td>
                        <td className="px-4 py-2 text-text-body">{row.division}</td>
                        <td className="px-4 py-2 text-text-body">{row.city}</td>
                        <td className="px-4 py-2 text-right font-heading text-text-primary">{row.score}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </CardContent>
        </Card>

        {/* Error banner demo */}
        <Card>
          <CardHeader className="px-8 pt-8 pb-0">
            <h2 className="text-text-primary">Error Banner Demo</h2>
            <p className="text-sm text-text-muted mt-1">
              Always returns a 500 — shows the inline error banner pattern.
            </p>
          </CardHeader>
          <CardContent className="p-8">
            <Button
              variant="outline"
              onClick={() => errorQuery.refetch()}
              disabled={errorQuery.isFetching}
              className="mb-6"
            >
              {errorQuery.isFetching ? 'Loading…' : 'Trigger 500 Error'}
            </Button>

            {errorQuery.isError && (
              <ErrorBanner
                message={errorQuery.error.message}
                onRetry={() => errorQuery.refetch()}
              />
            )}
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
