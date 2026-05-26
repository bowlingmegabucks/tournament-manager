import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { renderHook, waitFor } from '@testing-library/react'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { createElement } from 'react'
import { useTournaments, useTournament, useUpdateTournament } from './tournaments'

vi.mock('@/lib/api-client', () => ({
  apiFetch: vi.fn(),
}))

import { apiFetch } from '@/lib/api-client'

function makeWrapper() {
  const queryClient = new QueryClient({
    defaultOptions: { queries: { retry: false } },
  })
  return ({ children }: { children: React.ReactNode }) =>
    createElement(QueryClientProvider, { client: queryClient }, children)
}

const mockSummary = {
  id: 'uuid-1',
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  completed: false,
}

const mockDetail = {
  ...mockSummary,
  entryFee: 50.0,
  games: 6,
  finalsRatio: 7.0,
  cashRatio: 5.0,
  superSweeperCashRatio: 4.0,
  _counts: { divisions: 2, squads: 3, registrations: 48 },
}

function mockOkResponse(data: unknown) {
  return Promise.resolve({
    ok: true,
    json: () => Promise.resolve(data),
  })
}

function mockErrorResponse() {
  return Promise.resolve({ ok: false, json: () => Promise.resolve({}) })
}

describe('useTournaments', () => {
  beforeEach(() => vi.clearAllMocks())
  afterEach(() => vi.restoreAllMocks())

  it('returns data on success', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockOkResponse([mockSummary]) as never)
    const { result } = renderHook(() => useTournaments(), { wrapper: makeWrapper() })
    await waitFor(() => expect(result.current.isSuccess).toBe(true))
    expect(result.current.data).toEqual([mockSummary])
  })

  it('returns error on fetch failure', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockErrorResponse() as never)
    const { result } = renderHook(() => useTournaments(), { wrapper: makeWrapper() })
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toBe('Failed to load tournaments')
  })

  it('starts in loading state', () => {
    vi.mocked(apiFetch).mockReturnValue(new Promise(() => {}) as never)
    const { result } = renderHook(() => useTournaments(), { wrapper: makeWrapper() })
    expect(result.current.isLoading).toBe(true)
  })
})

describe('useTournament', () => {
  beforeEach(() => vi.clearAllMocks())

  it('returns detail data on success', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockOkResponse(mockDetail) as never)
    const { result } = renderHook(() => useTournament('uuid-1'), { wrapper: makeWrapper() })
    await waitFor(() => expect(result.current.isSuccess).toBe(true))
    expect(result.current.data).toEqual(mockDetail)
  })

  it('returns error on fetch failure', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockErrorResponse() as never)
    const { result } = renderHook(() => useTournament('uuid-1'), { wrapper: makeWrapper() })
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toBe('Failed to load tournament')
  })
})

describe('useUpdateTournament', () => {
  beforeEach(() => vi.clearAllMocks())

  it('calls PUT and returns updated tournament', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockOkResponse(mockDetail) as never)
    const { result } = renderHook(() => useUpdateTournament(), { wrapper: makeWrapper() })
    result.current.mutate({
      id: 'uuid-1',
      data: {
        name: 'Fall Classic 2025',
        start: '2025-10-10',
        end: '2025-10-12',
        bowlingCenter: 'Bowlero Tucson',
        entryFee: 50.0,
        games: 6,
        finalsRatio: 7.0,
        cashRatio: 5.0,
        superSweeperCashRatio: 4.0,
        completed: false,
      },
    })
    await waitFor(() => expect(result.current.isSuccess).toBe(true))
    expect(result.current.data).toEqual(mockDetail)
  })

  it('returns error when PUT fails', async () => {
    vi.mocked(apiFetch).mockReturnValue(mockErrorResponse() as never)
    const { result } = renderHook(() => useUpdateTournament(), { wrapper: makeWrapper() })
    result.current.mutate({
      id: 'uuid-1',
      data: {
        name: 'x',
        start: '2025-10-10',
        end: '2025-10-12',
        bowlingCenter: 'x',
        entryFee: 50.0,
        games: 6,
        finalsRatio: 7.0,
        cashRatio: 5.0,
        superSweeperCashRatio: 4.0,
        completed: false,
      },
    })
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toBe('Failed to update tournament')
  })
})
