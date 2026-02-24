
using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Interfaces.Users;
using MindCarePro.Domain.Entities.Users;
using MindCarePro.Infrastructure.Persistence;


namespace MindCarePro.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            
            var normalizedEmail = email.Trim().ToLower();
            
            return await _context.Set<User>()
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
        }
    }
}

