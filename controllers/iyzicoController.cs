using iyzico_freelancer.data;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iyzico_freelancer.controllers
{
    [Route("[controller]/[action]")]
    public class iyzicoController : Controller
    {
        public async Task<IActionResult> Odeme([FromBody] BagisBilgi bilgi)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-FEg1X1p3Gm1kpgtQivvyY0kkmr1dzBjz";
            options.SecretKey = "sandbox-u3XjoYGaAP4JGnfqndKtOLRfCeFmflXr";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = bilgi.Price;
            request.PaidPrice = bilgi.Price;
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:5001/iyzico/callBack";

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = bilgi.CardHolderName;
            paymentCard.CardNumber = bilgi.CardNumber;
            paymentCard.ExpireMonth = bilgi.ExpireMonth;
            paymentCard.ExpireYear = bilgi.ExpireYear;
            paymentCard.Cvc = bilgi.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = bilgi.CardHolderName.Split(" ")[0];
            buyer.Surname = bilgi.CardHolderName.Split(" ")[1
                ];
            buyer.GsmNumber = bilgi.phone;
            buyer.Email = bilgi.mail;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Selimiye, Selimiye İskele Caddesi No. 17/B1-2 Üsküdar";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = bilgi.CardHolderName;
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Selimiye, Selimiye İskele Caddesi No. 17/B1-2 Üsküdar";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = bilgi.CardHolderName;
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Selimiye, Selimiye İskele Caddesi No. 17/B1-2 Üsküdar";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Bagis";
            firstBasketItem.Category1 = "bagis";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            firstBasketItem.Price = bilgi.Price;
            basketItems.Add(firstBasketItem);
            request.BasketItems = basketItems;
            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);
           
            if (threedsInitialize.Status == "success")
            {
                return Ok(threedsInitialize);
            }
            else
            {
                return BadRequest(threedsInitialize);
            }
        }

        [HttpPost]
        public async Task<IActionResult> callBack([FromForm] iyzicoStatus iyzicoCallBack)
        {
            if(iyzicoCallBack.status== "success")
            {
                Options options = new Options();
                options.ApiKey = "sandbox-FEg1X1p3Gm1kpgtQivvyY0kkmr1dzBjz";
                options.SecretKey = "sandbox-u3XjoYGaAP4JGnfqndKtOLRfCeFmflXr";
                options.BaseUrl = "https://sandbox-api.iyzipay.com";
                CreateThreedsPaymentRequest request = new CreateThreedsPaymentRequest();
                request.Locale = Locale.TR.ToString();
                request.ConversationId = iyzicoCallBack.conversationId.ToString();
                request.PaymentId = iyzicoCallBack.paymentId.ToString();
                request.ConversationData = iyzicoCallBack.conversationData;

                ThreedsPayment threedsPayment = ThreedsPayment.Create(request, options);
                ViewBag.durum = true;
                ViewBag.odemeDurum = threedsPayment;
                return View("iyzicoCallBackView");

            }
            else
            {
                ViewBag.durum = false;
                return View("iyzicoCallBackView");
            }

        }
    }
}
