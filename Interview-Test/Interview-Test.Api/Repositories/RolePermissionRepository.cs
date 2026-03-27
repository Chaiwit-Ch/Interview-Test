using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using System.Data;

namespace Interview_Test.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly InterviewTestDbContext _context;

    public RolePermissionRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public List<RolePermissionMappingModel> GetRolePermission()
    {
        return _context.RolePermissionMappingTb.ToList();
    }

    public int CreateRolePermission(RolePermissionMappingModel rolePermission)
    {
        bool exists = _context.RolePermissionMappingTb.Any(x =>
                        x.RoleId == rolePermission.RoleId &&
                        x.PermissionId == rolePermission.PermissionId);

        if (exists)
            throw new Exception("Role-Permission mapping already exists.");

        _context.RolePermissionMappingTb.Add(rolePermission);
        return _context.SaveChanges();
    }
}