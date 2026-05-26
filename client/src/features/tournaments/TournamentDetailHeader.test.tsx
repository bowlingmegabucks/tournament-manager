import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { TournamentDetailHeader } from './TournamentDetailHeader'
import type { TournamentDetail } from '@/api/tournaments'

const tournament: TournamentDetail = {
  id: 'uuid-1',
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  completed: false,
  entryFee: 50,
  games: 6,
  finalsRatio: 7,
  cashRatio: 5,
  superSweeperCashRatio: 4,
  _counts: { divisions: 2, squads: 3, registrations: 48 },
}

describe('TournamentDetailHeader', () => {
  it('renders tournament name', () => {
    render(<TournamentDetailHeader tournament={tournament} />)
    expect(screen.getByRole('heading', { name: 'Fall Classic 2025' })).toBeInTheDocument()
  })

  it('renders Active badge for active tournament', () => {
    render(<TournamentDetailHeader tournament={tournament} />)
    expect(screen.getByText('Active')).toBeInTheDocument()
  })

  it('renders Completed badge for completed tournament', () => {
    render(<TournamentDetailHeader tournament={{ ...tournament, completed: true }} />)
    expect(screen.getByText('Completed')).toBeInTheDocument()
  })

  it('renders date range in meta strip', () => {
    render(<TournamentDetailHeader tournament={tournament} />)
    expect(screen.getByText(/Oct 10/)).toBeInTheDocument()
  })

  it('renders bowling center in meta strip', () => {
    render(<TournamentDetailHeader tournament={tournament} />)
    expect(screen.getByText(/Bowlero Tucson/)).toBeInTheDocument()
  })
})
