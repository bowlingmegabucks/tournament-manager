import { Request, Response, NextFunction } from 'express';
import { apiError } from '../lib/errors';

export function requireApiVersion(
  req: Request,
  res: Response,
  next: NextFunction,
) {
  const raw = req.headers['x-api-version'];
  const version = Array.isArray(raw) ? raw[0] : (raw ?? '1');
  if (version !== '1') {
    return res
      .status(400)
      .json(
        apiError('UNSUPPORTED_VERSION', 'x-api-version header must be "1"'),
      );
  }
  next();
}
