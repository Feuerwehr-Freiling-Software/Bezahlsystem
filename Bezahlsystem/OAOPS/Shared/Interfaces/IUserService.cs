using OAOPS.Client.DTO;
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
        public Task<List<PaymentDto>> GetAllPaymentsFiltered(DateTime? fromDate = null, DateTime? toDate = null, string? category = null, double? minAmount = null, double? maxAmount = null);
        public Task<List<TopUpDto>> GetAllTopupsFiltered(string username, DateTime? fromDate, DateTime? toDate, string? executor, double? amount);
        public Task<List<UserDto>> GetAllUsers();
        public Task<List<RoleDto>> GetRoles();
        public Task<double> GetUserBalance(string username);
        public Task<List<UserDto>> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null);
        public Task<UserStatsDto> GetUserStats(string username);
        public Task<ErrorDto?> UpdateUser(UserDto user, string executor);
    }
}
