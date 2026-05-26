import { NavLink } from 'react-router-dom'
import { cn } from '@/lib/utils'

interface SidebarSection {
  label: string
  items: { label: string; path: string }[]
}

interface TournamentDetailSidebarProps {
  readonly tournamentId: string
}

function getSections(id: string): SidebarSection[] {
  return [
    {
      label: 'Setup',
      items: [
        { label: 'Overview', path: `/tournaments/${id}/overview` },
        { label: 'Divisions', path: `/tournaments/${id}/divisions` },
        { label: 'Squads', path: `/tournaments/${id}/squads` },
      ],
    },
    {
      label: 'During Event',
      items: [
        { label: 'Registrations', path: `/tournaments/${id}/registrations` },
        { label: 'Lane Assignments', path: `/tournaments/${id}/lanes` },
      ],
    },
    {
      label: 'After Event',
      items: [
        { label: 'Results', path: `/tournaments/${id}/results` },
        { label: 'Sweepers', path: `/tournaments/${id}/sweepers` },
      ],
    },
  ]
}

export function TournamentDetailSidebar({ tournamentId }: TournamentDetailSidebarProps) {
  const sections = getSections(tournamentId)

  return (
    <nav aria-label="Tournament sections" className="w-full md:w-48 md:shrink-0">
      {/* Desktop sidebar */}
      <div className="hidden md:block w-44 shrink-0 border-r border-border py-4">
        {sections.map((section) => (
          <div key={section.label}>
            <p className="px-5 pb-1 pt-3 text-[10px] font-semibold uppercase tracking-widest text-text-muted">
              {section.label}
            </p>
            {section.items.map((item) => (
              <NavLink
                key={item.path}
                to={item.path}
                className={({ isActive }) =>
                  cn(
                    'block px-5 py-[9px] text-sm text-text-body transition-colors hover:bg-surface-subtle',
                    isActive &&
                      'border-r-[3px] border-interactive bg-interactive/10 font-semibold text-text-primary',
                  )
                }
              >
                {item.label}
              </NavLink>
            ))}
          </div>
        ))}
      </div>

      {/* Mobile: horizontal scrollable strip */}
      <div className="flex md:hidden overflow-x-auto gap-1 pb-2 mb-4 border-b border-border">
        {sections.flatMap((section) =>
          section.items.map((item) => (
            <NavLink
              key={item.path}
              to={item.path}
              className={({ isActive }) =>
                cn(
                  'shrink-0 rounded px-3 py-1.5 text-sm text-text-body whitespace-nowrap transition-colors hover:bg-surface-subtle',
                  isActive && 'border-b-2 border-interactive font-semibold text-text-primary',
                )
              }
            >
              {item.label}
            </NavLink>
          )),
        )}
      </div>
    </nav>
  )
}
