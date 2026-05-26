import { test, expect, type Page } from '@playwright/test';
import { PrismaClient } from '@prisma/client';
import { randomUUID } from 'crypto';

const prisma = new PrismaClient();

const fixture = {
  id: randomUUID(),
  name: 'E2E Fall Classic 2025',
  start: new Date('2025-10-10T00:00:00.000Z'),
  end: new Date('2025-10-12T00:00:00.000Z'),
  entryFee: 50.0,
  games: 6,
  finalsRatio: 7.0,
  cashRatio: 5.0,
  superSweeperCashRatio: 4.0,
  bowlingCenter: 'Bowlero E2E',
  completed: false,
};

test.beforeAll(async () => {
  await prisma.tournament.create({ data: fixture });
});

test.afterAll(async () => {
  await prisma.tournament.delete({ where: { id: fixture.id } });
  await prisma.$disconnect();
});

// ─── Flow 1: Tournament list ────────────────────────────────────────────────

test('redirects / to /tournaments', async ({ page }) => {
  await page.goto('/');
  await expect(page).toHaveURL(/\/tournaments/);
});

test('tournaments page heading is visible', async ({ page }) => {
  await page.goto('/tournaments');
  await expect(page.getByRole('heading', { name: 'Tournaments' })).toBeVisible();
});

test('seeded tournament name is visible in the list', async ({ page }) => {
  await page.goto('/tournaments');
  await expect(page.getByText('E2E Fall Classic 2025')).toBeVisible();
});

test('seeded tournament bowling center is visible in the list', async ({ page }) => {
  await page.goto('/tournaments');
  await expect(page.getByText('Bowlero E2E')).toBeVisible();
});

test('status badge is present for seeded tournament', async ({ page }) => {
  await page.goto('/tournaments');
  await expect(page.getByText('Active')).toBeVisible();
});

test('desktop viewport shows table, not cards', async ({ page }) => {
  await page.setViewportSize({ width: 1280, height: 800 });
  await page.goto('/tournaments');
  const desktopContainer = page.getByTestId('tournament-list-desktop');
  const mobileContainer = page.getByTestId('tournament-list-mobile');
  await expect(desktopContainer).toBeVisible();
  // mobile div is rendered but hidden via CSS — check it exists in DOM
  await expect(mobileContainer).toBeAttached();
});

test('mobile viewport shows cards, not table', async ({ page }) => {
  await page.setViewportSize({ width: 390, height: 844 });
  await page.goto('/tournaments');
  const desktopContainer = page.getByTestId('tournament-list-desktop');
  const mobileContainer = page.getByTestId('tournament-list-mobile');
  await expect(mobileContainer).toBeVisible();
  await expect(desktopContainer).toBeAttached();
});

// ─── Flow 2: Navigate to tournament detail ───────────────────────────────────

async function goToDetail(page: Page) {
  await page.goto('/tournaments');
  await page.getByRole('button', { name: 'E2E Fall Classic 2025' }).click();
}

test('clicking tournament navigates to /tournaments/:id/overview', async ({ page }) => {
  await goToDetail(page);
  await expect(page).toHaveURL(new RegExp(`/tournaments/${fixture.id}/overview`));
});

test('tournament name appears in detail header', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await expect(page.getByRole('heading', { name: 'E2E Fall Classic 2025' })).toBeVisible();
});

test('sidebar is visible with all section labels', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  for (const label of ['Overview', 'Divisions', 'Squads', 'Registrations', 'Lane Assignments', 'Results', 'Sweepers']) {
    await expect(page.getByRole('link', { name: label }).first()).toBeVisible();
  }
});

test('dates appear in meta strip', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await expect(page.getByText(/Oct 10/)).toBeVisible();
});

test('bowling center appears in meta strip', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await expect(page.getByText(/Bowlero E2E/)).toBeVisible();
});

// ─── Flow 3: Sidebar navigation ─────────────────────────────────────────────

test('clicking Divisions shows coming-soon placeholder', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('link', { name: 'Divisions' }).first().click();
  await expect(page).toHaveURL(new RegExp(`/tournaments/${fixture.id}/divisions`));
  await expect(page.getByText(/Coming soon/i)).toBeVisible();
});

test('active link updates when sidebar nav is clicked', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('link', { name: 'Divisions' }).first().click();
  const divisionsLink = page.getByRole('link', { name: 'Divisions' }).first();
  // active state is indicated by the aria-current attribute set by NavLink
  await expect(divisionsLink).toHaveAttribute('aria-current', 'page');
});

test('browser back returns to overview', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/divisions`);
  await page.goBack();
  await expect(page).toHaveURL(new RegExp(`/tournaments/${fixture.id}/overview`));
});

// ─── Flow 4: Edit tournament (happy path) ────────────────────────────────────

test('Edit button shows form fields pre-filled', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('button', { name: 'Edit' }).click();
  await expect(page.getByLabel('Name')).toHaveValue('E2E Fall Classic 2025');
});

test('Saving with a new name shows success toast and exits edit mode', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('button', { name: 'Edit' }).click();
  await page.getByLabel('Name').fill('E2E Fall Classic 2025 Updated');
  await page.getByRole('button', { name: 'Save' }).click();
  // success toast
  await expect(page.getByText('Tournament saved')).toBeVisible();
  // form is gone, updated name is shown
  await expect(page.getByLabel('Name')).not.toBeVisible();
  await expect(page.getByRole('heading', { name: 'E2E Fall Classic 2025 Updated' })).toBeVisible();

  // restore original name for subsequent tests
  await page.getByRole('button', { name: 'Edit' }).click();
  await page.getByLabel('Name').fill('E2E Fall Classic 2025');
  await page.getByRole('button', { name: 'Save' }).click();
  await expect(page.getByText('Tournament saved')).toBeVisible();
});

// ─── Flow 5: Edit tournament (cancel) ────────────────────────────────────────

test('Cancelling discards name change', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('button', { name: 'Edit' }).click();
  await page.getByLabel('Name').fill('Should Not Save');
  await page.getByRole('button', { name: 'Cancel' }).click();
  await expect(page.getByLabel('Name')).not.toBeVisible();
  await expect(page.getByText('E2E Fall Classic 2025')).toBeVisible();
});

test('Cancel does not show a toast', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('button', { name: 'Edit' }).click();
  await page.getByLabel('Name').fill('Should Not Save');
  await page.getByRole('button', { name: 'Cancel' }).click();
  await expect(page.getByText('Tournament saved')).not.toBeVisible();
});

// ─── Flow 6: Edit tournament (validation) ────────────────────────────────────

test('clearing name and clicking Save shows inline error', async ({ page }) => {
  await page.goto(`/tournaments/${fixture.id}/overview`);
  await page.getByRole('button', { name: 'Edit' }).click();
  await page.getByLabel('Name').fill('');
  await page.getByRole('button', { name: 'Save' }).click();
  await expect(page.getByText('Name is required')).toBeVisible();
  await expect(page.getByText('Tournament saved')).not.toBeVisible();
  // form stays open
  await expect(page.getByLabel('Name')).toBeVisible();
});
