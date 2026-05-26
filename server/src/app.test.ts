import { describe, it, expect, vi } from 'vitest';
import request from 'supertest';

vi.mock('./db/models/Tournament', () => ({ default: {} }));

import app from './app';

describe('GET /health', () => {
  it('returns 200 with status ok', async () => {
    const res = await request(app).get('/health').set('x-api-version', '1');
    expect(res.status).toBe(200);
    expect(res.body).toEqual({ status: 'ok' });
  });

  it('status value is the string "ok", not empty', async () => {
    const res = await request(app).get('/health').set('x-api-version', '1');
    expect(res.body.status).toBe('ok');
  });
});

describe('404 handler', () => {
  it('returns 404 for unknown routes', async () => {
    const res = await request(app)
      .get('/does-not-exist')
      .set('x-api-version', '1');
    expect(res.status).toBe(404);
  });

  it('returns NOT_FOUND error code', async () => {
    const res = await request(app)
      .get('/does-not-exist')
      .set('x-api-version', '1');
    expect(res.body.error.code).toBe('NOT_FOUND');
  });

  it('returns a non-empty error message', async () => {
    const res = await request(app)
      .get('/does-not-exist')
      .set('x-api-version', '1');
    expect(res.body.error.message).toBeTruthy();
  });
});

describe('apiVersion middleware integration', () => {
  it('rejects requests with unsupported x-api-version before hitting routes', async () => {
    const res = await request(app).get('/health').set('x-api-version', '2');
    expect(res.status).toBe(400);
    expect(res.body.error.code).toBe('UNSUPPORTED_VERSION');
  });

  it('accepts requests without x-api-version header (defaults to 1)', async () => {
    const res = await request(app).get('/health');
    expect(res.status).toBe(200);
  });
});

describe('CORS policy', () => {
  it('allows requests from localhost:5173 (default dev origin)', async () => {
    const res = await request(app)
      .get('/health')
      .set('Origin', 'http://localhost:5173');
    expect(res.headers['access-control-allow-origin']).toBe(
      'http://localhost:5173',
    );
  });

  it('blocks requests from untrusted origins', async () => {
    const res = await request(app)
      .get('/health')
      .set('Origin', 'https://evil.example.com');
    expect(res.headers['access-control-allow-origin']).toBeUndefined();
  });

  it('allows server-to-server requests with no Origin header', async () => {
    const res = await request(app).get('/health');
    expect(res.status).toBe(200);
  });

  it('sets credentials header for allowed origins', async () => {
    const res = await request(app)
      .get('/health')
      .set('Origin', 'http://localhost:5173');
    expect(res.headers['access-control-allow-credentials']).toBe('true');
  });
});
