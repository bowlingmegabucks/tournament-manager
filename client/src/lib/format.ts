const currencyFormatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
});

const dateFormatter = new Intl.DateTimeFormat('en-US', {
  month: 'short',
  day: 'numeric',
  year: 'numeric',
  timeZone: 'UTC',
});

export function formatCurrency(n: number): string {
  return currencyFormatter.format(n);
}

export function formatDate(iso: string): string {
  return dateFormatter.format(new Date(iso));
}

export function formatDateRange(startIso: string, endIso: string): string {
  const start = new Date(startIso);
  const end = new Date(endIso);

  const startMonth = start.toLocaleString('en-US', { month: 'short', timeZone: 'UTC' });
  const endMonth = end.toLocaleString('en-US', { month: 'short', timeZone: 'UTC' });
  const startDay = start.toLocaleString('en-US', { day: 'numeric', timeZone: 'UTC' });
  const endDay = end.toLocaleString('en-US', { day: 'numeric', timeZone: 'UTC' });
  const year = end.toLocaleString('en-US', { year: 'numeric', timeZone: 'UTC' });

  if (startMonth === endMonth) {
    return `${startMonth} ${startDay} – ${endDay}, ${year}`;
  }

  return `${startMonth} ${startDay} – ${endMonth} ${endDay}, ${year}`;
}
