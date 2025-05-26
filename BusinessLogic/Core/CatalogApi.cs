using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class CatalogApi
    {
        public List<Movie> GetAllMoviess()
        {
            using (var db = new WebDbContext())
            {
                return db.Movies
                    .Include("Genres.Genre")
                    .Include("Actors.Actor")
                    .Include("Directors.Director")
                    .Include("Comments.User") 
                    .ToList();
            }
        }
        public Movie GetMovieById(Guid id)
        {
            using (var db = new WebDbContext())
            {
                return db.Movies
                    .Include("Genres.Genre")
                    .Include("Actors.Actor")
                    .Include("Directors.Director")
                    .Include("Comments.User")
                    .FirstOrDefault(m => m.Id == id);
            }
        }
        public List<Genre> GetGenress()
        {
            using (var db = new WebDbContext())
            {
                return db.Genres.ToList();
            }
        }
    }
}
