import { useQuery } from '@tanstack/react-query'

export interface TempRow {
  rank: number
  bowler: string
  division: string
  city: string
  score: number
}

async function fetchTempData(): Promise<TempRow[]> {
  const res = await fetch('/api/v1/temp/data')
  if (!res.ok) {
    const body = await res.json().catch(() => ({}))
    throw new Error(body?.error?.message ?? `Request failed (${res.status})`)
  }
  const json = await res.json()
  return json.data
}

async function fetchTempError(): Promise<never> {
  const res = await fetch('/api/v1/temp/error')
  if (!res.ok) {
    const body = await res.json().catch(() => ({}))
    throw new Error(body?.error?.message ?? `Request failed (${res.status})`)
  }
  throw new Error('Expected a 500 but server returned success')
}

export function useTempData() {
  return useQuery({
    queryKey: ['temp', 'data'],
    queryFn: fetchTempData,
    enabled: false,
  })
}

export function useTempError() {
  return useQuery({
    queryKey: ['temp', 'error'],
    queryFn: fetchTempError,
    enabled: false,
    retry: false,
  })
}
