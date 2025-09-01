# WinForms MVP Application Development & Implementation Guide

This guide describes best practices for developing a **Windows Forms (WinForms)** application using the **MVP (Model–View–Presenter)** pattern.
The **presentation layer** (presenters, view interfaces, adapters, and models) exists in a separate project from the WinForms UI project.
Communication with the backend is performed exclusively through **adapters** that encapsulate HTTP calls to the Web API.
**OpenTelemetry** is used to capture end-to-end observability across:
**WinForms → Web API → Database → Background Jobs**.

---

## 🏗️ MVP Pattern Overview

- **View Interfaces**: Define what the UI can display and which events it raises.
- **Views (WinForms)**: Implement the view interfaces. Forward UI events to the presenter.
- **Presenters**: Contain orchestration logic. Subscribe to view events, call adapters, update the view.
- **Adapters**: Encapsulate all HTTP communication with the Web API.
- **Models (DTOs)**: Represent API data contracts.

---

## 🎨 UI Design & Implementation Best Practices

Although the application is primarily static, careful attention to UI design and implementation ensures a maintainable and user-friendly experience.
This section outlines general guidelines as well as special considerations for the form that will eventually support live updates via **SignalR**.

### General Guidelines

- **Consistency**:
  - Follow a consistent layout and control naming convention.
  - Use standard Windows controls where possible to align with user expectations.
- **Accessibility**:
  - Configure `TabIndex` and `TabStop` for logical navigation.
  - Provide keyboard shortcuts for common actions.
  - Add tooltips where control purpose may not be obvious.
- **Layout**:
  - Prefer `TableLayoutPanel` and `FlowLayoutPanel` for dynamic, resizable layouts.
  - Avoid hard-coded pixel values when controls should scale.
- **Feedback & Responsiveness**:
  - Disable buttons during long-running operations.
  - Provide progress indicators or status labels when API calls are in-flight.
  - Ensure forms remain responsive by using `async/await` for all I/O-bound operations.
- **Error Handling**:
  - Display user-friendly error messages in dialogs or status bars.
  - Avoid exposing technical exceptions directly to the user.

### Reusable User Controls & Fluid Layout

To improve maintainability and create a consistent look across the application, use **UserControls** for reusable UI components (e.g., search panels, data grids, toolbars).

#### Best Practices

- **Encapsulation**:
  - Group related controls into a `UserControl` rather than duplicating them across multiple forms.
  - Expose properties and events so that presenters can interact with the control without depending on internal details.

- **Fluid Layout**:
  - Use **`Dock`** and **`Anchor`** properties to allow controls to resize with their container.
  - Combine with `TableLayoutPanel` or `FlowLayoutPanel` inside the UserControl to achieve responsive layouts.
  - Avoid fixed positions and hard-coded sizes unless absolutely necessary.

- **Design for reuse**:
  - UserControls should adapt visually whether hosted in a full form, a panel, or a tab page.
  - Provide sensible minimum sizes so controls do not break when shrunk.
  - Consider different DPI settings and scaling (WinForms supports automatic scaling via `AutoScaleMode`).

- **Consistency**:
  - Standardize padding, margins, and font usage across all UserControls.
  - Apply styles through the parent form where possible for a uniform theme.

#### Example

- A **CustomerGridControl** UserControl encapsulates a `DataGridView` and a toolbar with buttons (`Add`, `Edit`, `Delete`).
- When placed in a form that is resizable, the grid automatically expands or contracts, while the toolbar stays docked at the top.
- The same UserControl can be reused in multiple forms, ensuring consistency and reducing duplicate code.

Here is a concise example of a UserControl with fluid layout that developers can use as a reference

```csharp
// Example: CustomerGridControl.cs
public partial class CustomerGridControl : UserControl
{
    public event EventHandler AddCustomerClicked;
    public event EventHandler EditCustomerClicked;
    public event EventHandler DeleteCustomerClicked;

    public CustomerGridControl()
    {
        InitializeComponent();
        SetupLayout();
    }

    private void SetupLayout()
    {
        // Toolbar
        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight
        };

        var btnAdd = new Button { Text = "Add" };
        var btnEdit = new Button { Text = "Edit" };
        var btnDelete = new Button { Text = "Delete" };

        btnAdd.Click += (s, e) => AddCustomerClicked?.Invoke(this, EventArgs.Empty);
        btnEdit.Click += (s, e) => EditCustomerClicked?.Invoke(this, EventArgs.Empty);
        btnDelete.Click += (s, e) => DeleteCustomerClicked?.Invoke(this, EventArgs.Empty);

        toolbar.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

        // Grid
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };

        // Add controls to UserControl
        Controls.Add(grid);
        Controls.Add(toolbar)
    }
}
```

##### Key Takeaways

