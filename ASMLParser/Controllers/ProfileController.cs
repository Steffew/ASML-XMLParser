using ASMLXMLParser.Models;
using Business;
using Microsoft.AspNetCore.Mvc;

namespace ASMLXMLParser.Controllers
{
    public class ProfileController : Controller
    {

        public IActionResult Index(UserService userService)
        {
            RoleViewModel role = new RoleViewModel(1, "Admin");
            UserViewModel user = new UserViewModel(1, "Tim", role);

            //hiermee verder
            bool isActive = userService.CheckIfUserIsActive();
            ViewData["IsActive"] = isActive;

            return View(user);
        }
    }
}
