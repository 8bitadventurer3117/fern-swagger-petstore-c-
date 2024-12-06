using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public partial class StoreClient
{
    private RawClient _client;

    internal StoreClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Returns a map of status codes to quantities
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Store.GetInventoryAsync();
    /// </code>
    /// </example>
    public async Task<Dictionary<string, int>> GetInventoryAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "store/inventory",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Dictionary<string, int>>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new BitadventurerApiException("Failed to deserialize response", e);
            }
        }

        throw new BitadventurerApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Place a new order in the store
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Store.PlaceOrderAsync(new Order());
    /// </code>
    /// </example>
    public async Task<Order> PlaceOrderAsync(
        Order request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "store/order",
                Body = request,
                ContentType = "application/x-www-form-urlencoded",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Order>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new BitadventurerApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 405:
                    throw new MethodNotAllowedError(JsonUtils.Deserialize<object>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new BitadventurerApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// For valid response try integer IDs with value &lt;= 5 or &gt; 10. Other values will generate exceptions.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Store.GetOrderByIdAsync(1000000);
    /// </code>
    /// </example>
    public async Task<Order> GetOrderByIdAsync(
        long orderId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"store/order/{orderId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Order>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new BitadventurerApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<object>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<object>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new BitadventurerApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// For valid response try integer IDs with value &lt; 1000. Anything above 1000 or nonintegers will generate API errors
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Store.DeleteOrderAsync(1000000);
    /// </code>
    /// </example>
    public async Task DeleteOrderAsync(
        long orderId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"store/order/{orderId}",
                Options = options,
            },
            cancellationToken
        );
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<object>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<object>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new BitadventurerApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }
}
