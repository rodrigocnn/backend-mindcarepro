using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Application.Interfaces.Users;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}