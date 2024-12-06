using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public partial class PetClient
{
    private RawClient _client;

    internal PetClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Add a new pet to the store
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.AddPetAsync(
    ///     new Pet
    ///     {
    ///         Name = "doggie",
    ///         PhotoUrls = new List&lt;string&gt;() { "photoUrls" },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<Pet> AddPetAsync(
        Pet request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "pet",
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
                return JsonUtils.Deserialize<Pet>(responseBody)!;
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
    /// Update an existing pet by Id
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.UpdatePetAsync(
    ///     new Pet
    ///     {
    ///         Name = "doggie",
    ///         PhotoUrls = new List&lt;string&gt;() { "photoUrls" },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<Pet> UpdatePetAsync(
        Pet request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = "pet",
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
                return JsonUtils.Deserialize<Pet>(responseBody)!;
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
    /// Multiple status values can be provided with comma separated strings
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.FindPetsByStatusAsync(new FindPetsByStatusRequest());
    /// </code>
    /// </example>
    public async Task<IEnumerable<Pet>> FindPetsByStatusAsync(
        FindPetsByStatusRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Status != null)
        {
            _query["status"] = request.Status.Value.Stringify();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "pet/findByStatus",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<IEnumerable<Pet>>(responseBody)!;
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
    /// Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.FindPetsByTagsAsync(new FindPetsByTagsRequest());
    /// </code>
    /// </example>
    public async Task<IEnumerable<Pet>> FindPetsByTagsAsync(
        FindPetsByTagsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["tags"] = request.Tags;
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "pet/findByTags",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<IEnumerable<Pet>>(responseBody)!;
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
    /// Returns a single pet
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.GetPetByIdAsync(1000000);
    /// </code>
    /// </example>
    public async Task<Pet> GetPetByIdAsync(
        long petId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"pet/{petId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Pet>(responseBody)!;
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
    ///
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.UpdatePetWithFormAsync(1000000, new UpdatePetWithFormRequest());
    /// </code>
    /// </example>
    public async Task UpdatePetWithFormAsync(
        long petId,
        UpdatePetWithFormRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.Status != null)
        {
            _query["status"] = request.Status;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = $"pet/{petId}",
                Query = _query,
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
    ///
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Pet.DeletePetAsync(1000000);
    /// </code>
    /// </example>
    public async Task DeletePetAsync(
        long petId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"pet/{petId}",
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
    ///
    /// </summary>
    public async Task<ApiResponse> UploadFileAsync(
        long petId,
        Stream request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.StreamApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = $"pet/{petId}/uploadImage",
                Body = request,
                ContentType = "application/octet-stream",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ApiResponse>(responseBody)!;
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
}
