using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IPermissionRepository
{
    List<PermissionModel> GetPermission();
    int CreatePermission(PermissionModel permission);
}