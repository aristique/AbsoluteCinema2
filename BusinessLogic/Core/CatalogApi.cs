using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class CatalogApi
    {
        public IQueryable<Movie> QueryMovies(WebDbContext db)
            => db.Movies
                .Include("Genres.Genre")
                .Include("Actors.Actor")
                .Include("Directors.Director");

        public List<Genre> GetGenres(WebDbContext db)
            => db.Genres.ToList();
    }
}
