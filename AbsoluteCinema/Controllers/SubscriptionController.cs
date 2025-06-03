using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscription _subscriptionService = new SubscriptionBL();


        [HttpGet]
        public ActionResult Subscribe()
        {
            
            if (_subscriptionService.HasActive())
            {
                return RedirectToAction(nameof(AlreadySubscribed));
            }

         
            return View(new PaymentViewModel());
        }

        // POST: /Subscription/Subscribe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(PaymentViewModel paymentViewModel)
        {
            
            if (_subscriptionService.HasActive())
            {
                return RedirectToAction(nameof(AlreadySubscribed));
            }

            
            if (!ModelState.IsValid)
            {
                return View(paymentViewModel);
            }

            
            var paymentRequest = new PaymentModel
            {
                CardNumber = paymentViewModel.CardNumber,
                ExpiryDate = paymentViewModel.ExpiryDate,
                CVV = paymentViewModel.CVV,
                SubscriptionType = paymentViewModel.SubscriptionType
            };

            
            _subscriptionService.Subscribe(paymentRequest);

            
            return RedirectToAction(nameof(AlreadySubscribed));
        }

        
        [HttpGet]
        public ActionResult AlreadySubscribed()
        {
            
            if (!_subscriptionService.HasActive())
            {
                return RedirectToAction(nameof(Subscribe));
            }

           
            return View();
        }
    }
}
