import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { createElement } from 'react'
import App from './App'

vi.mock('@/pages/TournamentsPage', () => ({
  TournamentsPage: () => <div data-testid="tournaments-page" />,
}))
vi.mock('@/pages/TournamentDetailPage', () => ({
  TournamentDetailPage: () => <div data-testid="detail-page" />,
}))
vi.mock('@/features/tournaments/TournamentOverview', () => ({
  TournamentOverview: () => <div data-testid="overview" />,
}))

function renderApp(initialPath = '/') {
  const queryClient = new QueryClient({ defaultOptions: { queries: { retry: false } } })
  return render(
    createElement(
      MemoryRouter,
      { initialEntries: [initialPath] },
      createElement(QueryClientProvider, { client: queryClient }, createElement(App)),
    ),
  )
}

describe('App — layout', () => {
  it('renders brand name in header', () => {
    renderApp('/tournaments')
    expect(screen.getByText('BowlingMegaBucks')).toBeInTheDocument()
  })

  it('renders all four nav links', () => {
    renderApp('/tournaments')
    expect(screen.getByRole('link', { name: 'Tournaments' })).toBeInTheDocument()
    expect(screen.getByRole('link', { name: 'Bowlers' })).toBeInTheDocument()
    expect(screen.getByRole('link', { name: 'Squads' })).toBeInTheDocument()
    expect(screen.getByRole('link', { name: 'Results' })).toBeInTheDocument()
  })

  it('renders footer with current year', () => {
    renderApp('/tournaments')
    expect(
      screen.getByText(new RegExp(String(new Date().getFullYear()))),
    ).toBeInTheDocument()
  })
})

describe('App — routing', () => {
  it('redirects / to /tournaments', () => {
    renderApp('/')
    expect(screen.getByTestId('tournaments-page')).toBeInTheDocument()
  })

  it('renders TournamentsPage at /tournaments', () => {
    renderApp('/tournaments')
    expect(screen.getByTestId('tournaments-page')).toBeInTheDocument()
  })

  it('renders TournamentDetailPage at /tournaments/:id/overview', () => {
    renderApp('/tournaments/uuid-1/overview')
    expect(screen.getByTestId('detail-page')).toBeInTheDocument()
  })
})
