---
applyTo: "*.cs"
---

# C# Code Instructions

Apply the [general coding guidelines](./copilot-instructions.md) on all code with a focus on C# best practices.

## Role Definition

- C# Language Expert
- Code Quality Specialist

## General

C# code should be written to maximize readability, maintainability, performance, and correctness while minimizing complexity. Follow the [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions) and the [C# Language Specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/) for detailed guidelines.

### Requirements

- Write clear, self-documenting code.
- Keep abstractions simple and focused.
- Minimize dependencies and coupling
- Use modern C# features appropriately

## Code Organization

### Use Meaningful Names

```csharp
// Good: Clear intent
public async Task<Result<Order>> ProcessOrderAsync(OrderRequest request, CancellationToken cancellationToken)

// Avoid: Unclear abbreviations
public async Task<Result<Order>> ProcOrdAsync(OrderRequest req, CancellationToken ct)
```

### Separate State from Behavior

```csharp
// Good: Behavior separate from state
public sealed record Order(OrderId Id, List<OrderLine> Lines);

public static class OrderOperations
{
    public static decimal CalculateTotal(Order order) =>
        order.Lines.Sum(line => line.Price * line.Quantity);
}
```
### Prefer Pure Methods

```csharp
// Good: Pure function
public static decimal CalculateTotalPrice(
    IEnumerable<OrderLine> lines,
    decimal taxRate) =>
    lines.Sum(line => line.Price * line.Quantity) * (1 + taxRate);

// Avoid: Method with side effects
public void CalculateAndUpdateTotalPrice()
{
    this.Total = this.Lines.Sum(l => l.Price * l.Quantity);
    this.UpdateDatabase();
}
```
### Use Extension Methods Appropriately

```csharp
// Good: Extension method for domain-specific operations
public static class OrderExtensions
{
    public static bool CanBeFulfilled(this Order order, Inventory inventory) =>
        order.Lines.All(line => inventory.HasStock(line.ProductId, line.Quantity));
}
```
### Design for Testability

```csharp
// Good: Easy to test pure functions
public static class PriceCalculator
{
    public static decimal CalculateDiscount(
        decimal price,
        int quantity,
        CustomerTier tier) =>
        // Pure calculation
}

// Avoid: Hard to test due to hidden dependencies
public decimal CalculateDiscount()
{
    var user = _userService.GetCurrentUser();  // Hidden dependency
    var settings = _configService.GetSettings(); // Hidden dependency
    // Calculation
}
```
## Dependency Management

### Minimize Constructor Injection

```csharp
// Good: Minimal dependencies
public sealed class OrderProcessor(IOrderRepository repository)
{
    // Implementation
}

// Avoid: Too many dependencies
// Too many dependencies indicates possible design issues
public class OrderProcessor(
    IOrderRepository repository,
    ILogger logger,
    IEmailService emailService,
    IMetrics metrics,
    IValidator validator)
{
    // Implementation
}
```

### Prefer Composition with Interfaces

```csharp
// Good: Compose with interfaces for flexibility
public interface ILogger { void Log(string message); }
public class ConsoleLogger : ILogger { public void Log(string m) => Console.WriteLine(m); }
public class OrderProcessor {
    private readonly ILogger _logger;
    public OrderProcessor(ILogger logger) { _logger = logger; }
    public void Process() { _logger.Log("Order processed"); }
}

// Avoid: Inherit just for code reuse
public class Logger { public void Log(string m) => Console.WriteLine(m); }
public class OrderProcessorBad : Logger {
    public void Process() { Log("Order processed"); }
}
```

## Documentation

### Ensure All Public Members have XML Comments (excluding unit tests) and Use Inheritdoc for Inherited Members

