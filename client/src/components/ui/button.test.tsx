import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { Button } from './button'

describe('Button', () => {
  it('renders a button element', () => {
    render(<Button>Click me</Button>)
    expect(screen.getByRole('button', { name: 'Click me' })).toBeInTheDocument()
  })

  it('renders children', () => {
    render(<Button>Label text</Button>)
    expect(screen.getByText('Label text')).toBeInTheDocument()
  })

  it('calls onClick when clicked', async () => {
    const onClick = vi.fn()
    render(<Button onClick={onClick}>Click</Button>)
    await userEvent.click(screen.getByRole('button'))
    expect(onClick).toHaveBeenCalledOnce()
  })

  it('is disabled when disabled prop is set', () => {
    render(<Button disabled>Click</Button>)
    expect(screen.getByRole('button')).toBeDisabled()
  })

  it('does not fire onClick when disabled', async () => {
    const onClick = vi.fn()
    render(<Button disabled onClick={onClick}>Click</Button>)
    await userEvent.click(screen.getByRole('button'))
    expect(onClick).not.toHaveBeenCalled()
  })

  it('applies className prop', () => {
    render(<Button className="custom-class">Click</Button>)
    expect(screen.getByRole('button')).toHaveClass('custom-class')
  })

  it('renders the child element when asChild is true', () => {
    render(
      <Button asChild>
        <a href="/test">Link</a>
      </Button>,
    )
    expect(screen.getByRole('link', { name: 'Link' })).toBeInTheDocument()
    expect(screen.queryByRole('button')).not.toBeInTheDocument()
  })

  it('forwards ref to the button element', () => {
    const ref = { current: null as HTMLButtonElement | null }
    render(<Button ref={ref}>Ref</Button>)
    expect(ref.current).toBeInstanceOf(HTMLButtonElement)
  })

  it('passes additional HTML attributes to the button', () => {
    render(<Button aria-label="custom label">X</Button>)
    expect(screen.getByRole('button', { name: 'custom label' })).toBeInTheDocument()
  })
})
