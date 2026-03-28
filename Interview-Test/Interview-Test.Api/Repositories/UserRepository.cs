using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;

namespace Interview_Test.Repositories;

public class UserRepository : IUserRepository
{
    private readonly InterviewTestDbContext _context;

    public UserRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public dynamic GetUsers()
    {
        var users = _context.UserTb
            .Select(u => new
            {
                id = u.Id,
                userId = u.UserId,
                username = u.Username,
                firstName = u.UserProfile.FirstName,
                lastName = u.UserProfile.LastName,
                age = u.UserProfile.Age,

                roles = u.UserRoleMappings
                    .Select(urm => new
                    {
                        roleId = urm.Role.RoleId,
                        roleName = urm.Role.RoleName
                    })
                    .Distinct()
                    .ToList(),

                permissions = u.UserRoleMappings
                    .SelectMany(urm => urm.Role.RolePermissionMappings)
                    .Select(rp => rp.Permission.Permission)
                    .Distinct()
                    .ToList()
            })
            .ToList();

        return users;
    }

    public dynamic GetUserById(string id)
    {
        var user = _context.UserTb
            .Where(u => u.UserId == id)
            .Select(u => new
            {
                id = u.Id,
                userId = u.UserId,
                username = u.Username,
                firstName = u.UserProfile.FirstName,
                lastName = u.UserProfile.LastName,
                age = u.UserProfile.Age,

                roles = u.UserRoleMappings
                    .Select(urm => new
                    {
                        roleId = urm.Role.RoleId,
                        roleName = urm.Role.RoleName
                    })
                    .Distinct()
                    .ToList(),

                permissions = u.UserRoleMappings
                    .SelectMany(urm => urm.Role.RolePermissionMappings)
                    .Select(rp => rp.Permission.Permission)
                    .Distinct()
                    .ToList()
            })
            .FirstOrDefault();

        return user;
    }

    public int CreateUser(UserModel? user)
    {
        foreach (var dataUser in Data.Users)
        {
            bool userExists = _context.UserTb.Any(x =>
                x.Id == dataUser.Id || x.UserId == dataUser.UserId);

            if (userExists)
                continue;

            var newUser = new UserModel
            {
                Id = dataUser.Id,
                UserId = dataUser.UserId,
                Username = dataUser.Username,
                UserProfile = new UserProfileModel
                {
                    FirstName = dataUser.UserProfile.FirstName,
                    LastName = dataUser.UserProfile.LastName,
                    Age = dataUser.UserProfile.Age
                },
                UserRoleMappings = new List<UserRoleMappingModel>()
            };

            foreach (var dataUserRole in dataUser.UserRoleMappings)
            {
                if (dataUserRole.Role == null)
                    continue;

                int roleIdFromData = dataUserRole.Role.RoleId;

                var role = _context.RoleTb
                    .FirstOrDefault(r => r.RoleId == roleIdFromData);

                if (role == null)
                    continue;

                bool mappingExists = newUser.UserRoleMappings
                    .Any(x => x.Role.RoleId == role.RoleId);

                if (mappingExists)
                    continue;

                newUser.UserRoleMappings.Add(new UserRoleMappingModel
                {
                    Role = role
                });
            }

            _context.UserTb.Add(newUser);
        }

        return _context.SaveChanges();
    }
}