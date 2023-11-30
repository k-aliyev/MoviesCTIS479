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
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
        bool Add(DirectorModel model);
        bool Update(DirectorModel model);
        bool Delete(int id);
    }

    public class DirectorService : IDirectorService
    {
        #region Db Constructor Injection
        private readonly Db _db;

        public DirectorService(Db db)
        {
            _db = db;
        }
        #endregion

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors
                .Select(d => new DirectorModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Surname = d.Surname,
                    BirthDate = d.BirthDate,
                    BirthDateOutput = d.BirthDate.HasValue ? d.BirthDate.Value.ToString("yyyy-MM-dd") : "N/A",
                    IsRetired = d.IsRetired
                });
        }

        public bool Add(DirectorModel model)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == model.Name.Trim().ToUpper() &&
                           d.Surname.ToUpper() == model.Surname.Trim().ToUpper()))
                return false;

            Director directorEntity = new Director()
            {
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim(),
                BirthDate = model.BirthDate,
                IsRetired = model.IsRetired
            };

            _db.Directors.Add(directorEntity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(DirectorModel model)
        {
            var existingDirector = _db.Directors.SingleOrDefault(d => d.Id == model.Id);
            if (existingDirector is null)
                return false;

            existingDirector.Name = model.Name.Trim();
            existingDirector.Surname = model.Surname.Trim();
            existingDirector.BirthDate = model.BirthDate;
            existingDirector.IsRetired = model.IsRetired;

            _db.Directors.Update(existingDirector);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var directorEntity = _db.Directors.SingleOrDefault(d => d.Id == id);
            if (directorEntity is null)
                return false;

            _db.Directors.Remove(directorEntity);
            _db.SaveChanges();

            return true;
        }
    }
}
