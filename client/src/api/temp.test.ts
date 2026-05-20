import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { renderHook, waitFor } from '@testing-library/react'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { createElement } from 'react'
import { useTempData, useTempError } from './temp'

function makeWrapper() {
  const client = new QueryClient({
    defaultOptions: { queries: { retry: false } },
  })
  return ({ children }: { children: React.ReactNode }) =>
    createElement(QueryClientProvider, { client }, children)
}

const MOCK_ROWS = Array.from({ length: 10 }, (_, i) => ({
  rank: i + 1,
  bowler: `Bowler ${i + 1}`,
  division: 'Open',
  city: 'Akron',
  score: 220 + i,
}))

beforeEach(() => {
  vi.stubGlobal('fetch', vi.fn())
})

afterEach(() => {
  vi.restoreAllMocks()
})

describe('useTempData', () => {
  it('starts with no data and does not fetch automatically', () => {
    vi.mocked(fetch).mockResolvedValue(new Response(JSON.stringify({ data: MOCK_ROWS })))
    const { result } = renderHook(() => useTempData(), { wrapper: makeWrapper() })
    expect(result.current.data).toBeUndefined()
    expect(fetch).not.toHaveBeenCalled()
  })

  it('returns data after refetch', async () => {
    vi.mocked(fetch).mockResolvedValue(
      new Response(JSON.stringify({ data: MOCK_ROWS }), { status: 200 }),
    )
    const { result } = renderHook(() => useTempData(), { wrapper: makeWrapper() })
    result.current.refetch()
    await waitFor(() => expect(result.current.isSuccess).toBe(true))
    expect(result.current.data).toHaveLength(10)
    expect(result.current.data![0]).toMatchObject({ rank: 1, bowler: 'Bowler 1' })
  })

  it('sets isError when server returns non-ok status', async () => {
    vi.mocked(fetch).mockResolvedValue(
      new Response(JSON.stringify({ error: { code: 'INTERNAL_ERROR', message: 'Oops' } }), {
        status: 500,
      }),
    )
    const { result } = renderHook(() => useTempData(), { wrapper: makeWrapper() })
    result.current.refetch()
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toContain('Oops')
  })

  it('uses the server error message from the envelope', async () => {
    vi.mocked(fetch).mockResolvedValue(
      new Response(JSON.stringify({ error: { code: 'INTERNAL_ERROR', message: 'Custom message' } }), {
        status: 500,
      }),
    )
    const { result } = renderHook(() => useTempData(), { wrapper: makeWrapper() })
    result.current.refetch()
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toBe('Custom message')
  })

  it('falls back to generic message when error body is not JSON', async () => {
    vi.mocked(fetch).mockResolvedValue(new Response('not json', { status: 500 }))
    const { result } = renderHook(() => useTempData(), { wrapper: makeWrapper() })
    result.current.refetch()
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toMatch(/500/)
  })
})

describe('useTempError', () => {
  it('does not fetch automatically', () => {
    vi.mocked(fetch).mockResolvedValue(new Response('{}', { status: 500 }))
    const { result } = renderHook(() => useTempError(), { wrapper: makeWrapper() })
    expect(fetch).not.toHaveBeenCalled()
    expect(result.current.isError).toBe(false)
  })

  it('sets isError after refetch on a 500 response', async () => {
    vi.mocked(fetch).mockResolvedValue(
      new Response(JSON.stringify({ error: { code: 'INTERNAL_ERROR', message: 'Server exploded' } }), {
        status: 500,
      }),
    )
    const { result } = renderHook(() => useTempError(), { wrapper: makeWrapper() })
    result.current.refetch()
    await waitFor(() => expect(result.current.isError).toBe(true))
    expect(result.current.error?.message).toBe('Server exploded')
  })
})
