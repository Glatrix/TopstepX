using System;
using System.Linq;
using TopstepX;
using TopstepX.Models.History;
using TopstepX.Models.Orders;

namespace Samples
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Run();
      Console.ReadLine();
    }

    static async void Run()
    {
      var client = new TopstepBroker("email@example.com", "TOKEN_HERE");

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
    }
  }
}
