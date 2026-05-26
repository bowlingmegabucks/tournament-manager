import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter, Route, Routes } from 'react-router-dom'
import { TournamentDetailPage } from './TournamentDetailPage'

vi.mock('@/features/tournaments/TournamentDetailLayout', () => ({
  TournamentDetailLayout: () => <div data-testid="detail-layout" />,
}))

describe('TournamentDetailPage', () => {
  it('renders TournamentDetailLayout', () => {
    render(
      <MemoryRouter initialEntries={['/tournaments/uuid-1/overview']}>
        <Routes>
          <Route path="/tournaments/:id/*" element={<TournamentDetailPage />} />
        </Routes>
      </MemoryRouter>,
    )
    expect(screen.getByTestId('detail-layout')).toBeInTheDocument()
  })
})
