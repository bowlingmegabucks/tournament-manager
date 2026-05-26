import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom'
import { TournamentListDesktop } from './TournamentListDesktop'
import type { TournamentSummary } from '@/api/tournaments'

const mockNavigate = vi.fn()
vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await importOriginal<typeof import('react-router-dom')>()
  return { ...actual, useNavigate: () => mockNavigate }
})

const tournaments: TournamentSummary[] = [
  {
    id: 'uuid-1',
    name: 'Fall Classic 2025',
    start: '2025-10-10',
    end: '2025-10-12',
    bowlingCenter: 'Bowlero Tucson',
    completed: false,
  },
  {
    id: 'uuid-2',
    name: 'Spring Open 2025',
    start: '2025-04-05',
    end: '2025-04-07',
    bowlingCenter: 'Lucky Lanes',
    completed: true,
  },
]

function renderTable() {
  return render(
    <MemoryRouter>
      <TournamentListDesktop tournaments={tournaments} />
    </MemoryRouter>,
  )
}

describe('TournamentListDesktop', () => {
  it('renders a table element', () => {
    renderTable()
    expect(screen.getByRole('table')).toBeInTheDocument()
  })

  it('renders a row for each tournament', () => {
    renderTable()
    expect(screen.getByText('Fall Classic 2025')).toBeInTheDocument()
    expect(screen.getByText('Spring Open 2025')).toBeInTheDocument()
  })

  it('renders formatted dates', () => {
    renderTable()
    expect(screen.getByText('Oct 10, 2025')).toBeInTheDocument()
  })

  it('renders bowling center', () => {
    renderTable()
    expect(screen.getByText('Bowlero Tucson')).toBeInTheDocument()
  })

  it('renders Active badge for non-completed', () => {
    renderTable()
    expect(screen.getByText('Active')).toBeInTheDocument()
  })

  it('renders Completed badge for completed', () => {
    renderTable()
    expect(screen.getByText('Completed')).toBeInTheDocument()
  })

  it('navigates when name link is clicked', async () => {
    renderTable()
    await userEvent.click(screen.getByRole('button', { name: 'Fall Classic 2025' }))
    expect(mockNavigate).toHaveBeenCalledWith('/tournaments/uuid-1')
  })
})
