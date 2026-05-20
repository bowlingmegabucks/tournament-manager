# BowlingMegaBucks — UI Theming Guide

> Source of truth for visual design in the React app at `tournament.bowlingmegabucks.com`.
> All values are derived from the existing `bowlingmegabucks.com` site (Event Star theme by Acme Themes)
> to ensure a seamless experience across both properties.

---

## Fonts

Both fonts are available via Google Fonts:

```html
<link rel="preconnect" href="https://fonts.googleapis.com" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
<link
  href="https://fonts.googleapis.com/css2?family=Lato:wght@400;700&family=Open+Sans:wght@400;600&display=swap"
  rel="stylesheet"
/>
```

| Role                         | Family                    | Weight |
| ---------------------------- | ------------------------- | ------ |
| Headings (h1–h6), Navigation | `"Lato", sans-serif`      | 700    |
| Body, Labels, Inputs         | `"Open Sans", sans-serif` | 400    |
| Code / monospace             | `monospace`               | 400    |

### Type Scale

| Element | Size | Line Height |
| ------- | ---- | ----------- |
| h1      | 36px | 1.2         |
| h2      | 28px | 1.2         |
| h3      | 22px | 1.2         |
| h4      | 18px | 1.2         |
| h5      | 16px | 1.2         |
| h6      | 14px | 1.2         |
| Body    | 15px | 24px        |

---

## Color Palette

### CSS Custom Properties (`:root`)

```css
:root {
  /* Brand */
  --color-brand-black: #000000;
  --color-brand-blue: #2196f3;

  /* Surface */
  --color-surface-dark: #1a1a1a; /* header, nav, footer background */
  --color-surface-page: #ffffff; /* main content area */
  --color-surface-subtle: #f9f9f9; /* alternate section backgrounds */
  --color-surface-muted: #f2f2f2; /* form inputs, table stripes */

  /* Text */
  --color-text-primary: #3a3a3a; /* headings, strong text on light bg */
  --color-text-body: #666666; /* body copy on light bg */
  --color-text-muted: #868686; /* secondary / meta text */
  --color-text-placeholder: #b8b8b8; /* input placeholder text */
  --color-text-on-dark: #ffffff; /* any text rendered on dark surfaces */

  /* Interactive */
  --color-interactive: #2196f3; /* links, primary buttons, focus rings */
  --color-interactive-hover: #1976d2; /* buttons/links on hover */

  /* Borders & Dividers */
  --color-border: #e6e6e6;
  --color-border-light: #eeeeee;
  --color-border-form: #dddddd;

  /* Shadows / Overlays */
  --shadow-nav: 0 2px 4px rgba(0, 0, 0, 0.133);
  --shadow-card: 0 1px 3px rgba(0, 0, 0, 0.2);
  --overlay-gallery: rgba(0, 0, 0, 0.3);

  /* Radius */
  --radius-sm: 4px;
  --radius-md: 8px;
}
```

### Quick Reference Swatches

| Token                    | Hex / Value | Usage                                          |
| ------------------------ | ----------- | ---------------------------------------------- |
| `--color-brand-black`    | `#000000`   | Site brand; header/nav/footer bg               |
| `--color-brand-blue`     | `#2196f3`   | Primary interactive accent                     |
| `--color-surface-dark`   | `#1a1a1a`   | Dark surface (slightly softer than pure black) |
| `--color-surface-page`   | `#ffffff`   | Page / card backgrounds                        |
| `--color-surface-subtle` | `#f9f9f9`   | Alternate row / section shading                |
| `--color-text-primary`   | `#3a3a3a`   | Headings on white/light bg                     |
| `--color-text-body`      | `#666666`   | Paragraph text                                 |
| `--color-text-on-dark`   | `#ffffff`   | Text on headers, nav, footer                   |
| `--color-interactive`    | `#2196f3`   | Links, primary CTA buttons                     |
| `--color-border`         | `#e6e6e6`   | Dividers, card outlines                        |

---

## Spacing System

Mirrors the rhythm used on the main site:

| Token       | Value |
| ----------- | ----- |
| `--space-1` | 4px   |
| `--space-2` | 8px   |
| `--space-3` | 10px  |
| `--space-4` | 15px  |
| `--space-5` | 20px  |
| `--space-6` | 30px  |
| `--space-7` | 50px  |
| `--space-8` | 60px  |

---

## Component Patterns

### Header / Navigation

- Background: `--color-brand-black` (`#000000`)
- Text / icons: `--color-text-on-dark` (`#ffffff`)
- Active / hover link: `--color-brand-blue` (`#2196f3`)
- Navigation item padding: `10px 15px`
- Bottom border / shadow: `var(--shadow-nav)`

### Buttons

