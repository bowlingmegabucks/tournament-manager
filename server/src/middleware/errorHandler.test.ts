import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import type { Request, Response, NextFunction } from 'express';
import { errorHandler } from './errorHandler';

function makeMocks() {
  const req = {} as Request;
  const json = vi.fn();
  const status = vi.fn().mockReturnValue({ json });
  const res = { status, json } as unknown as Response;
  const next = vi.fn() as unknown as NextFunction;
  return { req, res, next, status, json };
}

describe('errorHandler', () => {
  beforeEach(() => {
    vi.spyOn(console, 'error').mockImplementation(() => {});
  });

  afterEach(() => {
    vi.restoreAllMocks();
  });

  it('logs the error to console.error', () => {
    const { req, res, next } = makeMocks();
    const err = new Error('boom');
    errorHandler(err, req, res, next);
    expect(console.error).toHaveBeenCalledWith(err);
  });

  it('responds with 500 and INTERNAL_ERROR code', () => {
    const { req, res, next, status, json } = makeMocks();
    errorHandler(new Error('boom'), req, res, next);
    expect(status).toHaveBeenCalledWith(500);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({
        error: expect.objectContaining({ code: 'INTERNAL_ERROR' }),
      }),
    );
  });

  it('does not expose the internal error message to the caller', () => {
    const { req, res, next, json } = makeMocks();
    errorHandler(new Error('sensitive db details'), req, res, next);
    const body = json.mock.calls[0][0];
    expect(JSON.stringify(body)).not.toContain('sensitive db details');
  });
});