```csharp
// Good: Fully documented public class and method with exception handling and inheritance doc
/// <summary>
/// Represents customer operations.
/// </summary>
public interface ICustomer
{
    /// <summary>
    /// Retrieves a customer with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    /// <exception cref="InvalidIdException">Thrown when the provided ID is invalid.</exception>
    Customer? WithId(Guid id);
}

public class CustomerService
    : ICustomer
{
    /// <inheritdoc />
    public Customer? WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidIdException("ID cannot be empty.");
        }
        // Logic to retrieve customer by ID
        return null;
    }
}

// Avoid: Missing XML comments for public members
public interface ICustomer
{
    Customer? WithId(Guid id);
}
```
## Coding Style

### General Style

- Always follow the rules defined in the repository's [.editorconfig](../../.editorconfig) file and specific C# [.editorconfig](../../backend/.editorconfig) file.
- When defining methods that are only a single line, use arrow expressions and have it indented.

    ```csharp
    // Good: Single-line method with arrow expression
    public int Add(int a, int b)
        => a + b;
    ```
- Constructors should never use arrow expressions and should always use curly braces.

    ```csharp
    // Good: Constructor with curly braces
    public MyClass(int value)
    {
        this.Value = value;
    }
    ```
### Type Definitions

- Prefer `record` for data types

    ```csharp
    // Good: Record type for data
    public sealed record Order(Guid Id, List<OrderLine> Lines);
    ```

- Make classes `sealed` by default unless inheritance is required.

    ```csharp
    // Good: Sealed class
    public sealed class OrderProcessor
    {
        // Implementation
    }

    // Avoid: Non-sealed class unless necessary
    public class OrderProcessorBase
    {
        // Implementation
    }
    ```
- When inheriting from a class or implementing an interface, the class name shoudl be on the main line, and the inheritence should be done intended on the next line.

    ```csharp
    // Good: Class with inheritance on a new line
    public class OrderProcessor
        : IOrderProcessor
    {
        // Implementation
    }

    // Avoid: Inheritance on the same line as the class name
    public class OrderProcessor : IOrderProcessor
    {
        // Implementation
    }
    ```

- Empty methods or constructors should have the open curly brace, a single space, and the closing curly brace on the line under the method/constructor declaration.

    ```csharp
    // Good: Empty method with proper formatting (braces on same line split by one space)
    public void DoNothing()
    { }

    // Avoid: Empty method without proper formatting
    public void DoNothing() { }
    ```

- Marker interfaces should not have curly braces, but terminated with a semi-colon.

    ```csharp
    // Good: Marker interface without curly braces
    public interface IMarkerInterface;

    // Avoid: Marker interface with curly braces
    public interface IMarkerInterface { }
    ```

### Control Flow

- Prefer range indexers over LINQ methods for simple indexing.

    ```csharp
    // Good: Range indexer for simple indexing
    var subArray = array[1..3];

    // Avoid: Using LINQ for simple indexing
    var subArrayBad = array.Skip(1).Take(2).ToArray();
    ```

- Prefer Collection Expressions.  When initializing collections, use C# collection expressions (introduced in C# 12) for clarity and brevity instead of older initialization methods.

**Do this:**
```csharp
var numbers = [1, 2, 3, 4];
var list = [ "apple", "banana", "cherry" ];
```

**Avoid this:**
```csharp
var numbers = new List<int> { 1, 2, 3, 4 };
var list = new List<string> { "apple", "banana", "cherry" };
```

Collection expressions are more concise and improve readability. Use them wherever

- Use pattern matching effectively:

    ```csharp
    // Good: Clear pattern matching
    public decimal CalculateDiscount(Customer customer) =>
        customer switch
        {
            { Tier: CustomerTier.Premium } => 0.2m,
            { OrderCount: > 10 } => 0.1m,
            _ => 0m
        };

    // Avoid: Nested if statements
    public decimal CalculateDiscount(Customer customer)
    {
        if (customer.Tier == CustomerTier.Premium)
            return 0.2m;
        if (customer.OrderCount > 10)
            return 0.1m;
        return 0m;
    }
    ```

### Nullability

