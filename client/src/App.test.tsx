import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import App from './App'
import { useTempData, useTempError } from '@/api/temp'
import { toast } from 'sonner'
import type { TempRow } from '@/api/temp'

vi.mock('@/api/temp', () => ({
  useTempData: vi.fn(),
  useTempError: vi.fn(),
}))

vi.mock('sonner', () => ({
  toast: { success: vi.fn() },
}))

const mockUseTempData = vi.mocked(useTempData)
const mockUseTempError = vi.mocked(useTempError)
const mockToastSuccess = vi.mocked(toast.success)

type TempDataQuery = ReturnType<typeof useTempData>
type TempErrorQuery = ReturnType<typeof useTempError>

function makeQuery<T extends TempDataQuery | TempErrorQuery = TempDataQuery>(
  overrides: Partial<TempDataQuery> = {},
): T {
  return {
    isSuccess: false,
    isError: false,
    isFetching: false,
    data: undefined,
    error: null,
    refetch: vi.fn(),
    dataUpdatedAt: 0,
    ...overrides,
  } as unknown as T
}

const MOCK_ROWS: TempRow[] = [
  { rank: 1, bowler: 'Alice Smith', division: 'Open', city: 'Akron', score: 300 },
  { rank: 2, bowler: 'Bob Jones', division: 'Senior', city: 'Toledo', score: 289 },
]

describe('App', () => {
  beforeEach(() => {
    mockUseTempData.mockReturnValue(makeQuery())
    mockUseTempError.mockReturnValue(makeQuery<TempErrorQuery>())
    mockToastSuccess.mockClear()
  })

  describe('layout', () => {
    it('renders the brand name in the header', () => {
      render(<App />)
      expect(screen.getByText('BowlingMegaBucks')).toBeInTheDocument()
    })

    it('renders all four nav links', () => {
      render(<App />)
      expect(screen.getByRole('link', { name: 'Tournaments' })).toBeInTheDocument()
      expect(screen.getByRole('link', { name: 'Bowlers' })).toBeInTheDocument()
      expect(screen.getByRole('link', { name: 'Squads' })).toBeInTheDocument()
      expect(screen.getByRole('link', { name: 'Results' })).toBeInTheDocument()
    })

    it('renders the footer with current year', () => {
      render(<App />)
      expect(screen.getByText(new RegExp(String(new Date().getFullYear())))).toBeInTheDocument()
    })
  })

  describe('Load Data card', () => {
    it('renders the Load Data button in default state', () => {
      render(<App />)
      expect(screen.getByRole('button', { name: 'Load Data' })).toBeInTheDocument()
    })

    it('shows Loading… on the Load Data button while fetching', () => {
      mockUseTempData.mockReturnValue(makeQuery({ isFetching: true }))
      render(<App />)
      const buttons = screen.getAllByRole('button', { name: 'Loading…' })
      expect(buttons).toHaveLength(1)
    })

    it('disables the Load Data button while fetching', () => {
      mockUseTempData.mockReturnValue(makeQuery({ isFetching: true }))
      render(<App />)
      expect(screen.getByRole('button', { name: 'Loading…' })).toBeDisabled()
    })

    it('calls refetch when Load Data is clicked', async () => {
      const refetch = vi.fn()
      mockUseTempData.mockReturnValue(makeQuery({ refetch }))
      render(<App />)
      await userEvent.click(screen.getByRole('button', { name: 'Load Data' }))
      expect(refetch).toHaveBeenCalledOnce()
    })

    it('shows an error banner when the data query fails', () => {
      mockUseTempData.mockReturnValue(
        makeQuery({ isError: true, error: new Error('Network error') }),
      )
      render(<App />)
      expect(screen.getByRole('alert')).toBeInTheDocument()
      expect(screen.getByText('Network error')).toBeInTheDocument()
    })

    it('shows the data table when data is loaded', () => {
      mockUseTempData.mockReturnValue(makeQuery({ data: MOCK_ROWS }))
      render(<App />)
      expect(screen.getByText('Alice Smith')).toBeInTheDocument()
      expect(screen.getByText('Bob Jones')).toBeInTheDocument()
    })

    it('renders all data table columns', () => {
      mockUseTempData.mockReturnValue(makeQuery({ data: MOCK_ROWS }))
      render(<App />)
      expect(screen.getByRole('columnheader', { name: 'Rank' })).toBeInTheDocument()
      expect(screen.getByRole('columnheader', { name: 'Bowler' })).toBeInTheDocument()
      expect(screen.getByRole('columnheader', { name: 'Division' })).toBeInTheDocument()
      expect(screen.getByRole('columnheader', { name: 'City' })).toBeInTheDocument()
      expect(screen.getByRole('columnheader', { name: 'Score' })).toBeInTheDocument()
    })

    it('fires a success toast when data loads', () => {
      mockUseTempData.mockReturnValue(makeQuery({ isSuccess: true, dataUpdatedAt: 12345 }))
      render(<App />)
      expect(mockToastSuccess).toHaveBeenCalledWith('Data loaded successfully')
    })

    it('does not fire a success toast in the idle state', () => {
      render(<App />)
      expect(mockToastSuccess).not.toHaveBeenCalled()
    })
  })

  describe('Error Banner card', () => {
    it('renders the Trigger 500 Error button in default state', () => {
      render(<App />)
      expect(screen.getByRole('button', { name: 'Trigger 500 Error' })).toBeInTheDocument()
    })

    it('shows Loading… on the error button while fetching', () => {
      mockUseTempError.mockReturnValue(makeQuery<TempErrorQuery>({ isFetching: true }))
      render(<App />)
      const buttons = screen.getAllByRole('button', { name: 'Loading…' })
      expect(buttons).toHaveLength(1)
    })

    it('calls refetch when Trigger 500 Error is clicked', async () => {
      const refetch = vi.fn()
      mockUseTempError.mockReturnValue(makeQuery<TempErrorQuery>({ refetch }))
      render(<App />)
      await userEvent.click(screen.getByRole('button', { name: 'Trigger 500 Error' }))
      expect(refetch).toHaveBeenCalledOnce()
    })

    it('shows an error banner when the error query fails', () => {
      mockUseTempError.mockReturnValue(
        makeQuery<TempErrorQuery>({ isError: true, error: new Error('Server exploded') }),
      )
      render(<App />)
      expect(screen.getByRole('alert')).toBeInTheDocument()
      expect(screen.getByText('Server exploded')).toBeInTheDocument()
    })
  })
})
