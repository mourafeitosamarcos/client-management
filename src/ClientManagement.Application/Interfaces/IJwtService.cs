using ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(
            Client client,
            IEnumerable<string> roles,
            int? expiryMinutes = null);
        Task<bool> ValidateToken(string token);
        Task<IDictionary<string, string>> GetClaims(string token);
    }
}
