import express from 'express';
import helmet from 'helmet';
import cors from 'cors';
import morgan from 'morgan';
import rateLimit from 'express-rate-limit';
import { errorHandler } from './middleware/errorHandler';
import { requireApiVersion } from './middleware/apiVersion';
import { apiError } from './lib/errors';
// import authRoutes from './routes/auth';
// import tournamentRoutes from './routes/tournaments';

const app = express();

app.use(helmet());
app.use(cors());
app.use(morgan('dev'));
app.use(express.json());

const authLimiter = rateLimit({ windowMs: 15 * 60 * 1000, max: 20 });
const generalLimiter = rateLimit({ windowMs: 15 * 60 * 1000, max: 300 });

app.use(requireApiVersion);

app.use('/auth', authLimiter);
app.use('/', generalLimiter);

app.get('/health', (_req, res) => {
  res.json({ status: 'ok' });
});

// app.use('/auth', authRoutes);
// app.use('/tournaments', tournamentRoutes);

app.use((_req, res) => {
  res.status(404).json(apiError('NOT_FOUND', 'Route not found'));
});

app.use(errorHandler);

export default app;
