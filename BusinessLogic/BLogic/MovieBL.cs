using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class MovieBL : MovieApi, IMovie
    {
        private readonly WebDbContext _db;

        public MovieBL(WebDbContext db)
        {
            _db = db;
        }

        public List<Movie> GetAll() => GetAll(_db);

        public Movie Get(Guid id) => Find(_db, id);

        public Movie Create(Movie movie, List<Guid> genreIds, List<Guid> actorIds, List<Guid> directorIds, string youtubeId)
        {
            movie.Genres = genreIds?.Select(g => new MovieGenre { GenreId = g }).ToList() ?? new List<MovieGenre>();
            movie.Actors = actorIds?.Select(a => new MovieActor { ActorId = a }).ToList() ?? new List<MovieActor>();
            movie.Directors = directorIds?.Select(d => new MovieDirector { DirectorId = d }).ToList() ?? new List<MovieDirector>();
            movie.YouTubeVideoId = youtubeId;

            _db.Movies.Add(movie);
            _db.SaveChanges();
            return movie;
        }
        public void Update(Movie movie, List<Guid> genreIds, List<Guid> actorIds, List<Guid> directorIds)
        {
            var existing = Find(_db, movie.Id);
            if (existing == null) return;
            existing.Title = movie.Title;
            existing.Year = movie.Year;
            existing.Country = movie.Country;
            existing.Description = movie.Description;
            existing.YouTubeVideoId = movie.YouTubeVideoId;

            _db.MovieGenres.RemoveRange(existing.Genres);
            _db.MovieActors.RemoveRange(existing.Actors);
            _db.MovieDirectors.RemoveRange(existing.Directors);
            existing.Genres = genreIds?.Select(g => new MovieGenre { GenreId = g }).ToList() ?? new List<MovieGenre>();
            existing.Actors = actorIds?.Select(a => new MovieActor { ActorId = a }).ToList() ?? new List<MovieActor>();
            existing.Directors = directorIds?.Select(d => new MovieDirector { DirectorId = d }).ToList() ?? new List<MovieDirector>();

            Save(_db);
        }


        public void Delete(Guid id)
        {
            var movie = Find(_db, id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
                Save(_db);
            }
        }
    }
}
