import { defineConfig } from 'prisma/config';
import 'dotenv/config';

const { DB_HOST = '127.0.0.1', DB_PORT = '13306', DB_NAME, DB_USER, DB_PASSWORD } = process.env;

export default defineConfig({
  schema: 'prisma/schema.prisma',
  datasource: {
    url: `mysql://${DB_USER}:${DB_PASSWORD}@${DB_HOST}:${DB_PORT}/${DB_NAME}`,
  },
});
