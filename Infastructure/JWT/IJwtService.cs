using inventory_api.Domain.Entities;

namespace inventory_api.Infastructure.JWT
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
