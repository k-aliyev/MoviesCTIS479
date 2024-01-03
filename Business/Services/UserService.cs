using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        IQueryable<UserModel> Query();

        bool Add(UserModel model);
        bool Update(UserModel model);
        bool DeleteUser(int id);
    }

    public class UserService : IUserService 
    {
        #region Db Constructor Injection
        private readonly Db _db;


        public UserService(Db db)
        {
            _db = db;
        }
        #endregion


        public IQueryable<UserModel> Query()
        {

            return _db.Users.Include(e => e.Role).OrderByDescending(e => e.Name)
                .Select(e => new UserModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Password = e.Password,
                    RoleId = e.RoleId,
                    Status = e.Status,

                    RoleNameOutput = e.Role.Name,
                });
        }

        public bool Add(UserModel model)
        {
            List<User> existingUsers = _db.Users.ToList();
            if (existingUsers.Any(u => u.Name.Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                return false;


            User userEntity = new User()
            {
                Name = model.Name.Trim(),
                Password = model.Password.Trim(),
                RoleId = model.RoleId ?? 0,
                Status = model.Status
            };


            _db.Users.Add(userEntity);


            _db.SaveChanges();

            return true;
        }

        public bool Update(UserModel model)
        {
        
            var existingUsers = _db.Users.Where(u => u.Id != model.Id).ToList();
            if (existingUsers.Any(u => u.Name.Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                return false;


            var userEntity = _db.Users.SingleOrDefault(u => u.Id == model.Id);


            if (userEntity is null)
                return false;

            userEntity.Name = model.Name.Trim();
            userEntity.Password = model.Password.Trim();
            userEntity.RoleId = model.RoleId ?? 0;
            userEntity.Status = model.Status;

            _db.Users.Update(userEntity);

            _db.SaveChanges();

            return true;
        }


        // Way 2: a better way
        public bool DeleteUser(int id)
        {

            var userEntity = _db.Users.SingleOrDefault(u => u.Id == id);
            if (userEntity is null)
                return false;

            _db.Users.Remove(userEntity);

            _db.SaveChanges(); 

            return true;
        }
    }
}