- Mark nullable fields explicitly:

    ```csharp
    // Good: Explicit nullability
    public class OrderProcessor
    {
        private readonly ILogger<OrderProcessor>? _logger;
        private string? _lastError;

        public OrderProcessor(ILogger<OrderProcessor>? logger = null)
        {
            _logger = logger;
        }
    }

    // Avoid: Implicit nullability
    public class OrderProcessor
    {
        private readonly ILogger<OrderProcessor> _logger; // Warning: Could be null
        private string _lastError; // Warning: Could be null
    }
    ```

- Use null checks only when necessary for reference types and public methods:

    ```csharp
    // Good: Proper null checking
    public void ProcessOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        _logger?.LogInformation("Processing order {Id}", order.Id);
    }

    // Good: Using pattern matching for null checks
    public decimal CalculateTotal(Order? order) =>
        order switch
        {
            null => throw new ArgumentNullException(nameof(order)),
            { Lines: null } => throw new ArgumentException("Order lines cannot be null", nameof(order)),
            _ => order.Lines.Sum(l => l.Total)
        };
    // Avoid: null checks for value types
    public void ProcessOrder(int orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
    }

    // Avoid: null checks for non-public methods
    private void ProcessOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
    }
    ```

- Use null-forgiving operator when appropriate:

    ```csharp
    public class OrderValidator
    {
        private readonly IValidator<Order> _validator;

        public OrderValidator(IValidator<Order> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public ValidationResult Validate(Order order)
        {
            // We know _validator can't be null due to constructor check
            return _validator!.Validate(order);
        }
    }
    ```

- Use nullability attributes:

    ```csharp
    public class StringUtilities
    {
        // Output is non-null if input is non-null
        [return: NotNullIfNotNull(nameof(input))]
        public static string? ToUpperCase(string? input) =>
            input?.ToUpperInvariant();

        // Method never returns null
        [return: NotNull]
        public static string EnsureNotNull(string? input) =>
            input ?? string.Empty;

        // Parameter must not be null when method returns true
        public static bool TryParse(string? input, [NotNullWhen(true)] out string? result)
        {
            result = null;
            if (string.IsNullOrEmpty(input))
                return false;

            result = input;
            return true;
        }
    }
    ```

- Use init-only properties with non-null validation:

    ```csharp
    // Good: Non-null validation in constructor
    public sealed record Order
    {
        public required OrderId Id { get; init; }
        public required ImmutableList<OrderLine> Lines { get; init; }

        public Order()
        {
            Id = null!; // Will be set by required property
            Lines = null!; // Will be set by required property
        }

        private Order(OrderId id, ImmutableList<OrderLine> lines)
        {
            Id = id;
            Lines = lines;
        }

        public static Order Create(OrderId id, IEnumerable<OrderLine> lines) =>
            new(id, lines.ToImmutableList());
    }
    ```

- Document nullability in interfaces:

    ```csharp
    public interface IOrderRepository
    {
        // Explicitly shows that null is a valid return value
        Task<Order?> FindByIdAsync(OrderId id, CancellationToken ct = default);

        // Method will never return null
        [return: NotNull]
        Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken ct = default);

        // Parameter cannot be null
        Task SaveAsync([NotNull] Order order, CancellationToken ct = default);
    }
    ```

### Safe Operations

- Use Try methods for safer operations:

    ```csharp
    // Good: Using TryGetValue for dictionary access
    if (dictionary.TryGetValue(key, out var value))
    {
        // Use value safely here
    }
    else
    {
        // Handle missing key case
    }
    ```

    ```csharp
    // Avoid: Direct indexing which can throw
    var value = dictionary[key];  // Throws if key doesn't exist

    // Good: Using Uri.TryCreate for URL parsing
    if (Uri.TryCreate(urlString, UriKind.Absolute, out var uri))
    {
        // Use uri safely here
    }
    else
    {
        // Handle invalid URL case
    }
    ```

    ```csharp
    // Avoid: Direct Uri creation which can throw
    var uri = new Uri(urlString);  // Throws on invalid URL

    // Good: Using int.TryParse for number parsing
    if (int.TryParse(input, out var number))
    {
        // Use number safely here
    }
    else
    {
        // Handle invalid number case
    }
    ```

    ```csharp
    // Good: Combining Try methods with null coalescing
    var value = dictionary.TryGetValue(key, out var result)
        ? result
        : defaultValue;

    // Good: Using Try methods in LINQ with pattern matching
    var validNumbers = strings
        .Select(s => (Success: int.TryParse(s, out var num), Value: num))
        .Where(x => x.Success)
        .Select(x => x.Value);
    ```

