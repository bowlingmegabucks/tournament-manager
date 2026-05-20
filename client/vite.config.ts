import { defineConfig } from 'vite';
import tailwindcss from '@tailwindcss/vite';
import react from '@vitejs/plugin-react';
import path from 'node:path';

export default defineConfig({
  plugins: [tailwindcss(), react()],
  cacheDir: process.env.VITE_CACHE_DIR ?? 'node_modules/.vite',
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    host: '0.0.0.0',
    port: 5173,
    // usePolling required for file-watching inside Docker on macOS
    watch: { usePolling: true },
    // No proxy needed: VITE_API_BASE points directly to the API server.
    // Docker Compose overrides VITE_API_BASE to http://server:3000 (container name).
    // Local dev uses VITE_API_BASE=http://localhost:3000 from .env.development.
  },
});
