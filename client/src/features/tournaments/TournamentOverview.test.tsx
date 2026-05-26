import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { MemoryRouter, Route, Routes } from 'react-router-dom'
import { TournamentOverview } from './TournamentOverview'

vi.mock('@/api/tournaments', () => ({
  useTournament: vi.fn(),
  useUpdateTournament: vi.fn(),
}))

vi.mock('sonner', () => ({
  toast: { success: vi.fn(), error: vi.fn() },
}))

import { useTournament, useUpdateTournament } from '@/api/tournaments'
import { toast } from 'sonner'

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

function renderOverview(id = 'uuid-1') {
  return render(
    <MemoryRouter initialEntries={[`/tournaments/${id}/overview`]}>
      <Routes>
        <Route path="/tournaments/:id/overview" element={<TournamentOverview />} />
      </Routes>
    </MemoryRouter>,
  )
}

describe('TournamentOverview — loading', () => {
  it('renders skeleton while loading', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: true,
      isError: false,
      data: undefined,
      error: null,
      refetch: vi.fn(),
    } as never)
    vi.mocked(useUpdateTournament).mockReturnValue({ isPending: false } as never)
    renderOverview()
    expect(document.querySelector('.animate-pulse')).toBeInTheDocument()
  })
})

describe('TournamentOverview — error', () => {
  it('renders ErrorBanner on query error', () => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: true,
      data: undefined,
      error: new Error('Network error'),
      refetch: vi.fn(),
    } as never)
    vi.mocked(useUpdateTournament).mockReturnValue({ isPending: false } as never)
    renderOverview()
    expect(screen.getByRole('alert')).toBeInTheDocument()
  })
})

describe('TournamentOverview — read-only view', () => {
  beforeEach(() => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockData,
      error: null,
      refetch: vi.fn(),
    } as never)
    vi.mocked(useUpdateTournament).mockReturnValue({ isPending: false } as never)
  })

  it('shows tournament name', () => {
    renderOverview()
    expect(screen.getByText('Fall Classic 2025')).toBeInTheDocument()
  })

  it('shows bowling center', () => {
    renderOverview()
    expect(screen.getByText('Bowlero Tucson')).toBeInTheDocument()
  })

  it('shows formatted entry fee', () => {
    renderOverview()
    expect(screen.getByText('$50.00')).toBeInTheDocument()
  })

  it('shows Edit button', () => {
    renderOverview()
    expect(screen.getByRole('button', { name: 'Edit' })).toBeInTheDocument()
  })
})

describe('TournamentOverview — edit mode', () => {
  const mutateAsync = vi.fn()

  beforeEach(() => {
    vi.mocked(useTournament).mockReturnValue({
      isLoading: false,
      isError: false,
      data: mockData,
      error: null,
      refetch: vi.fn(),
    } as never)
    vi.mocked(useUpdateTournament).mockReturnValue({
      isPending: false,
      mutateAsync,
    } as never)
  })

  it('shows form fields after clicking Edit', async () => {
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    expect(screen.getByLabelText('Name')).toBeInTheDocument()
  })

  it('pre-fills name field with current value', async () => {
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    expect(screen.getByLabelText('Name')).toHaveValue('Fall Classic 2025')
  })

  it('Cancel discards changes and hides form', async () => {
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    await userEvent.clear(screen.getByLabelText('Name'))
    await userEvent.type(screen.getByLabelText('Name'), 'Changed')
    await userEvent.click(screen.getByRole('button', { name: 'Cancel' }))
    expect(screen.queryByLabelText('Name')).not.toBeInTheDocument()
    expect(screen.getByText('Fall Classic 2025')).toBeInTheDocument()
  })

  it('shows inline error when name is cleared and Save clicked', async () => {
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    await userEvent.clear(screen.getByLabelText('Name'))
    await userEvent.click(screen.getByRole('button', { name: 'Save' }))
    await waitFor(() => {
      expect(screen.getByText('Name is required')).toBeInTheDocument()
    })
  })

  it('calls mutateAsync on valid save', async () => {
    mutateAsync.mockResolvedValue(mockData)
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    await userEvent.click(screen.getByRole('button', { name: 'Save' }))
    await waitFor(() => expect(mutateAsync).toHaveBeenCalled())
  })

  it('shows success toast after successful save', async () => {
    mutateAsync.mockResolvedValue(mockData)
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    await userEvent.click(screen.getByRole('button', { name: 'Save' }))
    await waitFor(() => expect(toast.success).toHaveBeenCalledWith('Tournament saved'))
  })

  it('shows error toast when save fails', async () => {
    mutateAsync.mockRejectedValue(new Error('Save failed'))
    renderOverview()
    await userEvent.click(screen.getByRole('button', { name: 'Edit' }))
    await userEvent.click(screen.getByRole('button', { name: 'Save' }))
    await waitFor(() => expect(toast.error).toHaveBeenCalledWith('Failed to save tournament'))
  })
})
