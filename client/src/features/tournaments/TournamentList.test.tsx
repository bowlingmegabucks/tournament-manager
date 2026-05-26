import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom'
import { TournamentList } from './TournamentList'

vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await importOriginal<typeof import('react-router-dom')>()
  return { ...actual, useNavigate: () => vi.fn() }
})

vi.mock('@/api/tournaments', () => ({
  useTournaments: vi.fn(),
}))

import { useTournaments } from '@/api/tournaments'

const mockTournaments = [
  {
    id: 'uuid-1',
    name: 'Fall Classic 2025',
    start: '2025-10-10',
    end: '2025-10-12',
    bowlingCenter: 'Bowlero Tucson',
    completed: false,
  },
]

function renderList() {
  return render(
    <MemoryRouter>
      <TournamentList />
    </MemoryRouter>,
  )
}

describe('TournamentList — loading state', () => {
  it('renders skeleton when loading', () => {
    vi.mocked(useTournaments).mockReturnValue({
      isLoading: true,
      isError: false,
      data: undefined,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderList()
    expect(screen.getByTestId('tournament-list-skeleton')).toBeInTheDocument()
  })
})

describe('TournamentList — error state', () => {
  it('renders ErrorBanner on error', () => {
    vi.mocked(useTournaments).mockReturnValue({
      isLoading: false,
      isError: true,
      data: undefined,
      error: new Error('Network error'),
      refetch: vi.fn(),
    } as never)
    renderList()
    expect(screen.getByRole('alert')).toBeInTheDocument()
    expect(screen.getByText('Network error')).toBeInTheDocument()
  })

  it('calls refetch when Retry is clicked', async () => {
    const refetch = vi.fn()
    vi.mocked(useTournaments).mockReturnValue({
      isLoading: false,
      isError: true,
      data: undefined,
      error: new Error('Network error'),
      refetch,
    } as never)
    renderList()
    await userEvent.click(screen.getByRole('button', { name: /retry/i }))
    expect(refetch).toHaveBeenCalled()
  })
})

describe('TournamentList — data state', () => {
  it('renders desktop and mobile containers', () => {
    vi.mocked(useTournaments).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockTournaments,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderList()
    expect(screen.getByTestId('tournament-list-desktop')).toBeInTheDocument()
    expect(screen.getByTestId('tournament-list-mobile')).toBeInTheDocument()
  })

  it('renders tournament data', () => {
    vi.mocked(useTournaments).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockTournaments,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderList()
    expect(screen.getAllByText('Fall Classic 2025')).toHaveLength(2) // desktop + mobile
  })
})
