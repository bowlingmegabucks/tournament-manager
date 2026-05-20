import './App.css'

function App() {
  return (
    <>
      <header className="site-header">
        <div className="site-header__inner">
          <a href="/" className="site-header__logo">
            BowlingMegaBucks
          </a>
          <nav aria-label="Main">
            <ul className="site-nav">
              <li>
                <a href="/tournaments" aria-current="page">
                  Tournaments
                </a>
              </li>
              <li>
                <a href="/bowlers">Bowlers</a>
              </li>
              <li>
                <a href="/squads">Squads</a>
              </li>
              <li>
                <a href="/results">Results</a>
              </li>
            </ul>
          </nav>
        </div>
      </header>

      <main className="site-main">
        <div className="card">
          <h1>Tournament Manager</h1>
          <p>
            Welcome to the BowlingMegaBucks Tournament Manager. Select a
            tournament from the navigation to get started.
          </p>
          <div style={{ display: 'flex', gap: 'var(--space-3)' }}>
            <a href="/tournaments" className="btn btn-primary">
              View Tournaments
            </a>
            <a href="/bowlers" className="btn btn-secondary">
              Manage Bowlers
            </a>
          </div>
        </div>
      </main>

      <footer className="site-footer">
        <p>&copy; {new Date().getFullYear()} BowlingMegaBucks. All rights reserved.</p>
      </footer>
    </>
  )
}

export default App
