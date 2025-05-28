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
   
        public void RecordDetailsVieww(Guid movieId)
        {
            using (var db = new WebDbContext())
            {
                var movie = db.Movies.Find(movieId);
                if (movie != null)
                {
                    movie.DetailsViewCount++;
                    db.SaveChanges();
                }
            }
        }
        public List<Movie> GetTopPopularMoviess(int top = 4)
        {
            using (var db = new WebDbContext())
            {
                return db.Movies
                         .OrderByDescending(m => m.DetailsViewCount)
                         .Take(top)
                         .ToList();
            }
        }
        public void IncrementDetailsViewCountt(Guid movieId)
        {
            using (var db = new WebDbContext())
            {
                var movie = db.Movies.Find(movieId);
                if (movie != null)
                {
                    movie.DetailsViewCount++;
                    db.SaveChanges();
                }
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
