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
        public List<Movie> GetAll(string genre = null)
        {
            var movies = GetAllMoviess();
            if (!string.IsNullOrEmpty(genre))
                movies = movies.Where(m => m.Genres.Any(g => g.Genre.Name == genre)).ToList();
            return movies;
        }

        public Movie GetById(Guid id)
        {
            return GetMovieById(id); 
        }

        public List<Genre> GetGenres()
        {
            return GetGenress();
        }
        public void RecordDetailsView(Guid movieId)
        {
            RecordDetailsVieww(movieId);
        }
        public List<Movie> GetPopular(int top = 4)
        {
            return base.GetTopPopularMoviess(top);
        }
        public void IncrementDetailsViewCount(Guid movieId)
        {
            base.IncrementDetailsViewCountt(movieId);
        }


    }
}
