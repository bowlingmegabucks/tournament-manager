import { describe, it, expect } from 'vitest';
import request from 'supertest';
import app from '../app';

const V1 = { 'x-api-version': '1' };

describe('GET /temp/data', () => {
  it('returns 200', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    expect(res.status).toBe(200);
  });

  it('returns a data array with 10 items', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    expect(Array.isArray(res.body.data)).toBe(true);
    expect(res.body.data).toHaveLength(10);
  });

  it('each item has rank, bowler, division, city, score', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    for (const row of res.body.data) {
      expect(row).toHaveProperty('rank');
      expect(row).toHaveProperty('bowler');
      expect(row).toHaveProperty('division');
      expect(row).toHaveProperty('city');
      expect(row).toHaveProperty('score');
    }
  });

  it('ranks are 1-10 in order', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    const ranks = res.body.data.map((r: { rank: number }) => r.rank);
    expect(ranks).toEqual([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
  });

  it('score is a number between 200 and 299', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    for (const row of res.body.data) {
      expect(typeof row.score).toBe('number');
      expect(row.score).toBeGreaterThanOrEqual(200);
      expect(row.score).toBeLessThanOrEqual(299);
    }
  });

  it('bowler is a non-empty string', async () => {
    const res = await request(app).get('/temp/data').set(V1);
    for (const row of res.body.data) {
      expect(typeof row.bowler).toBe('string');
      expect(row.bowler.length).toBeGreaterThan(0);
    }
  });
});

describe('GET /temp/error', () => {
  it('returns 500', async () => {
    const res = await request(app).get('/temp/error').set(V1);
    expect(res.status).toBe(500);
  });

  it('returns the standard error envelope', async () => {
    const res = await request(app).get('/temp/error').set(V1);
    expect(res.body).toHaveProperty('error');
    expect(res.body.error).toHaveProperty('code');
    expect(res.body.error).toHaveProperty('message');
  });

  it('error code is INTERNAL_ERROR', async () => {
    const res = await request(app).get('/temp/error').set(V1);
    expect(res.body.error.code).toBe('INTERNAL_ERROR');
  });
});
