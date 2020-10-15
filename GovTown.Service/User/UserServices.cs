

using GovTown.Core.Data;
using GovTown.Core.Domain.UserInfos;
using System.Linq;
using System;
using GovTown.Core.Domain.DeptInfos;

namespace GovTown.Services.User
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<UserInfo> _userRepo;
        private readonly IRepository<DeptInfo> _deptList;
        
        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserService(IRepository<UserInfo> userRepo, IRepository<DeptInfo> deptList)
        {
            _userRepo = userRepo;
            _deptList = deptList;
        }


        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var user = _userRepo.GetSingle(u => u.UserName == userName && u.Password == password);
            if (user != null && user.Id > 0)
            {
                return user.Id;
            }
            return 0;
        }

        public UserInfo GetUserDetail(int? Id)
        {
            var user = _userRepo.GetSingle(u => u.Id == Id);
            //if (user != null && user.Id > 0)
            //{
            //    return user.Id;
            //}
            //return 0;
            return user;
        }

        public IQueryable<UserInfo> GetUser()
        {
            var UserList = _userRepo.Get();

            return UserList;
        }

        public void InsterUser(UserInfo info)
        {
            _userRepo.Insert(info);
        }

        public void UpdateUser(UserInfo info)
        {
            _userRepo.Update(info);
        }

        public void DeleteUser(UserInfo info)
        {
            _userRepo.Delete(info);
        }

        public IQueryable<DeptInfo> GetDept(){
            var DeptrList = _deptList.Get();

            return DeptrList;
        }

        public UserInfo GetUserById(int? id)
        {
            var User = _userRepo.GetSingle(u=>u.Id ==id);

            return User;
        }

        public int CheckUserName(string username)
        {
            var user = _userRepo.GetSingle(u => u.UserName == username);
            if (user != null && user.Id > 0)
            {
                return user.Id;
            }
            return 0;
        }

        public IQueryable<UserInfo> GetMyself(int? Id)
        {
            var User = _userRepo.Get(u => u.Id == Id);

            return User;
        }
    }
}
