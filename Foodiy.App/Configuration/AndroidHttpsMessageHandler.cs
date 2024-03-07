using Javax.Net.Ssl;
using System.Net.Security;
using Xamarin.Android.Net;
using Object = Java.Lang.Object;

namespace Foodiy.App.Configuration;

public static class AndroidHttpsMessageHandler
{
    private const string _localIssuer = "CN=localhost";
    private const string _localhost = "10.0.2.2";

    public static HttpMessageHandler Get()
    {
        var handler = new LocalhostAndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) =>
            {
                if (cert?.Issuer == _localIssuer || errors == SslPolicyErrors.None)
                    return true;
                return false;
            }
        };

        return handler;
    }

    private class LocalhostAndroidMessageHandler : AndroidMessageHandler
    {
        protected override IHostnameVerifier? GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new LocalhostHostNameVerifier();
        }

        private class LocalhostHostNameVerifier : Object, IHostnameVerifier
        {
            public bool Verify(string? hostname, ISSLSession? session)
            {
                var verify = HttpsURLConnection.DefaultHostnameVerifier?.Verify(hostname, session);

                if (verify == true || hostname == _localhost)
                    return true;
                return false;
            }
        }
    }
}