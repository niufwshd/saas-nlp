using System.Collections.Generic;
using GovTown.Core.Domain.MenuInfos;
using System.Linq;

namespace GovTown.Services.Menu
{
    public interface IMenuService
    {
        IQueryable<MenuInfo> GetMenu();
    }
}
