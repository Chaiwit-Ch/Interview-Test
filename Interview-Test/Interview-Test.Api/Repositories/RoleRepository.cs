using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using System.Data;

namespace Interview_Test.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly InterviewTestDbContext _context;

    public RoleRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public List<RoleModel> GetRole()
    {
        return _context.RoleTb.ToList();
    }

    public int CreateRole(RoleModel role)
    {
        bool exists = _context.RoleTb.Any(x =>x.RoleName == role.RoleName);

        if (exists)
            throw new Exception("Role already exists.");

        _context.RoleTb.Add(role);
        return _context.SaveChanges();
    }
}