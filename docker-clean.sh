#!/usr/bin/env bash
set -euo pipefail

echo "==> Stopping and removing db container + anonymous volumes"
docker compose rm -sfv db

echo "==> Pruning any dangling volumes"
docker volume prune -f

echo "==> Starting db"
docker compose up -d db

echo "==> Tailing logs (Ctrl+C stops watching — container keeps running)"
docker compose logs -f db
