using GovTown.Core.Domain.RoleInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Role
{
    public interface IRoleService
    {
        IQueryable<RoleInfo> GetRole();

        void InsterRole(RoleInfo info);

        void UpdateRole(RoleInfo info);

        RoleInfo GetRoleDetail(int? Id);

        void DeleteRole(RoleInfo info);

        int GetRoleId(string roleName, string roleRemark);

        int CheckRoleName(string rolename);
    }
}
