using ASMLXMLParser.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASMLXMLParser.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            RoleViewModel role = new RoleViewModel(1, "Admin");
            UserViewModel user = new UserViewModel(1, "Tim", role);
            return View(user);
        }
    }
}
