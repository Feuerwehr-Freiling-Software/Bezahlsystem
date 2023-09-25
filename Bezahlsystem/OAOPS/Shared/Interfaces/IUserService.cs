using OAOPS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsers();
        public Task<double> GetUserBalance(string username);
        public Task<List<UserDto>> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null);
    }
}
