import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { TournamentListMobile } from './TournamentListMobile'
import type { TournamentSummary } from '@/api/tournaments'

vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await importOriginal<typeof import('react-router-dom')>()
  return { ...actual, useNavigate: () => vi.fn() }
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

describe('TournamentListMobile', () => {
  it('renders a card for each tournament', () => {
    render(
      <MemoryRouter>
        <TournamentListMobile tournaments={tournaments} />
      </MemoryRouter>,
    )
    expect(screen.getByText('Fall Classic 2025')).toBeInTheDocument()
    expect(screen.getByText('Spring Open 2025')).toBeInTheDocument()
  })

  it('renders no cards for empty list', () => {
    render(
      <MemoryRouter>
        <TournamentListMobile tournaments={[]} />
      </MemoryRouter>,
    )
    expect(screen.queryByRole('button')).not.toBeInTheDocument()
  })
})
