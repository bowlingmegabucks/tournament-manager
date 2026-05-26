import { Decimal } from '@prisma/client/runtime/library';
import prisma from '../db/models/Tournament';

export type TournamentSummary = {
  id: string;
  name: string;
  start: string;
  end: string;
  bowlingCenter: string;
  completed: boolean;
};

export type TournamentDetail = TournamentSummary & {
  entryFee: number;
  games: number;
  finalsRatio: number;
  cashRatio: number;
  superSweeperCashRatio: number;
  _counts: { divisions: number; squads: number; registrations: number };
};

export type TournamentUpdateData = {
  name: string;
  start: string;
  end: string;
  bowlingCenter: string;
  entryFee: number;
  games: number;
  finalsRatio: number;
  cashRatio: number;
  superSweeperCashRatio: number;
  completed: boolean;
};

function toDateString(date: Date): string {
  return date.toISOString().split('T')[0];
}

function toDecimal(d: Decimal): number {
  return d.toNumber();
}

export async function listTournaments(): Promise<TournamentSummary[]> {
  const rows = await prisma.tournament.findMany({
    orderBy: { start: 'desc' },
  });
  return rows.map((t) => ({
    id: t.id,
    name: t.name,
    start: toDateString(t.start),
    end: toDateString(t.end),
    bowlingCenter: t.bowlingCenter,
    completed: t.completed,
  }));
}

export async function getTournament(id: string): Promise<TournamentDetail | null> {
  const t = await prisma.tournament.findUnique({
    where: { id },
    include: {
      _count: { select: { divisions: true, squads: true } },
    },
  });

  if (!t) return null;

  const registrationsCount = await prisma.registration.count({
    where: { division: { tournamentId: id } },
  });

  return {
    id: t.id,
    name: t.name,
    start: toDateString(t.start),
    end: toDateString(t.end),
    bowlingCenter: t.bowlingCenter,
    completed: t.completed,
    entryFee: toDecimal(t.entryFee),
    games: t.games,
    finalsRatio: toDecimal(t.finalsRatio),
    cashRatio: toDecimal(t.cashRatio),
    superSweeperCashRatio: toDecimal(t.superSweeperCashRatio),
    _counts: {
      divisions: t._count.divisions,
      squads: t._count.squads,
      registrations: registrationsCount,
    },
  };
}

export async function updateTournament(
  id: string,
  data: TournamentUpdateData,
): Promise<TournamentDetail | null> {
  const exists = await prisma.tournament.findUnique({ where: { id } });
  if (!exists) return null;

  await prisma.tournament.update({
    where: { id },
    data: {
      name: data.name,
      start: new Date(data.start + 'T00:00:00.000Z'),
      end: new Date(data.end + 'T00:00:00.000Z'),
      bowlingCenter: data.bowlingCenter,
      entryFee: data.entryFee,
      games: data.games,
      finalsRatio: data.finalsRatio,
      cashRatio: data.cashRatio,
      superSweeperCashRatio: data.superSweeperCashRatio,
      completed: data.completed,
    },
  });

  return getTournament(id);
}
