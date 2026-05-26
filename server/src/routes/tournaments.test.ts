import { describe, it, expect, vi, beforeEach } from 'vitest';
import request from 'supertest';
import app from '../app';

vi.mock('../services/tournaments.service', () => ({
  listTournaments: vi.fn(),
  getTournament: vi.fn(),
  updateTournament: vi.fn(),
}));

import * as service from '../services/tournaments.service';

const API_VERSION = { 'x-api-version': '1' };

const mockSummary = {
  id: 'uuid-1',
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  completed: false,
};

const mockDetail = {
  ...mockSummary,
  entryFee: 50.0,
  games: 6,
  finalsRatio: 7.0,
  cashRatio: 5.0,
  superSweeperCashRatio: 4.0,
  _counts: { divisions: 2, squads: 3, registrations: 48 },
};

const validBody = {
  name: 'Fall Classic 2025',
  start: '2025-10-10',
  end: '2025-10-12',
  bowlingCenter: 'Bowlero Tucson',
  entryFee: 50.0,
  games: 6,
  finalsRatio: 7.0,
  cashRatio: 5.0,
  superSweeperCashRatio: 4.0,
  completed: false,
};

describe('GET /tournaments', () => {
  beforeEach(() => vi.clearAllMocks());

  it('returns 200 with an array', async () => {
    vi.mocked(service.listTournaments).mockResolvedValue([mockSummary]);
    const res = await request(app).get('/tournaments').set(API_VERSION);
    expect(res.status).toBe(200);
    expect(Array.isArray(res.body)).toBe(true);
  });

  it('returns the tournament data', async () => {
    vi.mocked(service.listTournaments).mockResolvedValue([mockSummary]);
    const res = await request(app).get('/tournaments').set(API_VERSION);
    expect(res.body[0]).toMatchObject(mockSummary);
  });

  it('returns empty array when no tournaments', async () => {
    vi.mocked(service.listTournaments).mockResolvedValue([]);
    const res = await request(app).get('/tournaments').set(API_VERSION);
    expect(res.body).toEqual([]);
  });
});

describe('GET /tournaments/:id', () => {
  beforeEach(() => vi.clearAllMocks());

  it('returns 200 with full detail', async () => {
    vi.mocked(service.getTournament).mockResolvedValue(mockDetail);
    const res = await request(app).get('/tournaments/uuid-1').set(API_VERSION);
    expect(res.status).toBe(200);
    expect(res.body).toMatchObject(mockDetail);
  });

  it('returns 404 with TOURNAMENT_NOT_FOUND when not found', async () => {
    vi.mocked(service.getTournament).mockResolvedValue(null);
    const res = await request(app).get('/tournaments/missing').set(API_VERSION);
    expect(res.status).toBe(404);
    expect(res.body.error.code).toBe('TOURNAMENT_NOT_FOUND');
  });

  it('includes _counts in response', async () => {
    vi.mocked(service.getTournament).mockResolvedValue(mockDetail);
    const res = await request(app).get('/tournaments/uuid-1').set(API_VERSION);
    expect(res.body._counts).toEqual({ divisions: 2, squads: 3, registrations: 48 });
  });
});

describe('PUT /tournaments/:id', () => {
  beforeEach(() => vi.clearAllMocks());

  it('returns 200 with updated tournament on success', async () => {
    vi.mocked(service.updateTournament).mockResolvedValue(mockDetail);
    const res = await request(app)
      .put('/tournaments/uuid-1')
      .set(API_VERSION)
      .send(validBody);
    expect(res.status).toBe(200);
    expect(res.body.name).toBe('Fall Classic 2025');
  });

  it('returns 404 with TOURNAMENT_NOT_FOUND when not found', async () => {
    vi.mocked(service.updateTournament).mockResolvedValue(null);
    const res = await request(app)
      .put('/tournaments/missing')
      .set(API_VERSION)
      .send(validBody);
    expect(res.status).toBe(404);
    expect(res.body.error.code).toBe('TOURNAMENT_NOT_FOUND');
  });

  it('returns 422 with VALIDATION_ERROR when body is invalid', async () => {
    const res = await request(app)
      .put('/tournaments/uuid-1')
      .set(API_VERSION)
      .send({ ...validBody, name: '' });
    expect(res.status).toBe(422);
    expect(res.body.error.code).toBe('VALIDATION_ERROR');
  });

  it('returns field-level errors on 422', async () => {
    const res = await request(app)
      .put('/tournaments/uuid-1')
      .set(API_VERSION)
      .send({ ...validBody, name: '', entryFee: -1 });
    expect(res.body.error.details).toBeInstanceOf(Array);
    expect(res.body.error.details.length).toBeGreaterThan(0);
  });

  it('rejects start > end with 422', async () => {
    const res = await request(app)
      .put('/tournaments/uuid-1')
      .set(API_VERSION)
      .send({ ...validBody, start: '2025-10-15', end: '2025-10-12' });
    expect(res.status).toBe(422);
  });
});
