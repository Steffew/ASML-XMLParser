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
			
			if (userDto.Role == "Admin")
			{
				return true;
			}else
			{
				return false;
			}
		}
	}
}
