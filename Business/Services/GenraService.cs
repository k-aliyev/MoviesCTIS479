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
    public interface IGenraService
    {
        IQueryable<GenraModel> Query();
        bool Add(GenraModel model);
        bool Delete(int id);
    }

    public class GenraService : IGenraService
    {
        #region Db Constructor Injection
        private readonly Db _db;

        public GenraService(Db db)
        {
            _db = db;
        }
        #endregion

        public IQueryable<GenraModel> Query()
        {
            return _db.Genras
                .Select(g => new GenraModel()
                {
                    Id = g.Id,
                    Name = g.Name
                });
        }

        public bool Add(GenraModel model)
        {
            if (_db.Genras.Any(g => g.Name.ToUpper() == model.Name.Trim().ToUpper()))
                return false;

            Genra genraEntity = new Genra()
            {
                Name = model.Name.Trim()
            };

            _db.Genras.Add(genraEntity);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var genraEntity = _db.Genras.Include(g => g.MovieGenras).SingleOrDefault(g => g.Id == id);
            if (genraEntity is null)
                return false;

            // Remove related records from the joining table
            if (genraEntity.MovieGenras != null)
            {
                _db.MovieGenras.RemoveRange(genraEntity.MovieGenras);
            }

            // Now remove the genre itself
            _db.Genras.Remove(genraEntity);
            _db.SaveChanges();

            return true;
        }

    }
}
