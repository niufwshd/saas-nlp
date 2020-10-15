using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovTown.Core.Domain.PermissionInfo;
using GovTown.Core.Data;

namespace GovTown.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<PermissionInfo> _permissionRepo;

        public PermissionService(IRepository<PermissionInfo> permissionRepo)
        {
            _permissionRepo = permissionRepo;
        }

        public IQueryable<PermissionInfo> GetPermission()
        {
            var PermissionList = _permissionRepo.Get();

            return PermissionList;

        }

        public IEnumerable<PermissionInfo> GetPermissionById(int? Id)
        {
            var PermissionList = _permissionRepo.Where<PermissionInfo>(u => u.RoleId == (int)Id);

            return PermissionList;
        }

        public PermissionInfo GetPermissionByMenuIdAndRoleId(int? MenuId, int? RoleId)
        {
            var PermissionList = _permissionRepo.GetSingle(u=>u.MenuId == MenuId && u.RoleId == RoleId);

            return PermissionList;
        }

        public IEnumerable<PermissionInfo> GetPermissionByMenuId(int? MenuId)
        {
            var PermissionList = _permissionRepo.Where<PermissionInfo>(u => u.MenuId == (int)MenuId);

            return PermissionList;
        }

        public int GetPermissionCountById(int? Id)
        {
            var message = 0;
            var PermissionList = _permissionRepo.Where<PermissionInfo>(u => u.RoleId == (int)Id);

            if (PermissionList.ToList().Count == 0)
            {
                return message;
            }
            else {
                message = 1;
                return message;
            }
        }

        public void InsterPermission(List<PermissionInfo> list)
        {
            for (var i = 0; i < list.Count; i++) {
                _permissionRepo.Insert(list[i]);
            }
        }

        public void UpdataPermission(List<PermissionInfo> list)
        {
            PermissionInfo p = new PermissionInfo(); 
            for (var i = 0; i < list.Count; i++)
            {
                p = _permissionRepo.GetSingle(u => u.MenuId == list[i].MenuId && u.RoleId == list[i].RoleId);
                //_permissionRepo.Update(list[i]);
                list[i].Id = p.Id;
            }
            
            _permissionRepo.UpdateRange(list);
        }

        public void DeletePermission(int? Id)
        {
            var pList = GetPermissionById(Id).ToList();

            _permissionRepo.DeleteRange(pList);
        }


    }
}
