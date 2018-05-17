using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMaker.Services.Apis
{
    public class AuthenticatedNativeMessageHandler : NativeMessageHandler
    {
        public AuthenticatedNativeMessageHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, Helpers.Settings.AccessToken);
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(true);
        }
    }
}
