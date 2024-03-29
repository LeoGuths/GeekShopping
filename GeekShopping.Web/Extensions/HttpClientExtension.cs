﻿using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Extensions;

public static class HttpClientExtension
{
    private static readonly MediaTypeHeaderValue ContentType = new ("application/json");
    
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
        Stream dataAsString = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true })!;
    }

    public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
    {
        string dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = ContentType;
        return httpClient.PostAsync(url, content);
    }
    
    public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
    {
        string dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = ContentType;
        return httpClient.PutAsync(url, content);
    }
}