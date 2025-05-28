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
        private readonly ISubscription _subscription = new SubscriptionBL();

        [HttpGet]
        public ActionResult Subscribe()
        {
            if (_subscription.HasActive())
                return RedirectToAction("Index", "Profile");

            return View(new PaymentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(PaymentViewModel paymentViewModel)
        {
            if (_subscription.HasActive())
                return RedirectToAction("Index", "Profile");

            if (!ModelState.IsValid)
                return View(paymentViewModel);

            var paymentRequest = new PaymentModel
            {
                CardNumber = paymentViewModel.CardNumber,
                ExpiryDate = paymentViewModel.ExpiryDate,
                CVV = paymentViewModel.CVV,
                SubscriptionType = paymentViewModel.SubscriptionType
            };

            _subscription.Subscribe(paymentRequest);
            return RedirectToAction("Index", "Profile");
        }
    }
}
