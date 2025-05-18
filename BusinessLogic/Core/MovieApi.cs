using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{

    public class MovieApi
    {
        public List<Movie> GetAll(WebDbContext db) =>
           db.Movies
             .Include(m => m.Genres.Select(g => g.Genre))
             .Include(m => m.Actors.Select(a => a.Actor))
             .Include(m => m.Directors.Select(d => d.Director))
             .ToList();
        public Movie Find(WebDbContext db, Guid id) =>
             db.Movies
               .Include(m => m.Genres.Select(g => g.Genre))
               .Include(m => m.Actors.Select(a => a.Actor))
               .Include(m => m.Directors.Select(d => d.Director))
               .FirstOrDefault(m => m.Id == id);
        public void Save(WebDbContext db) => db.SaveChanges();
    }
}
