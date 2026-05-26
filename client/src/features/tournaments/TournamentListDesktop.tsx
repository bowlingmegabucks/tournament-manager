import { useNavigate } from 'react-router-dom'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Badge } from '@/components/ui/badge'
import { formatDate } from '@/lib/format'
import type { TournamentSummary } from '@/api/tournaments'

interface TournamentListDesktopProps {
  readonly tournaments: TournamentSummary[]
}

export function TournamentListDesktop({ tournaments }: TournamentListDesktopProps) {
  const navigate = useNavigate()

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Name</TableHead>
          <TableHead>Start</TableHead>
          <TableHead>End</TableHead>
          <TableHead>Bowling Center</TableHead>
          <TableHead>Status</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {tournaments.map((t) => (
          <TableRow
            key={t.id}
            className="cursor-pointer"
            onClick={() => navigate(`/tournaments/${t.id}`)}
          >
            <TableCell className="font-semibold text-text-primary">
              <button
                type="button"
                className="text-left text-interactive hover:underline focus-visible:outline-none focus-visible:underline"
                onClick={(e) => {
                  e.stopPropagation()
                  navigate(`/tournaments/${t.id}`)
                }}
              >
                {t.name}
              </button>
            </TableCell>
            <TableCell>{formatDate(t.start)}</TableCell>
            <TableCell>{formatDate(t.end)}</TableCell>
            <TableCell>{t.bowlingCenter}</TableCell>
            <TableCell>
              <Badge variant={t.completed ? 'neutral' : 'success'}>
                {t.completed ? 'Completed' : 'Active'}
              </Badge>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
