import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter, Route, Routes } from 'react-router-dom'
import { TournamentDetailLayout } from './TournamentDetailLayout'

vi.mock('@/api/tournaments', () => ({
  useTournament: vi.fn(),
}))

import { useTournament } from '@/api/tournaments'

const mockData = {
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

function renderLayout() {
  return render(
    <MemoryRouter initialEntries={['/tournaments/uuid-1/overview']}>
      <Routes>
        <Route path="/tournaments/:id" element={<TournamentDetailLayout />}>
          <Route path="overview" element={<div>overview content</div>} />
        </Route>
      </Routes>
    </MemoryRouter>,
  )
}

describe('TournamentDetailLayout', () => {
  beforeEach(() => vi.clearAllMocks())

  it('shows skeleton while loading', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: true,
      isError: false,
      data: undefined,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderLayout()
    expect(document.querySelector('.animate-pulse')).toBeInTheDocument()
  })

  it('shows ErrorBanner on error', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: true,
      data: undefined,
      error: new Error('Load failed'),
      refetch: vi.fn(),
    } as never)
    renderLayout()
    expect(screen.getByRole('alert')).toBeInTheDocument()
    expect(screen.getByText('Load failed')).toBeInTheDocument()
  })

  it('calls refetch when ErrorBanner retry is clicked', async () => {
    const refetch = vi.fn()
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: true,
      data: undefined,
      error: new Error('Load failed'),
      refetch,
    } as never)
    renderLayout()
    await userEvent.click(screen.getByRole('button', { name: /retry/i }))
    expect(refetch).toHaveBeenCalled()
  })

  it('renders header with tournament name on success', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockData,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderLayout()
    expect(screen.getByRole('heading', { name: 'Fall Classic 2025' })).toBeInTheDocument()
  })

  it('renders sidebar navigation', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockData,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderLayout()
    expect(screen.getAllByText('Overview').length).toBeGreaterThan(0)
  })

  it('renders outlet content', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockData,
      error: null,
      refetch: vi.fn(),
    } as never)
    renderLayout()
    expect(screen.getByText('overview content')).toBeInTheDocument()
  })
})