- Dock = `DockStyle.Fill` makes the grid expand and contract with the UserControl
- Dock = `DockStyle.Top` keeps the toolbar at the top while allowing the grid to fill the remaining space.
- The UserControl itself can then be docked (Dock = `DockStyle.Fill`) or anchored within a form, and it will scale automatically
- Events are exposed (`AddCustomerClicked`) so the Presenter can handle them keeping the control reusable and decoupled

### High-DPI & Scaling Considerations

To ensure the UI looks correct on high-resolution displays and when the user changes DPI settings, follow these best practices:

#### Form-Level Settings

- Set `AutoScaleMode` to `Dpi` or `Font` in your form constructor or designer:

```csharp
public CustomerForm()
{
    InitializeComponent();
    this.AutoScaleMode = AutoScaleMode.Dpi; // or AutoScaleMode.Font
}
```

- This enables controls and fonts to scale automatically according to the system DPI

#### UserControl-Level Setting

- Ensure all controls inside a UserControl respect docking and anchoring; avoid fixed sizes
- Avoid hard-coding font sizes; use `SystemFonts` where possible
- Use layout panels (`TableLayoutPanel`, `FlowLayoutPanel`) to allow dynamic resizing rather than absolute positioning

#### Images & Icons

- Provide multiple-resolution versions for icons or images, or use vector-based images where possible
- Set `PictureBox.SizeMode = PictureBoxSizeMode.Zoom` to scale images proportionally with the control size

#### Testing

- Test the application at different DPI settings (100%, 125%, 150%, etc.)
- Verify that controls do not overlap, clip, or lose readability when scaled

#### Live Updates Form

- High-DPI handling is especially important for SignalR-enabled forms, as row and indicators may dynamically update.  Proper scaling ensure the UI remains readable and usable even under frequent live updates.

### Live Updates Form (SignalR Integration)

- **Design for concurrency**:
  - Assume data may change after being loaded; present real-time updates without requiring full refreshes.
  - Indicate visually when updates occur (highlight changed rows, animate briefly, or show a "last updated" timestamp).
- **Non-blocking updates**:
  - Apply SignalR events on the UI thread (`this.Invoke` / `SynchronizationContext`) to avoid cross-thread issues.
  - Never block the UI while waiting for messages.
- **Conflict resolution**:
  - Plan how to handle cases where multiple users update the same record. Options:
    - Last write wins (simplest).
    - Prompt user when conflicts occur.
    - Merge strategies if domain requires it.
- **Scalability**:
  - Limit the amount of data sent in updates. Prefer targeted changes (e.g., "row X updated") instead of full dataset reloads.
- **Testing**:
  - Validate the form under conditions where multiple updates happen quickly.
  - Ensure the UI remains stable even if updates arrive out of order or are delayed.

---

## 🖥️ Example Flow

1. User clicks "Load Customers" button in the WinForms form.
2. The form (view) raises an event defined in its interface.
3. The presenter handles the event, starts a telemetry span, and calls the adapter.
4. The adapter issues an HTTP request, with OpenTelemetry injecting the trace context.
5. The API processes the request, touches the database, or enqueues background jobs.
6. Results flow back → adapter → presenter → view.

---

## 📡 OpenTelemetry & Distributed Tracing

To achieve **end-to-end observability** across the WinForms client, Web API, database, and background jobs, integrate OpenTelemetry throughout the presentation layer and adapters.

### Goals

- Trace **user actions** (Presenter spans).
- Trace **API calls** (Adapter spans).
- Propagate trace context to the Web API and any background jobs.
- Include live updates (SignalR) in tracing for forms that receive asynchronous events.

### WinForms Client

**Program.cs setup:**

```csharp
services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("WinFormsClient"))
    .WithTracing(b => b
        .AddSource("Presentation.Presenter") // Presenter spans
        .AddSource("Presentation.Adapter")   // Adapter spans
        .AddHttpClientInstrumentation()     // Outgoing API calls
        .AddOtlpExporter());                // OTLP collector endpoint

// Register adapters
services.AddHttpClient<ICustomerApiAdapter, CustomerApiAdapter>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!);
});
```

### Presenter Spans

- Each **user action** (e.g., clicking a button) should create a span:

```csharp
using var activity = _activitySource.StartActivity("LoadCustomers", ActivityKind.Client);
```

- Status should be set to `Ok` or `Error` based on outcome.

### Adapter Spans

- Wrap **HTTP calls** in spans:

```csharp
using var activity = _activitySource.StartActivity("Http Get Customers", ActivityKind.Client);
var response = await _client.GetAsync("api/customers");
```

- OpenTelemetry automatically propagates **traceparent** headers so the API can join the same trace.

### SignalR / Live Updates

