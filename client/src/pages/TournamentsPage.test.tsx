import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { TournamentsPage } from './TournamentsPage'

vi.mock('@/features/tournaments/TournamentList', () => ({
  TournamentList: () => <div data-testid="tournament-list" />,
}))

describe('TournamentsPage', () => {
  it('renders page heading', () => {
    render(
      <MemoryRouter>
        <TournamentsPage />
      </MemoryRouter>,
    )
    expect(screen.getByRole('heading', { name: 'Tournaments' })).toBeInTheDocument()
  })

  it('renders TournamentList', () => {
    render(
      <MemoryRouter>
        <TournamentsPage />
      </MemoryRouter>,
    )
    expect(screen.getByTestId('tournament-list')).toBeInTheDocument()
  })
})
