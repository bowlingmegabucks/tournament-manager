import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
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

function renderHeader(t = tournament) {
  return render(
    <MemoryRouter>
      <TournamentDetailHeader tournament={t} />
    </MemoryRouter>,
  )
}

describe('TournamentDetailHeader', () => {
  it('renders tournament name as a heading', () => {
    renderHeader()
    expect(screen.getByRole('heading', { name: 'Fall Classic 2025' })).toBeInTheDocument()
  })

  it('renders Active badge for active tournament', () => {
    renderHeader()
    expect(screen.getByText('Active')).toBeInTheDocument()
  })

  it('renders Completed badge for completed tournament', () => {
    renderHeader({ ...tournament, completed: true })
    expect(screen.getByText('Completed')).toBeInTheDocument()
  })

  it('renders back button', () => {
    renderHeader()
    expect(screen.getByRole('button', { name: /back to tournaments/i })).toBeInTheDocument()
  })

  it('renders Edit Details button', () => {
    renderHeader()
    expect(screen.getByRole('button', { name: /edit details/i })).toBeInTheDocument()
  })

  it('renders date range in meta strip', () => {
    renderHeader()
    expect(screen.getByText(/Oct 10/)).toBeInTheDocument()
  })

  it('renders bowling center in meta strip', () => {
    renderHeader()
    expect(screen.getByText(/Bowlero Tucson/)).toBeInTheDocument()
  })

  it('renders entry fee in meta strip', () => {
    renderHeader()
    expect(screen.getByText(/\$50/)).toBeInTheDocument()
  })

  it('renders games count in meta strip', () => {
    renderHeader()
    expect(screen.getByText('6')).toBeInTheDocument()
  })

  it('renders finals ratio in meta strip', () => {
    renderHeader()
    expect(screen.getByText('1:7')).toBeInTheDocument()
  })

  it('renders cash ratio in meta strip', () => {
    renderHeader()
    expect(screen.getByText('1:5')).toBeInTheDocument()
  })
})
