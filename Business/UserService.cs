using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class UserService
	{
		//TODO: string role naar bool IsAdmin in UserDto en User.cs 
		//TODO: checken wat dadelijk de daadwerkelijke benaming is.
		
		public bool checkIfAdmin()
		{
			//Repository repository =  new Repository():

			UserDTO userDto = new UserDTO();
			//userDto = repository.GetUser();
			
			if (userDto.Role.Name == "Admin")
			{
				return true;
			}else
			{
				return false;
			}
		}

		public User GetById(int id)
		{
			UserDTO userDto = new UserDTO();
			//userDto = UserRepository.GetById(int id); // TODO: repository methode toevoegen.

			// RoleDTO roleDto = new RoleDTO();
			// user.Role = new Role(roleDto.Id, roleDto.Name); 
			
			User user = new User(userDto.Id, userDto.Name);

			user.Role = new Role(userDto.Role.Id, userDto.Role.Name); 
			
			return user;
		}
	}
}
