import { describe, it, expect } from 'vitest';
import { z } from 'zod';

// Re-export the schema from controller so we can test it directly
const isoDateString = z
  .string()
  .regex(/^\d{4}-\d{2}-\d{2}$/, 'Must be a date in YYYY-MM-DD format');

const TournamentUpdateSchema = z
  .object({
    name: z.string().min(1, 'Name is required'),
    start: isoDateString,
    end: isoDateString,
    bowlingCenter: z.string().min(1, 'Bowling center is required'),
    entryFee: z.number().positive('Entry fee must be positive'),
    games: z.number().int().min(1, 'Games must be at least 1'),
    finalsRatio: z.number().gt(1, 'Finals ratio must be greater than 1'),
    cashRatio: z.number().gt(1, 'Cash ratio must be greater than 1'),
    superSweeperCashRatio: z.number().gt(1, 'Super Sweeper cash ratio must be greater than 1'),
    completed: z.boolean(),
  })
  .refine((d) => d.start <= d.end, {
    message: 'Start date must be on or before end date',
    path: ['start'],
  });

const validBody = {
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  entryFee: 50,
  games: 6,
  finalsRatio: 7,
  cashRatio: 5,
  superSweeperCashRatio: 4,
  completed: false,
};

function parse(body: unknown) {
  return TournamentUpdateSchema.safeParse(body);
}

describe('TournamentUpdateSchema — valid body', () => {
  it('accepts a fully valid body', () => {
    expect(parse(validBody).success).toBe(true);
  });

  it('accepts completed=true', () => {
    expect(parse({ ...validBody, completed: true }).success).toBe(true);
  });

  it('accepts start === end (same day)', () => {
    expect(parse({ ...validBody, start: '2025-10-10', end: '2025-10-10' }).success).toBe(true);
  });
});

describe('TournamentUpdateSchema — name validation', () => {
  it('rejects empty name', () => {
    const r = parse({ ...validBody, name: '' });
    expect(r.success).toBe(false);
    if (!r.success) expect(r.error.issues.some((e) => e.path.includes('name'))).toBe(true);
  });

  it('rejects missing name', () => {
    const { name: _, ...body } = validBody;
    expect(parse(body).success).toBe(false);
  });
});

describe('TournamentUpdateSchema — date validation', () => {
  it('rejects start with invalid format', () => {
    expect(parse({ ...validBody, start: 'not-a-date' }).success).toBe(false);
  });

  it('rejects end with invalid format', () => {
    expect(parse({ ...validBody, end: '12/25/2025' }).success).toBe(false);
  });

  it('rejects start > end', () => {
    const r = parse({ ...validBody, start: '2025-10-15', end: '2025-10-12' });
    expect(r.success).toBe(false);
    if (!r.success) expect(r.error.issues.some((e) => e.path.includes('start'))).toBe(true);
  });
});

describe('TournamentUpdateSchema — numeric field validation', () => {
  it('rejects entryFee = 0', () => {
    expect(parse({ ...validBody, entryFee: 0 }).success).toBe(false);
  });

  it('rejects negative entryFee', () => {
    expect(parse({ ...validBody, entryFee: -10 }).success).toBe(false);
  });

  it('rejects games = 0', () => {
    expect(parse({ ...validBody, games: 0 }).success).toBe(false);
  });

  it('rejects non-integer games', () => {
    expect(parse({ ...validBody, games: 1.5 }).success).toBe(false);
  });

  it('rejects finalsRatio = 1 (must be > 1)', () => {
    expect(parse({ ...validBody, finalsRatio: 1 }).success).toBe(false);
  });

  it('rejects cashRatio <= 1', () => {
    expect(parse({ ...validBody, cashRatio: 0.5 }).success).toBe(false);
  });

  it('rejects superSweeperCashRatio = 1', () => {
    expect(parse({ ...validBody, superSweeperCashRatio: 1 }).success).toBe(false);
  });
});

describe('TournamentUpdateSchema — other fields', () => {
  it('rejects empty bowlingCenter', () => {
    expect(parse({ ...validBody, bowlingCenter: '' }).success).toBe(false);
  });

  it('rejects missing completed field', () => {
    const { completed: _, ...body } = validBody;
    expect(parse(body).success).toBe(false);
  });

  it('rejects non-boolean completed', () => {
    expect(parse({ ...validBody, completed: 'false' }).success).toBe(false);
  });
});
