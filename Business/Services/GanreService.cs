using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IGanreService
    {
        IQueryable<GanreModel> Query();
        bool Add(GanreModel model);
        bool Delete(int id);
    }

    public class GanreService : IGanreService
    {
        #region Db Constructor Injection
        private readonly Db _db;

        public GanreService(Db db)
        {
            _db = db;
        }
        #endregion

        public IQueryable<GanreModel> Query()
        {
            return _db.Ganres
                .Select(g => new GanreModel()
                {
                    Id = g.Id,
                    Name = g.Name
                });
        }

        public bool Add(GanreModel model)
        {
            if (_db.Ganres.Any(g => g.Name.ToUpper() == model.Name.Trim().ToUpper()))
                return false;

            Ganre ganreEntity = new Ganre()
            {
                Name = model.Name.Trim()
            };

            _db.Ganres.Add(ganreEntity);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var ganreEntity = _db.Ganres.SingleOrDefault(g => g.Id == id);
            if (ganreEntity is null)
                return false;

            _db.Ganres.Remove(ganreEntity);
            _db.SaveChanges();

            return true;
        }
    }
}
