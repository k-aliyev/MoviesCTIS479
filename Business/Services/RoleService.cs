using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
        bool Add(RoleModel model);
        bool Update(RoleModel model);
        bool Delete(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly Db _db;

        public RoleService(Db db)
        {
            _db = db;
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Include(r => r.Users).OrderBy(r => r.Name).Select(r => new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                UserCountOutput = r.Users.Count 
            });
        }

        public bool Add(RoleModel model)
        {
            
            var nameSqlParameter = new SqlParameter("name", model.Name.Trim()); // using a parameter prevents SQL Injection
            
            var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name)", nameSqlParameter);
            if (query.Any()) 
                return false;

            var entity = new Role()
            {
                Name = model.Name.Trim()
            };
            _db.Roles.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Update(RoleModel model)
        {

            var nameSqlParameter = new SqlParameter("name", model.Name.Trim());
            var idSqlParameter = new SqlParameter("id", model.Id);

            var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name) and Id != @id", nameSqlParameter, idSqlParameter);
            if (query.Any()) 
                return false;

            var entity = new Role()
            {
                Id = model.Id, // must be set
                Name = model.Name.Trim()
            };

           
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {

            var existingEntity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (existingEntity is null)
                return false;
            
            if (existingEntity.Users.Any())
                return false;

            _db.Roles.Remove(existingEntity);
            _db.SaveChanges();
            return true;
        }
    }
}
