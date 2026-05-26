import { PrismaClient } from '@prisma/client';
import { PrismaMariaDb } from '@prisma/adapter-mariadb';

const { DB_HOST = '127.0.0.1', DB_PORT = '13306', DB_NAME, DB_USER, DB_PASSWORD } = process.env;

const adapter = new PrismaMariaDb({
  host: DB_HOST,
  port: Number(DB_PORT),
  database: DB_NAME,
  user: DB_USER,
  password: DB_PASSWORD,
});

const prisma = new PrismaClient({ adapter });

export default prisma;
