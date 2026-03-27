using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using System.Data;

namespace Interview_Test.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly InterviewTestDbContext _context;

    public PermissionRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public List<PermissionModel> GetPermission()
    {
        return _context.PermissionTb.ToList();
    }

    public int CreatePermission(PermissionModel permission)
    {
        bool exists = _context.PermissionTb.Any(x =>x.Permission == permission.Permission);

        if (exists)
            throw new Exception("Permission already exists.");

        _context.PermissionTb.Add(permission);
        return _context.SaveChanges();
    }
}