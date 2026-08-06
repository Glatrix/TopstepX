# TopstepX SDK (Unofficial)

The **TopstepX SDK** is an **unofficial .NET SDK** for interacting with the Topstep API, designed to simplify account management, order handling, and integration with live market updates. This project is not affiliated with, endorsed by, or maintained by Topstep.

(Documentation done with AI cause I am too lazy)

---

## Important Links:
https://gateway.docs.projectx.com/docs/category/api-reference \
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

```bash
dotnet add package TopstepBroker
```

---

## Usage Guide

### Samples

```C#
      var client = new TopstepBroker("email", "token");

      var loginResponse = await client.AuthLoginKey();
      Console.WriteLine($"Token: {loginResponse.token}");

      // Validate your session
      var validateResponse = await client.AuthValidate();
      Console.WriteLine($"Is valid: {validateResponse.success}");

      var accounts = await client.AccountSearch(onlyActiveAccounts: true);
      var account = accounts.accounts.First();
      Console.WriteLine($"{accounts.accounts.Count} account(s) found.");

      var allContracts = await client.ContractAvailable(false);
      Console.WriteLine($"{allContracts.contracts.Count()} account(s) found.");

      var contracts = await client.ContractSearch(new() { live = false, searchText = "MES" });
      var contract = contracts.contracts.First();
      Console.WriteLine($"{contracts.contracts.Count()} account(s) found.");

      var bars = await client.GetBars(new()
      {
        live = false,
        endTime = DateTime.Now,
        startTime = DateTime.Now.AddDays(-1),
        contractId = contract.id,
        unit = AggregateBarUnit.Second,
        unitNumber = 5,
        limit = 100
      });

      Console.WriteLine($"{bars.bars.Count()} bars(s) found.");

      var orders = await client.OrderSearch(new() { accountId = account.id });
      Console.WriteLine($"{orders.orders.Count()} order(s) found.");

      var positions = await client.PositionSearchOpen(new() { accountId = account.id });
      Console.WriteLine($"{positions.positions.Count()} position(s) found.");

      var trades = await client.TradeSearch(new() { accountId = account.id });
      Console.WriteLine($"{trades.trades.Count()} trade(s) found.");

      var userGateway = client.CreateUserHubGateway(account.id);
      userGateway.OnAccount += o => Console.WriteLine($"Account updated: {o.balance}");
      userGateway.OnOrder += o => Console.WriteLine($"Order updated: {o.contractId}");
      userGateway.OnPosition += o => Console.WriteLine($"Position updated: {o.averagePrice}");

      var marketGateway = client.CreateMarketHubGateway(contract.id);
      marketGateway.OnQuote += (message, o) => Console.WriteLine($"Quote {message} vs {o.changePercent}");
      marketGateway.OnTrade += (message, o) => Console.WriteLine($"Trade {message} vs {o.symbolId}");
      marketGateway.OnDepth += (message, o) => Console.WriteLine($"Depth {message} vs {o.type}");

      var orderRequest = new PlaceOrderRequest
      {
        size = 1,
        contractId = contract.id,
        accountId = account.id,
        type = OrderType.Limit,
        side = OrderSide.Buy,
        limitPrice = bars.bars.Last().c - 500,
      };

      var orderResponse = await client.OrderPlace(orderRequest);
      Console.WriteLine($"Order placed successfully: {orderResponse.orderId}");

      var clearResponse = await client.OrderCancel(new() { accountId = account.id, orderId = orderResponse.orderId });
      Console.WriteLine($"Order cancelled successfully: {clearResponse.errorCode}");
```

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
