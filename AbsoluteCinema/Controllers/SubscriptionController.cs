using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscription _subscription;

        public SubscriptionController(ISubscription subscription)
        {
            _subscription = subscription;
        }

        [HttpGet]
        public ActionResult Subscribe()
        {
            var has = _subscription.HasActive();
            if (has)
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(PaymentViewModel model)
        {
            if (_subscription.HasActive())
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }
            if (!ModelState.IsValid)
                return View(model);

            _subscription.Subscribe(model);
            return RedirectToAction("Index", "Profile");
        }
    }
}