import { describe, it, expect } from 'vitest';
import { formatCurrency, formatDate, formatDateRange } from './format';

describe('formatCurrency', () => {
  it('formats whole dollar amount', () => {
    expect(formatCurrency(50)).toBe('$50.00');
  });

  it('formats decimal amount', () => {
    expect(formatCurrency(50.5)).toBe('$50.50');
  });

  it('formats zero', () => {
    expect(formatCurrency(0)).toBe('$0.00');
  });

  it('formats large amount with commas', () => {
    expect(formatCurrency(1000)).toBe('$1,000.00');
  });

  it('formats amount with two decimal places', () => {
    expect(formatCurrency(99.99)).toBe('$99.99');
  });
});

describe('formatDate', () => {
  it('formats a standard date', () => {
    expect(formatDate('2025-10-10')).toBe('Oct 10, 2025');
  });

  it('formats January 1st', () => {
    expect(formatDate('2025-01-01')).toBe('Jan 1, 2025');
  });

  it('formats December 31st', () => {
    expect(formatDate('2025-12-31')).toBe('Dec 31, 2025');
  });

  it('does not shift day due to timezone (uses UTC)', () => {
    // "2025-10-10" interpreted as UTC midnight must display as Oct 10, not Oct 9
    expect(formatDate('2025-10-10')).toContain('Oct 10');
  });
});

describe('formatDateRange', () => {
  it('formats same-month range compactly', () => {
    expect(formatDateRange('2025-10-10', '2025-10-12')).toBe('Oct 10 – 12, 2025');
  });

  it('formats cross-month range', () => {
    expect(formatDateRange('2025-10-28', '2025-11-02')).toBe('Oct 28 – Nov 2, 2025');
  });

  it('formats single-day event (start === end)', () => {
    expect(formatDateRange('2025-10-10', '2025-10-10')).toBe('Oct 10 – 10, 2025');
  });

  it('uses the end year', () => {
    expect(formatDateRange('2025-12-30', '2026-01-02')).toBe('Dec 30 – Jan 2, 2026');
  });
});
