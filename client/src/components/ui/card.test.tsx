import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { Card, CardHeader, CardTitle, CardDescription, CardContent, CardFooter } from './card'

describe('Card', () => {
  it('renders children', () => {
    render(<Card>Card body</Card>)
    expect(screen.getByText('Card body')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<Card className="my-card" />)
    expect(container.firstChild).toHaveClass('my-card')
  })

  it('forwards ref', () => {
    const ref = { current: null as HTMLDivElement | null }
    render(<Card ref={ref} />)
    expect(ref.current).toBeInstanceOf(HTMLDivElement)
  })
})

describe('CardHeader', () => {
  it('renders children', () => {
    render(<CardHeader>Header content</CardHeader>)
    expect(screen.getByText('Header content')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<CardHeader className="my-header" />)
    expect(container.firstChild).toHaveClass('my-header')
  })
})

describe('CardTitle', () => {
  it('renders children', () => {
    render(<CardTitle>My Title</CardTitle>)
    expect(screen.getByText('My Title')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<CardTitle className="my-title" />)
    expect(container.firstChild).toHaveClass('my-title')
  })
})

describe('CardDescription', () => {
  it('renders children', () => {
    render(<CardDescription>Description text</CardDescription>)
    expect(screen.getByText('Description text')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<CardDescription className="my-desc" />)
    expect(container.firstChild).toHaveClass('my-desc')
  })
})

describe('CardContent', () => {
  it('renders children', () => {
    render(<CardContent>Content here</CardContent>)
    expect(screen.getByText('Content here')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<CardContent className="my-content" />)
    expect(container.firstChild).toHaveClass('my-content')
  })
})

describe('CardFooter', () => {
  it('renders children', () => {
    render(<CardFooter>Footer text</CardFooter>)
    expect(screen.getByText('Footer text')).toBeInTheDocument()
  })

  it('applies className prop', () => {
    const { container } = render(<CardFooter className="my-footer" />)
    expect(container.firstChild).toHaveClass('my-footer')
  })
})
