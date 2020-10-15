
using GovTown.Core.Domain.DeptInfos;
using GovTown.Core.Domain.UserInfos;
using System.Linq;

namespace GovTown.Services.User
{
    public interface IUserService
    {
        int Authenticate(string userName, string password);

        IQueryable<UserInfo> GetUser();

        IQueryable<UserInfo> GetMyself(int? Id);
        void InsterUser(UserInfo info);

        void UpdateUser(UserInfo info);

        UserInfo GetUserDetail(int? Id);

        void DeleteUser(UserInfo info);

        UserInfo GetUserById(int? id);

        IQueryable<DeptInfo> GetDept();

        int CheckUserName(string username);
    }
}
