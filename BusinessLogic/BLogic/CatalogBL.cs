using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class CatalogBL : CatalogApi, ICatalog
    {
        private readonly WebDbContext _db;

        public CatalogBL(WebDbContext db)
        {
            _db = db;
        }

        public List<Movie> GetAll(string genre = null)
        {
            var query = QueryMovies(_db);
            if (!string.IsNullOrEmpty(genre))
                query = query.Where(m => m.Genres.Any(g => g.Genre.Name == genre));
            return query.ToList();
        }

        public Movie GetById(Guid id)
        {
            return QueryMovies(_db).FirstOrDefault(m => m.Id == id);
        }

        public List<Genre> GetGenres()
        {
            return GetGenres(_db);
        }
    }
}
