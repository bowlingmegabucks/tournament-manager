import { describe, it, expect, vi } from 'vitest';
import type { Request, Response, NextFunction } from 'express';
import { requireApiVersion } from './apiVersion';

function makeMocks(headers: Record<string, string> = {}) {
  const req = { headers } as unknown as Request;
  const json = vi.fn();
  const status = vi.fn().mockReturnValue({ json });
  const res = { status, json } as unknown as Response;
  const next = vi.fn() as unknown as NextFunction;
  return { req, res, next, status, json };
}

describe('requireApiVersion', () => {
  it('calls next when x-api-version is "1"', () => {
    const { req, res, next } = makeMocks({ 'x-api-version': '1' });
    requireApiVersion(req, res, next);
    expect(next).toHaveBeenCalledOnce();
    expect(next).toHaveBeenCalledWith();
  });

  it('defaults to version 1 when header is absent', () => {
    const { req, res, next } = makeMocks();
    requireApiVersion(req, res, next);
    expect(next).toHaveBeenCalledOnce();
  });

  it('accepts version 1 when x-api-version is a repeated header array', () => {
    const { req, res, next } = makeMocks({ 'x-api-version': ['1', '1'] as unknown as string });
    requireApiVersion(req, res, next);
    expect(next).toHaveBeenCalledOnce();
  });

  it('rejects an unsupported version with 400', () => {
    const { req, res, next, status, json } = makeMocks({
      'x-api-version': '2',
    });
    requireApiVersion(req, res, next);
    expect(next).not.toHaveBeenCalled();
    expect(status).toHaveBeenCalledWith(400);
    expect(json).toHaveBeenCalledWith(
      expect.objectContaining({
        error: expect.objectContaining({
          code: 'UNSUPPORTED_VERSION',
          message: 'x-api-version header must be "1"',
        }),
      }),
    );
  });
});
