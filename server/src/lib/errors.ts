export function apiError(code: string, message: string, details?: unknown) {
  return { error: { code, message, details } };
}
