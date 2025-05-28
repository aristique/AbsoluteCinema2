using System;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _adminBL;

        public AdminController()
        {
            
            _adminBL = new AdminBL();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BanUser(Guid userId)
        {
            _adminBL.BanUser(userId);
          
            return RedirectToAction("Index", "UserList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserRole(Guid userId, Guid roleId)
        {
            _adminBL.ChangeUserRole(userId, roleId);
            return RedirectToAction("Index", "UserList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(Guid commentId, Guid movieId)
        {
            _adminBL.DeleteComments(commentId);
            
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GrantSubscription(Guid userId, string subscriptionType)
        {
            _adminBL.GrantSubscription(userId, subscriptionType);
            return RedirectToAction("Index", "UserList");
        }
    }
    }