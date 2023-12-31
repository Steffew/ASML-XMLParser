﻿using DAL.DTO;
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

        public bool checkIfAdmin()
        {
            //Repository repository =  new Repository():

            UserDTO userDto = new();
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

        public User GetById(int id)
        {
            UserDTO userDto = new();
            //userDto = UserRepository.GetById(int id); // TODO: repository methode toevoegen.

            // RoleDTO roleDto = new RoleDTO();
            // user.Role = new Role(roleDto.Id, roleDto.Name); 

            User user = new User(userDto.Id, userDto.Name);

            user.Role = new Role(userDto.Role.Id, userDto.Role.Name);

            return user;
        }

        public void UpdateUserRole(int userId, Role role)
        {
            //UserRepository userRepository = new UserRepository();

            RoleDTO roleDto = new(role.Id, role.Name);

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

            List<UserDTO> userDtos = new List<UserDTO>();
            UserRepository userRepository = new UserRepository();
            userDtos = userRepository.GetUsersAndRoles(); // TODO: repository methode toevoegen.

            foreach (var userDto in userDtos)
            {
                User newUser = new(userDto.Id, userDto.Name);
                Role newRole = new(userDto.Role.Id, userDto.Role.Name);

                newUser.Role = newRole;
                users.Add(newUser);
            }

            return users;
        }
    }
}