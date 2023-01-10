using System.Net.Http.Headers;
using System.Web.Http;
using Toolbelt.Blazor;

namespace Paymentsystem.Client.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly NavigationManager nav;

        public HttpInterceptorService(HttpClientInterceptor interceptor, RefreshTokenService refreshTokenService, NavigationManager nav)
        {
            _interceptor = interceptor;
            _refreshTokenService = refreshTokenService;
            this.nav = nav;
        }

        public void RegisterEvent()
        {
            _interceptor.AfterSend += InterceptResponse;
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        }

        private void InterceptResponse(object? sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if(!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;

                switch (statusCode)
                {
                    case System.Net.HttpStatusCode.Continue:
                        break;
                    case System.Net.HttpStatusCode.SwitchingProtocols:
                        break;
                    case System.Net.HttpStatusCode.Processing:
                        break;
                    case System.Net.HttpStatusCode.EarlyHints:
                        break;
                    case System.Net.HttpStatusCode.OK:
                        break;
                    case System.Net.HttpStatusCode.Created:
                        break;
                    case System.Net.HttpStatusCode.Accepted:
                        break;
                    case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        break;
                    case System.Net.HttpStatusCode.ResetContent:
                        break;
                    case System.Net.HttpStatusCode.PartialContent:
                        break;
                    case System.Net.HttpStatusCode.MultiStatus:
                        break;
                    case System.Net.HttpStatusCode.AlreadyReported:
                        break;
                    case System.Net.HttpStatusCode.IMUsed:
                        break;
                    case System.Net.HttpStatusCode.Ambiguous:
                        break;
                    case System.Net.HttpStatusCode.Moved:
                        break;
                    case System.Net.HttpStatusCode.Found:
                        break;
                    case System.Net.HttpStatusCode.RedirectMethod:
                        break;
                    case System.Net.HttpStatusCode.NotModified:
                        break;
                    case System.Net.HttpStatusCode.UseProxy:
                        break;
                    case System.Net.HttpStatusCode.Unused:
                        break;
                    case System.Net.HttpStatusCode.RedirectKeepVerb:
                        break;
                    case System.Net.HttpStatusCode.PermanentRedirect:
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        nav.NavigateTo("/login");
                        message = "User is not authorized";
                        break;
                    case System.Net.HttpStatusCode.PaymentRequired:
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        break;
                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        break;
                    case System.Net.HttpStatusCode.NotAcceptable:
                        break;
                    case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        break;
                    case System.Net.HttpStatusCode.Gone:
                        break;
                    case System.Net.HttpStatusCode.LengthRequired:
                        break;
                    case System.Net.HttpStatusCode.PreconditionFailed:
                        break;
                    case System.Net.HttpStatusCode.RequestEntityTooLarge:
                        break;
                    case System.Net.HttpStatusCode.RequestUriTooLong:
                        break;
                    case System.Net.HttpStatusCode.UnsupportedMediaType:
                        break;
                    case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                        break;
                    case System.Net.HttpStatusCode.ExpectationFailed:
                        break;
                    case System.Net.HttpStatusCode.MisdirectedRequest:
                        break;
                    case System.Net.HttpStatusCode.UnprocessableEntity:
                        break;
                    case System.Net.HttpStatusCode.Locked:
                        break;
                    case System.Net.HttpStatusCode.FailedDependency:
                        break;
                    case System.Net.HttpStatusCode.UpgradeRequired:
                        break;
                    case System.Net.HttpStatusCode.PreconditionRequired:
                        break;
                    case System.Net.HttpStatusCode.TooManyRequests:
                        break;
                    case System.Net.HttpStatusCode.RequestHeaderFieldsTooLarge:
                        break;
                    case System.Net.HttpStatusCode.UnavailableForLegalReasons:
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        break;
                    case System.Net.HttpStatusCode.NotImplemented:
                        break;
                    case System.Net.HttpStatusCode.BadGateway:
                        break;
                    case System.Net.HttpStatusCode.ServiceUnavailable:
                        break;
                    case System.Net.HttpStatusCode.GatewayTimeout:
                        break;
                    case System.Net.HttpStatusCode.HttpVersionNotSupported:
                        break;
                    case System.Net.HttpStatusCode.VariantAlsoNegotiates:
                        break;
                    case System.Net.HttpStatusCode.InsufficientStorage:
                        break;
                    case System.Net.HttpStatusCode.LoopDetected:
                        break;
                    case System.Net.HttpStatusCode.NotExtended:
                        break;
                    case System.Net.HttpStatusCode.NetworkAuthenticationRequired:
                        break;
                    default:
                        break;
                }

                throw new Exception(statusCode.ToString() + message);
            }
        }

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("token") && !absPath.Contains("accounts"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                }
            }
        }
        public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
    }
}
