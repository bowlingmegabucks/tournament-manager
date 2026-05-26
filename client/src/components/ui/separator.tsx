import { cn } from '@/lib/utils'

interface SeparatorProps extends React.HTMLAttributes<HTMLHRElement> {
  readonly orientation?: 'horizontal' | 'vertical'
}

export function Separator({ orientation = 'horizontal', className, ...props }: SeparatorProps) {
  return (
    <hr
      className={cn(
        'border-border',
        orientation === 'horizontal' ? 'w-full border-t' : 'h-full border-l',
        className,
      )}
      {...props}
    />
  )
}