- Prefer Try methods over exception handling:

    ```csharp
    // Good: Using Try method
    if (decimal.TryParse(priceString, out var price))
    {
        // Process price
    }

    // Avoid: Exception handling for expected cases
    try
    {
        var price = decimal.Parse(priceString);
        // Process price
    }
    catch (FormatException)
    {
        // Handle invalid format
    }
    ```

### Asynchronous Programming

- Use Task.FromResult for pre-computed values:

    ```csharp
    // Good: Return pre-computed value
    public Task<int> GetDefaultQuantityAsync() =>
        Task.FromResult(1);

    // Better: Use ValueTask for zero allocations
    public ValueTask<int> GetDefaultQuantityAsync() =>
        new ValueTask<int>(1);

    // Avoid: Unnecessary thread pool usage
    public Task<int> GetDefaultQuantityAsync() =>
        Task.Run(() => 1);
    ```

- Always flow CancellationToken:

    ```csharp
    // Good: Propagate cancellation
    public async Task<Order> ProcessOrderAsync(
        OrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetAsync(
            request.OrderId,
            cancellationToken);

        await _processor.ProcessAsync(
            order,
            cancellationToken);

        return order;
    }
    ```

- Prefer await:

    ```csharp
    // Good: Using await
    public async Task<Order> ProcessOrderAsync(OrderId id)
    {
        var order = await _repository.GetAsync(id);
        await _validator.ValidateAsync(order);
        return order;
    }
    ```

- Never use Task.Result or Task.Wait:

    ```csharp
    // Good: Async all the way
    public async Task<Order> GetOrderAsync(OrderId id)
    {
        return await _repository.GetAsync(id);
    }

    // Avoid: Blocking on async code
    public Order GetOrder(OrderId id)
    {
        return _repository.GetAsync(id).Result; // Can deadlock
    }
    ```

- Use TaskCompletionSource correctly:

    ```csharp
    // Good: Using RunContinuationsAsynchronously
    private readonly TaskCompletionSource<Order> _tcs =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    // Avoid: Default TaskCompletionSource can cause deadlocks
    private readonly TaskCompletionSource<Order> _tcs = new();
    ```

- Always dispose CancellationTokenSources:

    ```csharp
    // Good: Proper disposal of CancellationTokenSource
    public async Task<Order> GetOrderWithTimeout(OrderId id)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        return await _repository.GetAsync(id, cts.Token);
    }
    ```

- Prefer async/await over direct Task return:

    ```csharp
    // Good: Using async/await
    public async Task<Order> ProcessOrderAsync(OrderRequest request)
    {
        await _validator.ValidateAsync(request);
        var order = await _factory.CreateAsync(request);
        return order;
    }

    // Avoid: Manual task composition
    public Task<Order> ProcessOrderAsync(OrderRequest request)
    {
        return _validator.ValidateAsync(request)
            .ContinueWith(t => _factory.CreateAsync(request))
            .Unwrap();
    }
    ```

### Symbol References

- Always use nameof operator:

    ```csharp
    // Good: Using nameof in attributes
    public class OrderProcessor
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = nameof(OrderId))]
        public string OrderId { get; init; }

        [MemberNotNull(nameof(_repository))]
        private void InitializeRepository()
        {
            _repository = new OrderRepository();
        }

        [NotifyPropertyChangedFor(nameof(FullName))]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
    }
    ```

