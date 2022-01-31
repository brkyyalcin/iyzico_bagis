using iyzico_freelancer.data;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iyzico_freelancer.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Options options = new Options();
            options.ApiKey = "sandbox-FEg1X1p3Gm1kpgtQivvyY0kkmr1dzBjz";
            options.SecretKey = "sandbox-u3XjoYGaAP4JGnfqndKtOLRfCeFmflXr";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.ConversationId = "123456789";
            request.Token = "token";
            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
        }
    }
}
