# Copilot Instructions

## Code Quality

- Always generate clear, readable, and maintainable code.
- Prefer explicitness over cleverness; prioritize understandability.
- Add comments for complex logic or non-obvious decisions.

## Consistency

- Follow the project's established naming conventions and file structure.
- Use consistent formatting (respect [.editorconfig](../../.editorconfig) and Prettier settings).
- Keep code style uniform across files and modules.

## Documentation

- Include docstrings or comments for public functions, classes, and modules.
- Document assumptions, side effects, and any non-standard behavior.

## Error Handling

- Handle errors gracefully and provide meaningful error messages.
- Avoid silent failures; log or report errors where appropriate.
- Avoid exposing sensitive information and inner workings in error messages in production code.  Exposing inner workings (e.g. stack traces) is acceptable in development code.

## Security & Safety

- Never hardcode secrets, credentials, or sensitive data.
- Validate and sanitize all user input
- Avoid using deprecated or insecure APIs.

## Testing

- Write code that is testable and modular.
- Prefer pure functions and avoid unnecessary side effects.
- When possible, include or suggest relevant unit tests.

## Dependencies

- Use well-maintained, reputable libraries.
- Avoid unnecessary dependencies; prefer built-in modules when possible.

## Collaboration

- Write code that is easy for others to review and extend.
- Leave TODOs or FIXMEs for incomplete or questionable sections, with clear explanations.
