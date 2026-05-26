import { describe, it, expect, vi } from 'vitest'

// vi.hoisted ensures these are available when the vi.mock factories below run
const { mockRender, mockCreateRoot } = vi.hoisted(() => {
  const mockRender = vi.fn()
  const mockCreateRoot = vi.fn(() => ({ render: mockRender }))
  return { mockRender, mockCreateRoot }
})

vi.mock('react-dom/client', () => ({ createRoot: mockCreateRoot }))
vi.mock('./App', () => ({ default: () => null }))
vi.mock('sonner', () => ({ Toaster: () => null }))
vi.mock('react-router-dom', () => ({ BrowserRouter: ({ children }: { children: React.ReactNode }) => children }))
vi.mock('@tanstack/react-query', () => ({
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  QueryClient: vi.fn(function QueryClient() {}),
  QueryClientProvider: ({ children }: { children: React.ReactNode }) => children,
}))

describe('main', () => {
  it('mounts the app into #root', async () => {
    document.body.innerHTML = '<div id="root"></div>'
    await import('./main')
    expect(mockCreateRoot).toHaveBeenCalledWith(document.getElementById('root'))
    expect(mockRender).toHaveBeenCalledOnce()
  })
})
