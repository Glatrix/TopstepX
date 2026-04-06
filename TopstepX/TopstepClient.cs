using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using TopstepX.Models.Accounts;
using TopstepX.Models.Auth;
using TopstepX.Models.Contracts;
using TopstepX.Models.History;
using TopstepX.Models.Orders;
using TopstepX.Models.Positions;
using TopstepX.Models.Trades;
using TopstepX.SignalR;

namespace TopstepX
{
    public class TopstepClient
    {
        private readonly string _apiKey;
        private readonly string _userName;
        private string _sessionToken;
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public TopstepClient(string userName, string apiKey)
        {
            _apiKey = userName == null ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _userName = userName ?? throw new ArgumentNullException(nameof(userName));
            _sessionToken = string.Empty;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.topstepx.com/api/")
            };
        }

        #region Helpers

        private async Task<T?> PostAsync<T>(string endpoint, object? payload = null)
        {
            try
            {
                HttpContent? content = null;

                if (payload != null)
                {
                    var json = JsonSerializer.Serialize(payload);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"HTTP {(int)response.StatusCode}: {error}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                    return default;

                return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Deserialization error on endpoint '{endpoint}': {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new TimeoutException($"Request to '{endpoint}' timed out.", ex);
            }
        }

        private async Task EnsureAuthenticated()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new InvalidOperationException("No active session. Call AuthLoginKey first.");
        }

        private void SetAuthHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        #endregion

        #region Accounts

        public async Task<AccountSearchResponse?> AccountSearch(bool onlyActiveAccounts = true)
        {
            EnsureAuthenticated();

            var request = new AccountSearchRequest { onlyActiveAccounts = onlyActiveAccounts };
            var result = await PostAsync<AccountSearchResponse>("Account/search", request);

            if (result != null && !result.success)
                throw new Exception($"AccountSearch failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region Authentication

        public async Task<LoginResponse?> AuthLoginKey()
        {
            var request = new LoginRequest(_userName, _apiKey);

            var result = await PostAsync<LoginResponse>("Auth/loginKey", request);

            if (result == null)
                throw new Exception("Login failed: empty response");

            if (!result.success)
                throw new Exception($"Login failed: {result.errorMessage}");

            _sessionToken = result.token;
            SetAuthHeader(_sessionToken);

            return result;
        }

        public async Task<LogoutResponse?> AuthLogout()
        {
            EnsureAuthenticated();

            var result = await PostAsync<LogoutResponse>("Auth/logout");

            if (result != null && result.success)
            {
                _sessionToken = string.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            else if (result != null)
            {
                throw new Exception($"Logout failed: {result.errorMessage}");
            }

            return result;
        }

        public async Task<ValidateResponse?> AuthValidate()
        {
            EnsureAuthenticated();

            var result = await PostAsync<ValidateResponse>("Auth/validate");

            if (result != null && result.success)
            {
                _sessionToken = result.newToken;
                SetAuthHeader(_sessionToken);
            }
            else if (result != null)
            {
                throw new Exception($"Validate failed: {result.errorMessage}");
            }

            return result;
        }

        #endregion

        #region Contracts

        public async Task<SearchContractResponse?> ContractSearch(SearchContractRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<SearchContractResponse>("Contract/search", request);

            if (result != null && !result.success)
                throw new Exception($"ContractSearch failed: {result.errorMessage}");

            return result;
        }

        public async Task<SearchContractByIdResponse?> ContractById(string contractId)
        {
            EnsureAuthenticated();

            var request = new SearchContractByIdRequest { contractId = contractId };

            var result = await PostAsync<SearchContractByIdResponse>("Contract/searchById", request);

            if (result != null && !result.success)
                throw new Exception($"ContractById failed: {result.errorMessage}");

            return result;
        }

        public async Task<ListAvailableContractResponse?> ContractAvailable(bool live)
        {
            EnsureAuthenticated();

            var request = new ListAvailableContractRequest { live = live };

            var result = await PostAsync<ListAvailableContractResponse>("Contract/available", request);

            if (result != null && !result.success)
                throw new Exception($"ContractAvailable failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region Orders

        public async Task<SearchOrderResponse?> OrderSearch(SearchOrderRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<SearchOrderResponse>("Order/search", request);

            if (result != null && !result.success)
                throw new Exception($"OrderSearch failed: {result.errorMessage}");

            return result;
        }

        public async Task<SearchOrderResponse?> OrderSearchOpen(SearchOpenOrderRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<SearchOrderResponse>("Order/searchOpen", request);

            if (result != null && !result.success)
                throw new Exception($"OrderSearchOpen failed: {result.errorMessage}");

            return result;
        }

        public async Task<PlaceOrderResponse?> OrderPlace(PlaceOrderRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<PlaceOrderResponse>("Order/place", request);

            if (result != null && !result.success)
                throw new Exception($"OrderPlace failed: {result.errorMessage}");

            return result;
        }

        public async Task<CancelOrderResponse?> OrderCancel(CancelOrderRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<CancelOrderResponse>("Order/cancel", request);

            if (result != null && !result.success)
                throw new Exception($"OrderCancel failed: {result.errorMessage}");

            return result;
        }

        public async Task<ModifyOrderResponse?> OrderModify(ModifyOrderRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<ModifyOrderResponse>("Order/modify", request);

            if (result != null && !result.success)
                throw new Exception($"OrderModify failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region Positions

        public async Task<SearchPositionResponse?> PositionSearchOpen(SearchPositionRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<SearchPositionResponse>("Position/searchOpen", request);

            if (result != null && !result.success)
                throw new Exception($"PositionSearchOpen failed: {result.errorMessage}");

            return result;
        }

        public async Task<ClosePositionResponse?> PositionClose(CloseContractPositionRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<ClosePositionResponse>("Position/closeContract", request);

            if (result != null && !result.success)
                throw new Exception($"PositionClose failed: {result.errorMessage}");

            return result;
        }

        public async Task<PartialClosePositionResponse?> PositionPartialClose(PartialCloseContractPositionRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<PartialClosePositionResponse>("Position/partialCloseContract", request);

            if (result != null && !result.success)
                throw new Exception($"PositionPartialClose failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region History

        public async Task<RetrieveBarResponse?> GetBars(RetrieveBarRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<RetrieveBarResponse>("History/retrieveBars", request);

            if (result != null && !result.success)
                throw new Exception($"GetBars failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region Trades

        public async Task<SearchHalfTradeResponse?> TradeSearch(SearchTradeRequest request)
        {
            EnsureAuthenticated();

            var result = await PostAsync<SearchHalfTradeResponse>("Trade/search", request);

            if (result != null && !result.success)
                throw new Exception($"TradeSearch failed: {result.errorMessage}");

            return result;
        }

        #endregion

        #region Status

        public async Task<string?> Ping()
        {
            try
            {
                var response = await _httpClient.GetAsync("Status/ping");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Ping failed: {response.StatusCode}");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ping failed", ex);
            }
        }

        #endregion

        #region Real-time Data

        public UserHubGateway CreateUserHubGateway(int accountId)
        {
            EnsureAuthenticated();
            return new UserHubGateway(_sessionToken, accountId);
        }

        public MarketHubGateway CreateMarketHubGateway(string contractId)
        {
            EnsureAuthenticated();
            return new MarketHubGateway(_sessionToken, contractId);
        }

        #endregion
    }
}