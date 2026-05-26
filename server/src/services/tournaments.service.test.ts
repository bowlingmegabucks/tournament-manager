import { describe, it, expect, vi, beforeEach } from 'vitest';
import { listTournaments, getTournament, updateTournament } from './tournaments.service';

vi.mock('../db/models/Tournament', () => ({
  default: {
    tournament: {
      findMany: vi.fn(),
      findUnique: vi.fn(),
      update: vi.fn(),
    },
    registration: {
      count: vi.fn(),
    },
  },
}));

import prisma from '../db/models/Tournament';

const mockTournament = {
  id: 'uuid-1',
  name: 'Fall Classic 2025',
  start: new Date('2025-10-10T00:00:00.000Z'),
  end: new Date('2025-10-12T00:00:00.000Z'),
  entryFee: { toNumber: () => 50.0 },
  games: 6,
  finalsRatio: { toNumber: () => 7.0 },
  cashRatio: { toNumber: () => 5.0 },
  superSweeperCashRatio: { toNumber: () => 4.0 },
  bowlingCenter: 'Bowlero Tucson',
  completed: false,
};

describe('listTournaments', () => {
  beforeEach(() => vi.clearAllMocks());

  it('returns tournaments sorted by start desc', async () => {
    vi.mocked(prisma.tournament.findMany).mockResolvedValue([mockTournament] as never);
    const result = await listTournaments();
    expect(prisma.tournament.findMany).toHaveBeenCalledWith({ orderBy: { start: 'desc' } });
    expect(result).toHaveLength(1);
  });

  it('formats start and end as ISO date strings', async () => {
    vi.mocked(prisma.tournament.findMany).mockResolvedValue([mockTournament] as never);
    const [t] = await listTournaments();
    expect(t.start).toBe('2025-10-10');
    expect(t.end).toBe('2025-10-12');
  });

  it('maps all summary fields', async () => {
    vi.mocked(prisma.tournament.findMany).mockResolvedValue([mockTournament] as never);
    const [t] = await listTournaments();
    expect(t).toEqual({
      id: 'uuid-1',
      name: 'Fall Classic 2025',
      start: '2025-10-10',
      end: '2025-10-12',
      bowlingCenter: 'Bowlero Tucson',
      completed: false,
    });
  });

  it('returns empty array when no tournaments', async () => {
    vi.mocked(prisma.tournament.findMany).mockResolvedValue([]);
    const result = await listTournaments();
    expect(result).toEqual([]);
  });
});

describe('getTournament', () => {
  beforeEach(() => vi.clearAllMocks());

  const mockWithCount = {
    ...mockTournament,
    _count: { divisions: 2, squads: 3 },
  };

  it('returns null when tournament not found', async () => {
    vi.mocked(prisma.tournament.findUnique).mockResolvedValue(null);
    const result = await getTournament('missing-id');
    expect(result).toBeNull();
  });

  it('returns full tournament detail with counts', async () => {
    vi.mocked(prisma.tournament.findUnique).mockResolvedValue(mockWithCount as never);
    vi.mocked(prisma.registration.count).mockResolvedValue(48);
    const result = await getTournament('uuid-1');
    expect(result).toEqual({
      id: 'uuid-1',
      name: 'Fall Classic 2025',
      start: '2025-10-10',
      end: '2025-10-12',
      bowlingCenter: 'Bowlero Tucson',
      completed: false,
      entryFee: 50.0,
      games: 6,
      finalsRatio: 7.0,
      cashRatio: 5.0,
      superSweeperCashRatio: 4.0,
      _counts: { divisions: 2, squads: 3, registrations: 48 },
    });
  });

  it('counts registrations via division.tournamentId', async () => {
    vi.mocked(prisma.tournament.findUnique).mockResolvedValue(mockWithCount as never);
    vi.mocked(prisma.registration.count).mockResolvedValue(0);
    await getTournament('uuid-1');
    expect(prisma.registration.count).toHaveBeenCalledWith({
      where: { division: { tournamentId: 'uuid-1' } },
    });
  });

  it('skips registration count query when tournament is null', async () => {
    vi.mocked(prisma.tournament.findUnique).mockResolvedValue(null);
    await getTournament('missing');
    expect(prisma.registration.count).not.toHaveBeenCalled();
  });
});

describe('updateTournament', () => {
  beforeEach(() => vi.clearAllMocks());

  const updateData = {
    name: 'Updated Name',
    start: '2025-10-10',
    end: '2025-10-12',
    bowlingCenter: 'Bowlero Tucson',
    entryFee: 55.0,
    games: 6,
    finalsRatio: 7.0,
    cashRatio: 5.0,
    superSweeperCashRatio: 4.0,
    completed: false,
  };

  it('returns null when tournament does not exist', async () => {
    vi.mocked(prisma.tournament.findUnique).mockResolvedValue(null);
    const result = await updateTournament('missing-id', updateData);
    expect(result).toBeNull();
    expect(prisma.tournament.update).not.toHaveBeenCalled();
  });

  it('calls update with correct mapped fields', async () => {
    const mockWithCount = { ...mockTournament, _count: { divisions: 0, squads: 0 } };
    vi.mocked(prisma.tournament.findUnique)
      .mockResolvedValueOnce(mockTournament as never)
      .mockResolvedValueOnce(mockWithCount as never);
    vi.mocked(prisma.tournament.update).mockResolvedValue(mockTournament as never);
    vi.mocked(prisma.registration.count).mockResolvedValue(0);

    await updateTournament('uuid-1', updateData);

    expect(prisma.tournament.update).toHaveBeenCalledWith({
      where: { id: 'uuid-1' },
      data: {
        name: 'Updated Name',
        start: new Date('2025-10-10T00:00:00.000Z'),
        end: new Date('2025-10-12T00:00:00.000Z'),
        bowlingCenter: 'Bowlero Tucson',
        entryFee: 55.0,
        games: 6,
        finalsRatio: 7.0,
        cashRatio: 5.0,
        superSweeperCashRatio: 4.0,
        completed: false,
      },
    });
  });

  it('returns updated tournament detail on success', async () => {
    const updated = { ...mockTournament, name: 'Updated Name', _count: { divisions: 0, squads: 0 } };
    vi.mocked(prisma.tournament.findUnique)
      .mockResolvedValueOnce(mockTournament as never)
      .mockResolvedValueOnce(updated as never);
    vi.mocked(prisma.tournament.update).mockResolvedValue(mockTournament as never);
    vi.mocked(prisma.registration.count).mockResolvedValue(0);

    const result = await updateTournament('uuid-1', { ...updateData, name: 'Updated Name' });
    expect(result?.name).toBe('Updated Name');
  });
});
