using Polly;
using Polly.Extensions.Http;

namespace TopicsApi;

public static class HttpPolicies
{

    public static IAsyncPolicy<HttpResponseMessage> GetDefaultRetryPolicy()
    {
        // So, when an actual network error happens when we call an API where this is applied,
        // if when we call that, that api return a TransientError or a 404, don't just throw an exception to the calling code.
        // Use this rety policy. 
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Exponential Backoff


    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        // this is for AFTER we have tried all the retries, just stop making ANY requests at all.
        // it will cause the HttpClient to just *immediately* return an error without actually calling on the network.
        // It's like "Don't kick a man when he's down"
        return HttpPolicyExtensions
            .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10));
    }
}
