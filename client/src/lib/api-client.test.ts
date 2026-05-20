import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { apiFetch } from './api-client'

describe('apiFetch', () => {
  beforeEach(() => {
    vi.stubGlobal('fetch', vi.fn().mockResolvedValue(new Response()))
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('calls fetch with the given path', async () => {
    await apiFetch('/some/path')
    expect(fetch).toHaveBeenCalledWith('/some/path', undefined)
  })

  it('passes RequestInit options to fetch', async () => {
    const init: RequestInit = { method: 'POST', body: JSON.stringify({ x: 1 }) }
    await apiFetch('/endpoint', init)
    expect(fetch).toHaveBeenCalledWith('/endpoint', init)
  })

  it('returns the Response from fetch', async () => {
    const mockResponse = new Response('body', { status: 200 })
    vi.stubGlobal('fetch', vi.fn().mockResolvedValue(mockResponse))
    const result = await apiFetch('/test')
    expect(result).toBe(mockResponse)
  })

  it('calls fetch exactly once per invocation', async () => {
    await apiFetch('/once')
    expect(fetch).toHaveBeenCalledTimes(1)
  })

  it('passes undefined init when no init argument is supplied', async () => {
    await apiFetch('/no-init')
    expect(fetch).toHaveBeenCalledWith('/no-init', undefined)
  })

  it('works with various HTTP methods', async () => {
    const init: RequestInit = { method: 'DELETE' }
    await apiFetch('/resource/1', init)
    expect(fetch).toHaveBeenCalledWith('/resource/1', init)
  })
})
