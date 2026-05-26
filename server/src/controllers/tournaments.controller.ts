import { Request, Response } from 'express';
import asyncHandler from 'express-async-handler';
import { z } from 'zod';
import { apiError } from '../lib/errors';
import * as tournamentService from '../services/tournaments.service';

const isoDateString = z
  .string()
  .regex(/^\d{4}-\d{2}-\d{2}$/, 'Must be a date in YYYY-MM-DD format');

const TournamentUpdateSchema = z
  .object({
    name: z.string().min(1, 'Name is required'),
    start: isoDateString,
    end: isoDateString,
    bowlingCenter: z.string().min(1, 'Bowling center is required'),
    entryFee: z.number().positive('Entry fee must be positive'),
    games: z.number().int().min(1, 'Games must be at least 1'),
    finalsRatio: z.number().gt(1, 'Finals ratio must be greater than 1'),
    cashRatio: z.number().gt(1, 'Cash ratio must be greater than 1'),
    superSweeperCashRatio: z.number().gt(1, 'Super Sweeper cash ratio must be greater than 1'),
    completed: z.boolean(),
  })
  .refine((d) => d.start <= d.end, {
    message: 'Start date must be on or before end date',
    path: ['start'],
  });

export const list = asyncHandler(async (_req: Request, res: Response) => {
  const tournaments = await tournamentService.listTournaments();
  res.json(tournaments);
});

export const detail = asyncHandler(async (req: Request, res: Response) => {
  const tournament = await tournamentService.getTournament(req.params.id);
  if (!tournament) {
    res
      .status(404)
      .json(apiError('TOURNAMENT_NOT_FOUND', `Tournament ${req.params.id} not found`));
    return;
  }
  res.json(tournament);
});

export const update = asyncHandler(async (req: Request, res: Response) => {
  const parsed = TournamentUpdateSchema.safeParse(req.body);
  if (!parsed.success) {
    const details = parsed.error.issues.map((e) => ({
      field: e.path.join('.'),
      message: e.message,
    }));
    res.status(422).json(apiError('VALIDATION_ERROR', 'Validation failed', details));
    return;
  }

  const tournament = await tournamentService.updateTournament(req.params.id, parsed.data);
  if (!tournament) {
    res
      .status(404)
      .json(apiError('TOURNAMENT_NOT_FOUND', `Tournament ${req.params.id} not found`));
    return;
  }
  res.json(tournament);
});