- Use nameof with exceptions:

    ```csharp
    public class OrderService
    {
        public async Task<Order> GetOrderAsync(OrderId id, CancellationToken ct)
        {
            var order = await _repository.FindAsync(id, ct);

            if (order is null)
                throw new OrderNotFoundException(
                    $"Order with {nameof(id)} '{id}' not found");

            if (!order.Lines.Any())
                throw new InvalidOperationException(
                    $"{nameof(order.Lines)} cannot be empty");

            return order;
        }

        public void ValidateOrder(Order order)
        {
            if (order.Lines.Count == 0)
                throw new ArgumentException(
                    "Order must have at least one line",
                    nameof(order));
        }
    }
    ```

- Use nameof in logging:

    ```csharp
    public class OrderProcessor
    {
        private readonly ILogger<OrderProcessor> _logger;

        public async Task ProcessAsync(Order order)
        {
            _logger.LogInformation(
                "Starting {Method} for order {OrderId}",
                nameof(ProcessAsync),
                order.Id);

            try
            {
                await ProcessInternalAsync(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error in {Method} for {Property} {Value}",
                    nameof(ProcessAsync),
                    nameof(order.Id),
                    order.Id);
                throw;
            }
        }
    }
    ```

### Usings and Namespaces

- Use implicit usings:

    ```csharp
    // Good: Implicit
    namespace MyNamespace
    {
        public class MyClass
        {
            // Implementation
        }
    }
    // Avoid:
    using System; // DON'T USE
    using System.Collections.Generic; // DON'T USE
    using System.IO; // DON'T USE
    using System.Linq; // DON'T USE
    using System.Net.Http; // DON'T USE
    using System.Threading; // DON'T USE
    using System.Threading.Tasks;// DON'T USE
    using System.Net.Http.Json; // DON'T USE
    using Microsoft.AspNetCore.Builder; // DON'T USE
    using Microsoft.AspNetCore.Hosting; // DON'T USE
    using Microsoft.AspNetCore.Http; // DON'T USE
    using Microsoft.AspNetCore.Routing; // DON'T USE
    using Microsoft.Extensions.Configuration; // DON'T USE
    using Microsoft.Extensions.DependencyInjection; // DON'T USE
    using Microsoft.Extensions.Hosting; // DON'T USE
    using Microsoft.Extensions.Logging; // DON'T USE
    using Good: Explicit usings; // DON'T USE

    namespace MyNamespace
    {
        public class MyClass
        {
            // Implementation
        }
    }
    ```

- Use file-scoped namespaces:

    ```csharp
    // Good: File-scoped namespace
    namespace MyNamespace;

    public class MyClass
    {
        // Implementation
    }

    // Avoid: Block-scoped namespace
    namespace MyNamespace
    {
        public class MyClass
        {
            // Implementation
        }
    }
    ```

## Naming Standards

- Keep names focused on the business concept for interfaces, but actual implementations can reflect the technical details:

    ```csharp
    // Good: Interface focuses on business meaning
    public interface ICustomer
    {
        Customer? WithId(Guid id);
    }

    // Implementation can reflect the actual repository or storage mechanism
    public class CustomerRepository
        : ICustomer
    {
        public Customer? WithId(Guid id)
        {
            // Implementation details, e.g., fetching from a database
            return null;
        }
    }
    ```

- Use business terminology for interfaces, but feel free to use technical terms in the implementation:

    ```csharp
    // Good: Interface represents business intent
    public interface IOrder
    {
        IEnumerable<Order> PendingOrders();
    }

    // Implementation can reflect technical details like data source
    public class OrderRepository
        : IOrder
    {
        public IEnumerable<Order> PendingOrders()
        {
            // Logic for fetching orders from a database
            return new List<Order>();
        }
    }
    ```

- Name interfaces based on what they represent, but implementations can be named to reflect how they work:

    ```csharp
    // Good: Interface expresses business meaning
    public interface IOrder
    {
        IEnumerable<Order> OrdersAwaitingShipment();
    }

    // Implementation reflects the storage or retrieval method
    public class OrderDatabaseService
        : IOrder
    {
        public IEnumerable<Order> OrdersAwaitingShipment()
        {
            // Implementation details, such as querying a database
            return new List<Order>();
        }
    }
    ```
