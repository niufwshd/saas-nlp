using GovTown.Core.Data;
using GovTown.Core.Domain.MenuInfos;
using GovTown.Services.Menu;
using System.Collections.Generic;
using System.Linq;

namespace GovTown.Services.Menu
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class MenuService : IMenuService
    {
        private readonly IRepository<MenuInfo> _menuRepo;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public MenuService(IRepository<MenuInfo> menuRepo)
        {
            _menuRepo = menuRepo;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IQueryable<MenuInfo> GetMenu()
        {
            var Menu = _menuRepo.Get();

            return Menu;
        }
    }
}
