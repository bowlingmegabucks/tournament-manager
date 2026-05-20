import { Request, Response, NextFunction } from 'express';
import jwt from 'jsonwebtoken';
import { apiError } from '../lib/errors';

type Role = 'director' | 'staff' | 'viewer';

interface JwtPayload {
  sub: string;
  role: Role;
}

declare global {
  namespace Express {
    interface Request {
      user?: { id: string; role: Role };
    }
  }
}

export function authenticate(req: Request, res: Response, next: NextFunction) {
  const header = req.headers.authorization;
  if (!header?.startsWith('Bearer ')) {
    return res
      .status(401)
      .json(apiError('UNAUTHORIZED', 'Missing or malformed token'));
  }

  const token = header.slice(7);
  const secret = process.env.JWT_SECRET;
  if (!secret) throw new Error('JWT_SECRET is not set');

  try {
    const payload = jwt.verify(token, secret) as JwtPayload;
    req.user = { id: payload.sub, role: payload.role };
    next();
  } catch {
    return res
      .status(401)
      .json(apiError('UNAUTHORIZED', 'Invalid or expired token'));
  }
}

export function requireRole(...roles: Role[]) {
  return (req: Request, res: Response, next: NextFunction) => {
    if (!req.user || !roles.includes(req.user.role)) {
      return res
        .status(403)
        .json(apiError('FORBIDDEN', 'Insufficient permissions'));
    }
    next();
  };
}
