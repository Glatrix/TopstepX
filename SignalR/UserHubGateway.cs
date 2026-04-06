using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using TopstepX.Models.Accounts;
using TopstepX.Models.Auth;
using TopstepX.Models.Contracts;
using TopstepX.Models.Gateway;
using TopstepX.Models.History;
using TopstepX.Models.Orders;
using TopstepX.Models.Positions;
using TopstepX.Models.Trades;

namespace TopstepX.SignalR
{
    public class UserHubGateway
    {
        private readonly string _jwt;
        private readonly int _accountId;
        private HubConnection _connection;

        public event Action<GatewayUserAccount> OnAccount;
        public event Action<GatewayUserOrder> OnOrder;
        public event Action<GatewayUserPosition> OnPosition;
        public event Action<GatewayUserTrade> OnTrade;

        public UserHubGateway(string jwt, int accountId)
        {
            _jwt = jwt;
            _accountId = accountId;
        }

        public async Task StartAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"https://rtc.topstepx.com/hubs/user?access_token={_jwt}", options =>
                {
                    options.SkipNegotiation = true;
                    options.Transports = HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();

            RegisterHandlers();

            _connection.Reconnected += async (_) =>
            {
                Console.WriteLine("Reconnected");
                await Subscribe();
            };

            _connection.Closed += async (ex) =>
            {
                Console.WriteLine($"Connection closed: {ex?.Message}");
            };

            await _connection.StartAsync();

            Console.WriteLine($"State after start: {_connection.State}");
            await Subscribe();
        }

        private void RegisterHandlers()
        {
            _connection.On<GatewayUserAccount>("GatewayUserAccount", data =>
            {
                OnAccount?.Invoke(data);
            });

            _connection.On<GatewayUserOrder>("GatewayUserOrder", data =>
            {
                OnOrder?.Invoke(data);
            });

            _connection.On<GatewayUserPosition>("GatewayUserPosition", data =>
            {
                OnPosition?.Invoke(data);
            });

            _connection.On<GatewayUserTrade>("GatewayUserTrade", data =>
            {
                OnTrade?.Invoke(data);
            });
        }

        public async Task Subscribe()
        {
            await _connection.InvokeAsync("SubscribeAccounts");
            await _connection.InvokeAsync("SubscribeOrders", _accountId);
            await _connection.InvokeAsync("SubscribePositions", _accountId);
            await _connection.InvokeAsync("SubscribeTrades", _accountId);
        }

        public async Task Unsubscribe()
        {
            await _connection.InvokeAsync("UnsubscribeAccounts");
            await _connection.InvokeAsync("UnsubscribeOrders", _accountId);
            await _connection.InvokeAsync("UnsubscribePositions", _accountId);
            await _connection.InvokeAsync("UnsubscribeTrades", _accountId);
        }

        public async Task StopAsync()
        {
            if (_connection != null)
            {
                await _connection.StopAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
