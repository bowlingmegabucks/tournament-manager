# Copilot Code Review Instructions

## General Review Guidelines

- Review code for clarity, maintainability, and readability.
- Ensure code follows project and language-specific style guides.
- Check for consistent naming conventions and file organization.
- Confirm that code is modular and functions/classes are not excessively large.

## Documentation & Comments

- Verify that public functions, classes, and modules have clear docstrings or comments.
- Ensure complex logic is explained with inline comments.
- Remove or flag commented-out code and unnecessary comments.

## Error Handling & Validation

- Check that errors are handled gracefully and not silently ignored.
- Ensure user input is validated and sanitized where appropriate.
- Look for meaningful error messages and logging.

## Security

- Ensure no secrets, credentials, or sensitive data are hardcoded.
- Check for use of secure APIs and libraries.
- Flag any use of deprecated or insecure functions.

## Testing

- Confirm that new code is covered by unit or integration tests.
- Check that tests are meaningful and not just superficial.
- Ensure test names are descriptive and test logic is clear.

## Performance & Efficiency

- Look for obvious performance bottlenecks or inefficient code.
- Suggest improvements for unnecessary loops, redundant calculations, or excessive memory usage.

## Dependencies

- Ensure only necessary dependencies are added.
- Check that dependencies are up-to-date and reputable.

## Pull Request Hygiene

- Confirm that the code is relevant to the purpose of the pull request.
- Ensure the pull request does not include unrelated changes or files.
- Check that the codebase builds and passes all tests after changes.

## Suggestions & Improvements

- Suggest refactoring opportunities for duplicated or overly complex code.
- Recommend best practices and modern language features where appropriate.
- Encourage the use of composition over inheritance, and interfaces for extensibility.

## Example Review Comments

- "Consider renaming this variable for clarity."
- "Add error handling for this function."
- "This logic could be simplified or extracted into a helper method."
- "Please add a test for this edge case."
- "Avoid using deprecated API X; use Y instead."

---

**When reviewing, be constructive and specific. Focus on helping the author improve the code and learn best practices.**
