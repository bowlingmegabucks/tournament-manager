import { AlertCircle, RefreshCw } from 'lucide-react'
import { cn } from '@/lib/utils'

interface ErrorBannerProps {
  message: string
  onRetry?: () => void
  className?: string
}

export function ErrorBanner({ message, onRetry, className }: ErrorBannerProps) {
  return (
    <div
      role="alert"
      className={cn(
        'flex items-start gap-3 rounded bg-red-50 border border-red-200 px-4 py-3 text-sm text-red-800',
        className,
      )}
    >
      <AlertCircle className="mt-0.5 size-4 shrink-0" />
      <span className="flex-1">{message}</span>
      {onRetry && (
        <button
          type="button"
          onClick={onRetry}
          className="flex items-center gap-1 font-semibold hover:underline shrink-0"
        >
          <RefreshCw className="size-3" />
          Retry
        </button>
      )}
    </div>
  )
}
