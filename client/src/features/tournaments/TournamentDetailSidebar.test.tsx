import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { TournamentDetailSidebar } from './TournamentDetailSidebar'

function renderSidebar(id = 'uuid-1') {
  return render(
    <MemoryRouter initialEntries={[`/tournaments/${id}/overview`]}>
      <TournamentDetailSidebar tournamentId={id} />
    </MemoryRouter>,
  )
}

describe('TournamentDetailSidebar', () => {
  it('renders all section group labels', () => {
    renderSidebar()
    expect(screen.getAllByText('Setup')).toHaveLength(1)
    expect(screen.getAllByText('During Event')).toHaveLength(1)
    expect(screen.getAllByText('After Event')).toHaveLength(1)
  })

  it('renders Overview nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Overview').length).toBeGreaterThan(0)
  })

  it('renders Divisions nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Divisions').length).toBeGreaterThan(0)
  })

  it('renders Squads nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Squads').length).toBeGreaterThan(0)
  })

  it('renders Registrations nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Registrations').length).toBeGreaterThan(0)
  })

  it('renders Lane Assignments nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Lane Assignments').length).toBeGreaterThan(0)
  })

  it('renders Results nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Results').length).toBeGreaterThan(0)
  })

  it('renders Sweepers nav link', () => {
    renderSidebar()
    expect(screen.getAllByText('Sweepers').length).toBeGreaterThan(0)
  })

  it('links use correct tournament id in href', () => {
    renderSidebar('uuid-42')
    const links = screen.getAllByRole('link', { name: 'Overview' })
    expect(links[0]).toHaveAttribute('href', '/tournaments/uuid-42/overview')
  })
})
