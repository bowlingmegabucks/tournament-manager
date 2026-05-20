import { describe, it, expect } from 'vitest'
import { cn } from './utils'

describe('cn', () => {
  it('returns a single class unchanged', () => {
    expect(cn('foo')).toBe('foo')
  })

  it('joins multiple classes with a space', () => {
    expect(cn('foo', 'bar', 'baz')).toBe('foo bar baz')
  })

  it('ignores undefined values', () => {
    expect(cn('foo', undefined, 'bar')).toBe('foo bar')
  })

  it('ignores false values', () => {
    expect(cn('foo', false, 'bar')).toBe('foo bar')
  })

  it('ignores null values', () => {
    expect(cn('foo', null, 'bar')).toBe('foo bar')
  })

  it('supports conditional class expressions', () => {
    const active = true
    const disabled = false
    expect(cn('base', active && 'active', disabled && 'disabled')).toBe('base active')
  })

  it('resolves conflicting Tailwind padding classes — last one wins', () => {
    expect(cn('p-4', 'p-8')).toBe('p-8')
  })

  it('resolves conflicting Tailwind text-color classes — last one wins', () => {
    expect(cn('text-red-500', 'text-blue-500')).toBe('text-blue-500')
  })

  it('returns empty string when called with no arguments', () => {
    expect(cn()).toBe('')
  })

  it('supports object syntax for conditional classes', () => {
    expect(cn({ active: true, disabled: false })).toBe('active')
  })
})
