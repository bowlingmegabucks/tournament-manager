import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { ErrorBanner } from './error-banner'

describe('ErrorBanner', () => {
  it('renders the message', () => {
    render(<ErrorBanner message="Something went wrong" />)
    expect(screen.getByText('Something went wrong')).toBeInTheDocument()
  })

  it('has role="alert" for accessibility', () => {
    render(<ErrorBanner message="Error" />)
    expect(screen.getByRole('alert')).toBeInTheDocument()
  })

  it('does not render Retry button when onRetry is not provided', () => {
    render(<ErrorBanner message="Error" />)
    expect(screen.queryByRole('button', { name: /retry/i })).not.toBeInTheDocument()
  })

  it('renders Retry button when onRetry is provided', () => {
    render(<ErrorBanner message="Error" onRetry={() => {}} />)
    expect(screen.getByRole('button', { name: /retry/i })).toBeInTheDocument()
  })

  it('calls onRetry when Retry button is clicked', async () => {
    const onRetry = vi.fn()
    render(<ErrorBanner message="Error" onRetry={onRetry} />)
    await userEvent.click(screen.getByRole('button', { name: /retry/i }))
    expect(onRetry).toHaveBeenCalledOnce()
  })

  it('applies className prop to the container', () => {
    render(<ErrorBanner message="Error" className="custom-class" />)
    expect(screen.getByRole('alert')).toHaveClass('custom-class')
  })
})
