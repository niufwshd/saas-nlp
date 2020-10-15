using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovTown.Core.Domain.RoleInfos;
using GovTown.Core.Data;
using GovTown.Services.Role;

namespace GovTown.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleInfo> _roleRepo;

        public RoleService(IRepository<RoleInfo> roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public IQueryable<RoleInfo> GetRole()
        {
            var RoleList = _roleRepo.Get();

            return RoleList;
        }

        public RoleInfo GetRoleDetail(int? Id)
        {
            var role = _roleRepo.GetSingle(u => u.Id == Id);
            return role;
        }

        public void InsterRole(RoleInfo info)
        {
            _roleRepo.Insert(info);
        }

        public void UpdateRole(RoleInfo info)
        {
            _roleRepo.Update(info);
        }

        public void DeleteRole(RoleInfo info)
        {
            _roleRepo.Delete(info);
        }

        public int GetRoleId(string roleName, string roleRemark)
        {
            var role = _roleRepo.GetSingle(u => u.RoleName == roleName && u.RolerReamrk == roleRemark);
            if (role != null && role.Id > 0)
            {
                return role.Id;
            }
            return 0;
        }

        public int CheckRoleName(string rolename)
        {
            var role = _roleRepo.GetSingle(u => u.RoleName == rolename);
            if (role != null && role.Id > 0)
            {
                return role.Id;
            }
            return 0;
        }
    }
}
