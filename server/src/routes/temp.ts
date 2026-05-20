import { Router } from 'express';
import asyncHandler from 'express-async-handler';

const router = Router();

const BOWLER_NAMES = [
  'Pete Weber', 'Walter Ray Williams Jr.', 'Norm Duke', 'Jason Belmonte',
  'Amleto Monacelli', 'Marshall Holman', 'Dick Weber', 'Don Carter',
  'Earl Anthony', 'Chris Barnes', 'Tommy Jones', 'Parker Bohn III',
];

const DIVISIONS = ['Open', 'Senior', 'Ladies', 'Youth'];
const CITIES = ['Akron', 'Las Vegas', 'Reno', 'Toledo', 'Buffalo', 'Detroit'];

router.get(
  '/data',
  asyncHandler(async (_req, res) => {
    const rows = Array.from({ length: 10 }, (_, i) => ({
      rank: i + 1,
      bowler: BOWLER_NAMES[i % BOWLER_NAMES.length],
      division: DIVISIONS[Math.floor(Math.random() * DIVISIONS.length)],
      city: CITIES[Math.floor(Math.random() * CITIES.length)],
      score: Math.floor(Math.random() * 100) + 200,
    }));
    res.json({ data: rows });
  }),
);

router.get(
  '/error',
  asyncHandler(async (_req, _res) => {
    throw new Error('Intentional 500 for error-handling demo');
  }),
);

export default router;
