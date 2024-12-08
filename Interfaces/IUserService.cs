using api.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Interfaces;

public interface IUserService 
{
    Task<IEnumerable<UserProfileDTO>> GetFriends(int userId);
    Task<object> GetFriendRequests(int userId);
  
}