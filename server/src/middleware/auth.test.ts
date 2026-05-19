import { describe, it, expect, vi, beforeEach } from 'vitest';
import type { Request, Response, NextFunction } from 'express';
import jwt from 'jsonwebtoken';
import { authenticate, requireRole } from './auth';

const TEST_SECRET = 'test-secret';

function makeMocks(headers: Record<string, string> = {}) {
  const req = { headers, user: undefined } as unknown as Request;
  const json = vi.fn();
  const status = vi.fn().mockReturnValue({ json });
  const res = { status, json } as unknown as Response;
  const next = vi.fn() as unknown as NextFunction;
  return { req, res, next, status, json };
}

function signToken(payload: object, secret = TEST_SECRET) {
  return jwt.sign(payload, secret, { expiresIn: '1h' });
}

describe('authenticate', () => {
  beforeEach(() => {
    process.env.JWT_SECRET = TEST_SECRET;
  });

  it('calls next and sets req.user for a valid token', () => {
    const token = signToken({ sub: 'user-1', role: 'staff' });
    const { req, res, next } = makeMocks({ authorization: `Bearer ${token}` });
    authenticate(req, res, next);
    expect(next).toHaveBeenCalledOnce();
    expect((req as any).user).toEqual({ id: 'user-1', role: 'staff' });
  });

  it('returns 401 when Authorization header is missing', () => {
    const { req, res, next, status, json } = makeMocks();
    authenticate(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(401);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({ error: expect.objectContaining({ code: 'UNAUTHORIZED', message: 'Missing or malformed token' }) })
    );
  });

  it('returns 401 when Authorization header does not start with "Bearer "', () => {
    const { req, res, next, status, json } = makeMocks({ authorization: 'Basic abc123' });
    authenticate(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(401);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({ error: expect.objectContaining({ message: 'Missing or malformed token' }) })
    );
  });

  it('returns 401 for an expired token', () => {
    const token = jwt.sign({ sub: 'user-1', role: 'staff' }, TEST_SECRET, { expiresIn: -1 });
    const { req, res, next, status, json } = makeMocks({ authorization: `Bearer ${token}` });
    authenticate(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(401);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({ error: expect.objectContaining({ message: 'Invalid or expired token' }) })
    );
  });

  it('returns 401 for a token signed with the wrong secret', () => {
    const token = signToken({ sub: 'user-1', role: 'staff' }, 'wrong-secret');
    const { req, res, next, status, json } = makeMocks({ authorization: `Bearer ${token}` });
    authenticate(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(401);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({ error: expect.objectContaining({ message: 'Invalid or expired token' }) })
    );
  });

  it('throws when JWT_SECRET is not set', () => {
    delete process.env.JWT_SECRET;
    const token = signToken({ sub: 'user-1', role: 'staff' }, TEST_SECRET);
    const { req, res, next } = makeMocks({ authorization: `Bearer ${token}` });
    expect(() => authenticate(req, res, next)).toThrow('JWT_SECRET is not set');
  });
});

describe('requireRole', () => {
  it('calls next when user has a permitted role', () => {
    const { req, res, next } = makeMocks();
    (req as any).user = { id: 'user-1', role: 'director' };
    requireRole('director', 'staff')(req, res, next);
    expect(next).toHaveBeenCalledOnce();
  });

  it('returns 403 when user role is not in the permitted list', () => {
    const { req, res, next, status, json } = makeMocks();
    (req as any).user = { id: 'user-1', role: 'viewer' };
    requireRole('director', 'staff')(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(403);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({ error: expect.objectContaining({ code: 'FORBIDDEN', message: 'Insufficient permissions' }) })
    );
  });

  it('returns 403 when req.user is not set', () => {
    const { req, res, next, status } = makeMocks();
    requireRole('director')(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(403);
  });
});
