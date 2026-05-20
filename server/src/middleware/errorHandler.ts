import { Request, Response, NextFunction } from 'express';
import { apiError } from '../lib/errors';

export function errorHandler(
  err: Error,
  _req: Request,
  res: Response,
  _next: NextFunction,
) {
  console.error(err);

  res.status(500).json(apiError('INTERNAL_ERROR', 'Something went wrong'));
}
