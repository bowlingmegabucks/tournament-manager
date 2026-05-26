import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import type { UseQueryResult, UseMutationResult } from '@tanstack/react-query'
import { apiFetch } from '@/lib/api-client'

export type TournamentSummary = {
  id: string
  name: string
  start: string
  end: string
  bowlingCenter: string
  completed: boolean
}

export type TournamentDetail = TournamentSummary & {
  entryFee: number
  games: number
  finalsRatio: number
  cashRatio: number
  superSweeperCashRatio: number
  _counts: { divisions: number; squads: number; registrations: number }
}

export type TournamentUpdateInput = Omit<TournamentDetail, 'id' | '_counts'>

async function fetchTournaments(): Promise<TournamentSummary[]> {
  const res = await apiFetch('/tournaments', { headers: { 'x-api-version': '1' } })
  if (!res.ok) throw new Error('Failed to load tournaments')
  return res.json()
}

async function fetchTournament(id: string): Promise<TournamentDetail> {
  const res = await apiFetch(`/tournaments/${id}`, { headers: { 'x-api-version': '1' } })
  if (!res.ok) throw new Error('Failed to load tournament')
  return res.json()
}

async function putTournament(id: string, data: TournamentUpdateInput): Promise<TournamentDetail> {
  const res = await apiFetch(`/tournaments/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json', 'x-api-version': '1' },
    body: JSON.stringify(data),
  })
  if (!res.ok) throw new Error('Failed to update tournament')
  return res.json()
}

export function useTournaments(): UseQueryResult<TournamentSummary[]> {
  return useQuery({
    queryKey: ['tournaments'],
    queryFn: fetchTournaments,
  })
}

export function useTournament(id: string): UseQueryResult<TournamentDetail> {
  return useQuery({
    queryKey: ['tournament', id],
    queryFn: () => fetchTournament(id),
  })
}

export function useUpdateTournament(): UseMutationResult<
  TournamentDetail,
  Error,
  { id: string; data: TournamentUpdateInput }
> {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: ({ id, data }) => putTournament(id, data),
    onSuccess: (_, { id }) => {
      queryClient.invalidateQueries({ queryKey: ['tournament', id] })
      queryClient.invalidateQueries({ queryKey: ['tournaments'] })
    },
  })
}
