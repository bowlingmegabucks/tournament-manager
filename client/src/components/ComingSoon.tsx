interface ComingSoonProps {
  readonly section: string
}

export function ComingSoon({ section }: ComingSoonProps) {
  return (
    <div className="py-12 text-center">
      <p className="text-lg font-semibold text-text-muted">{section}</p>
      <p className="mt-1 text-sm text-text-muted">Coming soon</p>
    </div>
  )
}
