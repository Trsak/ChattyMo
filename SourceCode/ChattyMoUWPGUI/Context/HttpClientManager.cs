using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChattyMoUWPGUI.Context;

internal static class HttpClientManager
{
    public static HttpClient Client = CreateHttpClient();

    private static HttpClient CreateHttpClient()
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("http://localhost:8080/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        return client;
    }
}