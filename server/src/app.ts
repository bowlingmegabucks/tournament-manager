import express from 'express';
import helmet from 'helmet';
import cors from 'cors';
import morgan from 'morgan';
import rateLimit from 'express-rate-limit';
import { errorHandler } from './middleware/errorHandler';
import { requireApiVersion } from './middleware/apiVersion';
import { apiError } from './lib/errors';
import tempRoutes from './routes/temp';

const app = express();

const allowedOrigins = process.env.CORS_ORIGINS
  ? process.env.CORS_ORIGINS.split(',').map((o) => o.trim())
  : ['http://localhost:5173'];

app.use(helmet());
app.use(
  cors({
    origin: (origin, callback) => {
      // Allow server-to-server / same-origin requests (no Origin header)
      if (!origin) return callback(null, true);
      if (allowedOrigins.includes(origin)) return callback(null, true);
      callback(new Error(`CORS: origin '${origin}' not allowed`));
    },
    credentials: true,
  }),
);
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

app.use('/temp', tempRoutes);

app.use((_req, res) => {
  res.status(404).json(apiError('NOT_FOUND', 'Route not found'));
});

app.use(errorHandler);

export default app;
