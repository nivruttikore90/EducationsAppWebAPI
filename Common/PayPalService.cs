using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PayPal.Api;

namespace Common
{
    public class PayPalService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _mode;

        public PayPalService(IConfiguration configuration)
        {
            var paypalSettings = configuration.GetSection("PayPal");
            _clientId = paypalSettings["ClientId"];
            _clientSecret = paypalSettings["ClientSecret"];
            _mode = paypalSettings["Mode"];
        }

        // Initialize PayPal API context
        public APIContext GetAPIContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", _mode }
            };

            var accessToken = new OAuthTokenCredential(_clientId, _clientSecret, config).GetAccessToken();
            var apiContext = new APIContext(accessToken)
            {
                Config = config
            };

            return apiContext;
        }
    }

}
