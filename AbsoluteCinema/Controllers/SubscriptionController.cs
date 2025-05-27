using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize]  
    public class SubscriptionController : Controller
    {
        private readonly SubscriptionBL _subscription = new SubscriptionBL();

        [HttpGet]
        public ActionResult Subscribe()
        {
            if (_subscription.HasActive())
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }

            
            return View(new PaymentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(PaymentViewModel vm)
        {
            if (_subscription.HasActive())
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }

            if (!ModelState.IsValid)
                return View(vm);

           
            var dto = new PaymentModel
            {
                CardNumber = vm.CardNumber,
                ExpiryDate = vm.ExpiryDate,
                CVV = vm.CVV,
                SubscriptionType = vm.SubscriptionType
            };

            _subscription.Subscribe(dto);
            return RedirectToAction("Index", "Profile");
        }
    }
}
