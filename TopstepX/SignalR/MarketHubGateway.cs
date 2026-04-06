using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using TopstepX.Models.Gateway;

namespace TopstepX.SignalR
{
    public class MarketHubGateway
    {
        private readonly string _jwt;
        private readonly string _contractId;
        private HubConnection _connection;

        public event Action<string, GatewayQuote> OnQuote;
        public event Action<string, GatewayTrade> OnTrade;
        public event Action<string, GatewayDepth> OnDepth;

        public MarketHubGateway(string jwt, string contractId)
        {
            _jwt = jwt;
            _contractId = contractId;
        }

        public async Task StartAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"https://rtc.topstepx.com/hubs/market?access_token={_jwt}", options =>
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
            _connection.On<string, GatewayQuote>("GatewayQuote", (contractId, data) =>
            {
                OnQuote?.Invoke(contractId, data);
            });

            _connection.On<string, GatewayTrade>("GatewayTrade", (contractId, data) =>
            {
                OnTrade?.Invoke(contractId, data);
            });

            _connection.On<string, GatewayDepth>("GatewayDepth", (contractId, data) =>
            {
                OnDepth?.Invoke(contractId, data);
            });
        }

        public async Task Subscribe()
        {
            await _connection.InvokeAsync("SubscribeContractQuotes", _contractId);
            await _connection.InvokeAsync("SubscribeContractTrades", _contractId);
            await _connection.InvokeAsync("SubscribeContractMarketDepth", _contractId);
        }

        public async Task Unsubscribe()
        {
            await _connection.InvokeAsync("UnsubscribeContractQuotes", _contractId);
            await _connection.InvokeAsync("UnsubscribeContractTrades", _contractId);
            await _connection.InvokeAsync("UnsubscribeContractMarketDepth", _contractId);
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
