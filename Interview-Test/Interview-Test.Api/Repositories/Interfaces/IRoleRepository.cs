using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IRoleRepository
{
    List<RoleModel> GetRole();
    int CreateRole(RoleModel role);
}