```css
/* Primary */
.btn-primary {
  background-color: var(--color-interactive);
  color: #ffffff;
  border: none;
  border-radius: var(--radius-sm);
  padding: 10px 20px;
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  transition: background-color 0.3s ease;
}
.btn-primary:hover {
  background-color: var(--color-interactive-hover);
}

/* Secondary / outline */
.btn-secondary {
  background-color: transparent;
  color: var(--color-interactive);
  border: 1px solid var(--color-interactive);
  border-radius: var(--radius-sm);
  padding: 10px 20px;
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  transition: all 0.3s ease;
}
.btn-secondary:hover {
  background-color: var(--color-interactive);
  color: #ffffff;
}
```

### Cards / Content Panels

- Background: `--color-surface-page` (`#ffffff`)
- Border: `1px solid var(--color-border)`
- Border radius: `var(--radius-sm)` (4px)
- Box shadow: `var(--shadow-card)`
- Padding: `var(--space-6)` (30px)

### Forms & Inputs

- Background: `--color-surface-muted` (`#f2f2f2`)
- Border: `1px solid var(--color-border-form)`
- Border radius: `var(--radius-sm)`
- Placeholder color: `--color-text-placeholder`
- Focus border: `--color-interactive`
- Font: `"Open Sans", sans-serif`, 15px

### Tables

- Header background: `--color-brand-black`
- Header text: `--color-text-on-dark`
- Row border: `--color-border`
- Alternate row: `--color-surface-subtle`

### Footer

- Background: `--color-brand-black`
- Text: `--color-text-on-dark`
- Link hover: `--color-brand-blue`

---

## Tailwind + shadcn/ui

The React app uses **Tailwind CSS v4** (CSS-first config) with **shadcn/ui** (Radix
UI primitives, copied components). Tokens are defined once in `client/src/index.css`
inside an `@theme` block and a `:root` block that satisfies shadcn's CSS variable
contract.

### File locations

| File | Purpose |
| ---- | ------- |
| `client/src/index.css` | `@theme` tokens + shadcn `:root` vars + `@layer base` element styles |
| `client/src/lib/utils.ts` | `cn()` helper (`clsx` + `tailwind-merge`) |
| `client/src/components/ui/` | Copied shadcn components — edit freely |
| `client/components.json` | shadcn CLI config (run `npx shadcn@latest add <component>`) |

### Token → Tailwind class mapping

| Token | Tailwind utility |
| ----- | ---------------- |
| `--color-brand-black` | `bg-brand-black` / `text-brand-black` |
| `--color-brand-blue` | `bg-brand-blue` / `text-brand-blue` |
| `--color-text-on-dark` | `text-text-on-dark` |
| `--color-text-body` | `text-text-body` |
| `--color-text-muted` | `text-text-muted` |
| `--color-surface-muted` | `bg-surface-muted` |
| `--color-surface-subtle` | `bg-surface-subtle` |
| `--color-border` | `border-border` |
| `--color-interactive` | `text-interactive` / `bg-interactive` |
| `--shadow-nav` | `shadow-nav` |
| `--shadow-card` | `shadow-card` |
| `--font-heading` | `font-heading` (Lato 700) |
| `--font-body` | `font-body` (Open Sans 400) |

### shadcn CSS variable contract

shadcn components reference a fixed set of semantic variables (`--primary`,
`--background`, `--border`, etc.). These are mapped to brand values in the `:root`
block in `index.css` — edit there to retheme all shadcn components at once.

| shadcn var | Brand value |
| ---------- | ----------- |
| `--primary` | `#2196f3` |
| `--primary-foreground` | `#ffffff` |
| `--background` | `#ffffff` |
| `--foreground` | `#3a3a3a` |
| `--muted` | `#f9f9f9` |
| `--muted-foreground` | `#868686` |
| `--border` | `#e6e6e6` |
| `--input` | `#dddddd` |
| `--ring` | `#2196f3` |
| `--radius` | `4px` |

### Adding shadcn components

```bash
npx shadcn@latest add <component-name>
# e.g. npx shadcn@latest add table dialog select
```

Components are written to `client/src/components/ui/` and are yours to modify.

### Usage pattern

```tsx
import { Button } from '@/components/ui/button'
import { cn } from '@/lib/utils'

// shadcn component — inherits brand colours via CSS vars
<Button variant="outline">Manage Bowlers</Button>

// Tailwind utility classes with brand tokens
<header className="bg-brand-black text-text-on-dark shadow-nav">

// cn() for conditional classes
<div className={cn('px-4 py-2', isActive && 'bg-surface-subtle')}>
```

---

## Notes

- The `bowlingmegabucks.com` site is built on the **Event Star theme (Acme Themes)** with heavy dark customization applied via the WordPress customizer. The theme's default accent blue (`#2196f3`) appears to be retained, but all chrome (header, nav, footer) is overridden to `#000000`.
- To maintain the seamless feel, `tournament.bowlingmegabucks.com` should share the same Google Fonts (`Lato` + `Open Sans`), the black site chrome, and the `#2196f3` blue for interactive elements.
- Transitions: use `0.3s ease` across the board to match the Event Star animation timing.
