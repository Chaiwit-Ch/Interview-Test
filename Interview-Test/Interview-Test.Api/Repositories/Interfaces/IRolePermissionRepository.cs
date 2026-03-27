using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IRolePermissionRepository
{
    List<RolePermissionMappingModel> GetRolePermission();
    int CreateRolePermission(RolePermissionMappingModel rolePermission);
}