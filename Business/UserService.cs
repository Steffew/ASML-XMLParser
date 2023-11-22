using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using DAL;

namespace Business
{
    public class UserService
    {
        //TODO: string role naar bool IsAdmin in UserDto en User.cs 
        //TODO: checken wat dadelijk de daadwerkelijke benaming is.
        UserRepository userRepository = new UserRepository();

        public bool checkIfAdmin()
        {
            //Repository repository =  new Repository():

            UserDTO userDto = new UserDTO();
            //userDto = repository.GetUser();

            if (userDto.Role.Name == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetByName(string name)
        {
            UserDTO userDto = new UserDTO();
            userDto = userRepository.LoadUserByName(name); // TODO: repository methode toevoegen.

            User user = new User(userDto.Id, userDto.Name);
            user.Role = new Role(userDto.Role.Id, userDto.Role.Name);

            return user;
        }

        public void UpdateUserRole(int userId, Role role)
        {
            RoleDTO roleDto = new RoleDTO();
            roleDto.Name = role.Name;
            roleDto.Id = role.Id;

            try
            {
                //userRepository.UpdateUserRole(userId, roleDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            List<UserDTO> userDtos = userRepository.LoadAllUsers();
            foreach (UserDTO userDto in userDtos)
            {
                User newUser = new User(userDto.Id, userDto.Name);
                newUser.Role = new Role(userDto.Role.Id, userDto.Role.Name);
                users.Add(newUser);
            }

            return users;
        }
    }
}