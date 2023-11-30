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
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        bool Add(MovieModel model);
        bool Update(MovieModel model);
        bool Delete(int id);
        List<MovieModel> GetList();
        MovieModel GetItem(int id);
    }

    public class MovieService : IMovieService
    {
        private readonly Db _db;

        public MovieService(Db db)
        {
            _db = db;
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies
                .Select(m => new MovieModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Year = m.Year,
                    Revenue = m.Revenue,
                    DirectorId = m.DirectorId,
                    DirectorOutput = m.Director.Name,
                    GenrasNamesOutput = string.Join(", ", m.MovieGenras.Select(mg => mg.Genra.Name)),
                    GenrasIdsInput = m.MovieGenras.Select(mg => mg.GenraId).ToList()
                });
        }

        public bool Add(MovieModel model)
        {
            if (_db.Movies.Any(m => m.Name == model.Name))
            {
                return false;
            }

            var newMovie = new Movie
            {
                Name = model.Name,
                Year = model.Year,
                Revenue = model.Revenue,
                DirectorId = model.DirectorId,
                MovieGenras = model.GenrasIdsInput
                    .Select(gid => new MovieGenra { GenraId = gid })
                    .ToList()
            };

            _db.Movies.Add(newMovie);
            _db.SaveChanges();

            return true;
        }

        public bool Update(MovieModel model)
        {
            var movie = _db.Movies.Include(m => m.MovieGenras).SingleOrDefault(m => m.Id == model.Id);
            if (movie == null)
            {
                return false;
            }

            movie.Name = model.Name;
            movie.Year = model.Year;
            movie.Revenue = model.Revenue;
            movie.DirectorId = model.DirectorId;

            movie.MovieGenras.Clear();
            if (model.GenrasIdsInput != null)
            {
                movie.MovieGenras = model.GenrasIdsInput
                    .Select(gid => new MovieGenra { MovieId = movie.Id, GenraId = gid })
                    .ToList();
            }

            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return false;
            }

            _db.Movies.Remove(movie);
            _db.SaveChanges();

            return true;
        }

        public List<MovieModel> GetList()
        {
            return Query().ToList();
        }

        public MovieModel GetItem(int id)
        {
            return Query().SingleOrDefault(m => m.Id == id);
        }
    }

}
