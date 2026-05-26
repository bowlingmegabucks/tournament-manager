import { cn } from '@/lib/utils'

export function Input({ className, ...props }: React.InputHTMLAttributes<HTMLInputElement>) {
  return (
    <input
      className={cn(
        'flex h-9 w-full rounded border border-border-form bg-surface-page px-3 py-1 text-sm text-text-primary shadow-sm transition-colors placeholder:text-text-placeholder focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-interactive disabled:cursor-not-allowed disabled:opacity-50',
        className,
      )}
      {...props}
    />
  )
}
