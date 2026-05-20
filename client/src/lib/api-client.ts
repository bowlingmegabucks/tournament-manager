const API_BASE = import.meta.env.VITE_API_BASE ?? ''

export async function apiFetch(path: string, init?: RequestInit): Promise<Response> {
  return fetch(`${API_BASE}${path}`, init)
}
