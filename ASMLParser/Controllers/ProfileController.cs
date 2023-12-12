using ASMLXMLParser.Models;
using Business;
using DAL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ASMLXMLParser.Controllers
{
    public class ProfileController : Controller
    {
        UserService userService = new UserService();
        public IActionResult Index()
        {
            RoleViewModel role = new RoleViewModel(1, "Admin");
            UserViewModel admin = new UserViewModel(1, "Tim", role);
            List<User> allUsers = userService.GetAll();
            List<UserViewModel> usersView = new List<UserViewModel>();
            foreach (User user in allUsers)
            {
                RoleViewModel roleView = new RoleViewModel(user.Role.Id, user.Role.Name);
                UserViewModel userView = new UserViewModel(user.Id, user.Name, roleView);
                usersView.Add(userView);
            }
            ProfileViewModel profile = new ProfileViewModel(admin, usersView);
            return View(profile);
        }
        public IActionResult EditAdmin(int id)
        {
            User user = userService.GetById(id);
            RoleViewModel roleModel = new RoleViewModel(user.Role.Id, user.Role.Name);
            UserViewModel userModel = new UserViewModel(user.Id, user.Name, roleModel);
            List<Role> allRoles = userService.GetAllRoles();
            List<RoleViewModel> allRoleModels = new List<RoleViewModel>();
            foreach(Role role in allRoles)
            {
                RoleViewModel tempRoleModel = new RoleViewModel(role.Id, role.Name);
                allRoleModels.Add(tempRoleModel);
            }
            EditViewModel editViewModel = new EditViewModel(userModel, allRoleModels);
            return View(editViewModel);
        }
    }
}