- Treat **incoming SignalR messages** as new spans (e.g., `CustomerUpdatedReceived`).
- Resume the trace if the message includes a `traceparent` or start a new child span under the user action that triggered the subscription.
- Use `this.Invoke()` or the UI `SynchronizationContext` when updating controls to avoid cross-thread issues.

```csharp
private void OnCustomerUpdated(CustomerDto dto, string traceParent)
{
    var context = ActivityContext.Parse(traceParent, null);
    using var activity = _activitySource.StartActivity("CustomerUpdatedReceived", ActivityKind.Consumer, context);

    this.Invoke(() => _grid.UpdateRow(dto));
}
```

### Observability Best Practices

- **Trace hierarchy**:
  - Presenter → Adapter → API → Database / Background Jobs.
  - SignalR updates should appear as **child spans** of the originating user action if applicable.
- **Logs**: Include `TraceId` and `SpanId` for correlation with external monitoring systems.
- **Metrics**: Track latency, success/failure rates, and job execution times.
- **Collector / Visualization**: Route traces to Jaeger, Zipkin, Grafana Tempo, or Azure Monitor.

### Tips

- **Adapters** should never swallow exceptions; propagate them so spans can reflect failures.
- **Presenter spans** should only measure orchestration and UI-related logic; avoid including heavy computation.
- For **SignalR**, consider batching rapid updates or sampling to avoid excessive telemetry.

---

## 🖼️ Example Components

### View Interface

```csharp
public interface ICustomerView
{
    void ShowCustomers(IEnumerable<CustomerDto> customers);
    void ShowError(string message);
    event EventHandler LoadCustomersRequested;
}
```

### WinForms View

```csharp
public partial class CustomerForm
    : Form, ICustomerView
{
    public event EventHandler LoadCustomersRequested;

    public CustomerForm()
    {
        InitializeComponent();
        btnLoad.Click += (s, e) => LoadCustomersRequested?.Invoke(this, EventArgs.Empty);
    }

    public void ShowCustomers(IEnumerable<CustomerDto> customers) =>
        gridCustomers.DataSource = customers.ToList();

    public void ShowError(string message) =>
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### Presenter

```csharp
using System.Diagnostics;

public class CustomerPresenter
{
    private readonly ICustomerView _view;
    private readonly ICustomerApiAdapter _adapter;
    private readonly ActivitySource _activitySource = new("Presentation.Presenter");

    public CustomerPresenter(ICustomerView view, ICustomerApiAdapter adapter)
    {
        _view = view;
        _adapter = adapter;
        _view.LoadCustomersRequested += async (s, e) => await OnLoadCustomersAsync();
    }

    private async Task OnLoadCustomersAsync()
    {
        using var activity = _activitySource.StartActivity("LoadCustomers", ActivityKind.Client);

        try
        {
            var customers = await _adapter.GetCustomersAsync();
            _view.ShowCustomers(customers);
            activity?.SetStatus(ActivityStatusCode.Ok);
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
        }
    }
}
```

### Adapter

```csharp
using System.Diagnostics;

public class CustomerApiAdapter
    : ICustomerApiAdapter
{
    private readonly HttpClient _client;
    private readonly ActivitySource _activitySource = new("Presentation.Adapter");

    public CustomerApiAdapter(HttpClient client) => _client = client;

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
    {
        using var activity = _activitySource.StartActivity("Http Get Customers", ActivityKind.Client);

        var response = await _client.GetAsync("api/customers");
        response.EnsureSuccessStatusCode();

        var customers = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
        return customers ?? Enumerable.Empty<CustomerDto>();
    }
}
```

---

## 🔍 Observability Best Practices

### Spans

- **Presenter** = high-level user actions
- **Adapter** = API calls
- **API** = request handling, EF Core/SQL.
- **Background Jobs** = resumed from trace context

### Logs

- Include `TraceId` and `SpanId` for correlation

### Metrics

- Track response times, error counts, and job execution durations.

### Tracing Tools

- Use Jaeger, Zipkin, Grafana Tempo, or Azure Monitor to visualize flows.

---

## 🧪 Testing Guidelines

### View Testing

- Mocked in tests; presenters tested against the interface

### Presenter Testing

- Test behavior by mocking adapters and verifying calls to view.

### Adapter Testing

- Test with mocked `HttpMessageHandler` to simulate API responses.

### Integration Testing

- Validate trace headers flow from WinForms --> API --> Database.

---

## Developer Checklist

- Views implement interfaces and raise events
- Presenters handle orchestration and contain no UI logic
- Adapters encapsulate HTTP calls and expose async methods
- Telemetry spans exist for both presenter actions and adapter calls
- Trace context flows end-to-end across client, API, database, and jobs.
- Logs and metrics enriched with trace identifiers
- Unit and integration tests in place
