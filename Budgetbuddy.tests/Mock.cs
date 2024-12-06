using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Budgetbuddy.tests;

public class MockHttpMessageHandler : HttpMessageHandler
{
    public readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsyncFunc;


    public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsyncFunc)
    {
        _sendAsyncFunc = sendAsyncFunc;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _sendAsyncFunc(request, cancellationToken);
    }
    public virtual Task<HttpResponseMessage> Handle(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}
