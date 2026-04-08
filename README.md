# TopstepX SDK (Unofficial)

The **TopstepX SDK** is an **unofficial .NET SDK** for interacting with the Topstep API, designed to simplify account management, order handling, and integration with live market updates. This project is not affiliated with, endorsed by, or maintained by Topstep.

(Documentation done with AI cause I am too lazy)

## Important Links:
https://gateway.docs.projectx.com/docs/category/api-reference
https://gateway.docs.projectx.com/docs/realtime/

---

## Features

- **Authentication**: Log in, validate, and manage user sessions securely.
- **Account Management**: Retrieve and manage account details, including active accounts.
- **Contracts**: Retrieve contract data and manage available contracts.
- **Orders**: Search, place, modify, and cancel orders.
- **Positions**: Manage trading positions, including open, close, and partial closes.
- **Trading History**: Retrieve historical bars and trade data for analysis.
- **Real-time Market Updates**: Integrate live updates with SignalR for tracking account changes and market movements.

---

## Disclaimer

This is an **unofficial SDK** created by the community and is **not maintained or supported by Topstep**. Use this library at your own discretion.

---

## Installation

### Prerequisites

- **.NET 8.0 or later** is required.
- Install the following NuGet packages:

```bash
dotnet add package Microsoft.AspNetCore.Http.Connections
dotnet add package Microsoft.AspNetCore.Http.Connections.Common
dotnet add package Microsoft.AspNetCore.SignalR.Client
```

---

## Usage Guide

### Initialization

To get started, instantiate the `TopstepClient` class with your `userName` and `apiKey`:

```csharp
var client = new TopstepClient("your-username", "your-api-key");
```

### Authentication

Authenticate yourself with the API:

```csharp
// Log in with your API key
var loginResponse = await client.AuthLoginKey();
Console.WriteLine($"Token: {loginResponse.Token}");

// Validate your session
var validateResponse = await client.AuthValidate();
Console.WriteLine($"Is valid: {validateResponse.Success}");

// Log out of your session
var logoutResponse = await client.AuthLogout();
Console.WriteLine($"Logged out: {logoutResponse.Success}");
```

### Accounts

Search for accounts:

```csharp
var accounts = await client.AccountSearch(onlyActiveAccounts: true);
Console.WriteLine($"{accounts.ActiveAccounts.Count} account(s) found.");
```

### Orders

Place, search, or cancel orders:

```csharp
var orderRequest = new PlaceOrderRequest
{
    ContractId = "contract-id",
    Quantity = 1,
    Price = 100
};

var orderResponse = await client.OrderPlace(orderRequest);
Console.WriteLine($"Order placed successfully: {orderResponse.OrderId}");
```

### Real-time Updates

Integrate live updates using SignalR:

```csharp
var userGateway = client.CreateUserHubGateway(accountId);
userGateway.OnAccountUpdate += account =>
{
    Console.WriteLine($"Account updated: {account.Id}");
};

var marketGateway = client.CreateMarketHubGateway("contract-id");
marketGateway.OnMarketUpdate += market =>
{
    Console.WriteLine($"Market updated for contract {market.ContractId}");
};
```

---

## Project Structure

- **`TopstepClient.cs`**: Core entry point for interacting with the API.
- **Helper Methods**: For post requests, authentication headers, and JSON deserialization.
- **`TopstepX.csproj`**: Configured for .NET 8.0 compatibility with required dependencies.

Regions in the `TopstepClient` class are used for modular organization:
- `#region Authentication`
- `#region Accounts`
- `#region Contracts`
- `#region Orders`

---

## Contributing

Contributions to improve the SDK are welcome.

1. Fork the repository.
2. Create a branch for your feature/bug fix.
3. Submit a pull request.

---

## License

This project is licensed under [LICENSE](https://github.com/Glatrix/TopstepX/blob/main/LICENSE).

---

## Disclaimer

This SDK is not an official Topstep project. For details about the Topstep API, refer to the [Topstep Documentation](https://www.topstep.com/api).
