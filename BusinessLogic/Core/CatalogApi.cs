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
                    .ToList();
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
