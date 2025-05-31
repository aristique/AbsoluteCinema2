using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class CatalogBL : CatalogApi, ICatalog
    {
        public List<MovieModel> GetAll()
        {
            var movies = GetAllMoviess();
            return movies.Select(m => new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Country = m.Country,
                YouTubeVideoId = m.YouTubeVideoId,
                Genres = m.Genres.Select(g => new GenreModel
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name
                }).ToList()
            }).ToList();
        }

        public MovieDetailsModel GetById(Guid id)
        {
            var movie = GetMovieById(id);
            if (movie == null)
                return null;

            return new MovieDetailsModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Country = movie.Country,
                Description = movie.Description,
                YouTubeVideoId = movie.YouTubeVideoId,
                Genres = movie.Genres.Select(g => g.Genre.Name).ToList(),
                Actors = movie.Actors.Select(a => a.Actor.Name).ToList(),
                Directors = movie.Directors.Select(d => d.Director.Name).ToList(),
                Comments = movie.Comments
                                .OrderByDescending(c => c.CreatedAt)
                                .Select(c => new CommentModel
                                {
                                    Id = c.Id,
                                    Text = c.Text,
                                    CreatedAt = c.CreatedAt,
                                    UserName = c.User?.Name ?? string.Empty
                                }).ToList()
            };
        }

        public List<MovieModel> GetPopular(int count = 4)
        {
            var popular = GetTopPopularMoviess(count);
            return popular.Select(m => new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Country = m.Country,
                YouTubeVideoId = m.YouTubeVideoId
            }).ToList();
        }

        public List<GenreModel> GetGenres()
        {
            var genres = GetGenress();
            return genres.Select(g => new GenreModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        public void IncrementDetailsViewCount(Guid id)
        {
            IncrementDetailsViewCountt(id); 
        }
    }
}
