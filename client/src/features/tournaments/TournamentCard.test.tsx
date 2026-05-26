import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom'
import { TournamentCard } from './TournamentCard'
import type { TournamentSummary } from '@/api/tournaments'

const mockNavigate = vi.fn()
vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await importOriginal<typeof import('react-router-dom')>()
  return { ...actual, useNavigate: () => mockNavigate }
})

const active: TournamentSummary = {
  id: 'uuid-1',
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  completed: false,
}

const completed: TournamentSummary = { ...active, completed: true }

function renderCard(t: TournamentSummary) {
  return render(
    <MemoryRouter>
      <TournamentCard tournament={t} />
    </MemoryRouter>,
  )
}

describe('TournamentCard', () => {
  it('renders tournament name', () => {
    renderCard(active)
    expect(screen.getByText('Fall Classic 2025')).toBeInTheDocument()
  })

  it('renders bowling center', () => {
    renderCard(active)
    expect(screen.getByText('Bowlero Tucson')).toBeInTheDocument()
  })

  it('renders date range', () => {
    renderCard(active)
    expect(screen.getByText(/Oct 10/)).toBeInTheDocument()
  })

  it('shows Active badge when not completed', () => {
    renderCard(active)
    expect(screen.getByText('Active')).toBeInTheDocument()
  })

  it('shows Completed badge when completed', () => {
    renderCard(completed)
    expect(screen.getByText('Completed')).toBeInTheDocument()
  })

  it('navigates to detail page on click', async () => {
    renderCard(active)
    await userEvent.click(screen.getByRole('button'))
    expect(mockNavigate).toHaveBeenCalledWith('/tournaments/uuid-1')
  })

  it('navigates on Enter keydown', async () => {
    renderCard(active)
    screen.getByRole('button').focus()
    await userEvent.keyboard('{Enter}')
    expect(mockNavigate).toHaveBeenCalledWith('/tournaments/uuid-1')
  })
})
