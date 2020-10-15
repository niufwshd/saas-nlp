using GovTown.Core.Domain.PermissionInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovTown.Services.Permission
{
    public interface IPermissionService
    {
        void InsterPermission(List<PermissionInfo> list);

        void UpdataPermission(List<PermissionInfo> list);

        IQueryable<PermissionInfo> GetPermission();

        IEnumerable<PermissionInfo> GetPermissionById(int? Id);

        PermissionInfo GetPermissionByMenuIdAndRoleId(int? MenuId,int? RoleId);

        int GetPermissionCountById(int? Id);

        void DeletePermission(int? Id);
    }
}
