import { Request, Response, NextFunction } from 'express';
import { apiError } from '../lib/errors';

export function errorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
    console.error(err);

    res.status(500).json(apiError('INTERNAL_ERROR', 'Something went wrong'));
}