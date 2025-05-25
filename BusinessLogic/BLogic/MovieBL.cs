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
        public List<Movie> GetAll()
        {
            return GetAlll();
        }

        public Movie Get(Guid id)
        {
            return Findd(id);
        }

        public Movie Create(Movie movie, List<Guid> genreIds, List<Guid> actorIds, List<Guid> directorIds, string youtubeId)
        {
            movie.Genres = genreIds?.Select(g => new MovieGenre { GenreId = g }).ToList() ?? new List<MovieGenre>();
            movie.Actors = actorIds?.Select(a => new MovieActor { ActorId = a }).ToList() ?? new List<MovieActor>();
            movie.Directors = directorIds?.Select(d => new MovieDirector { DirectorId = d }).ToList() ?? new List<MovieDirector>();
            movie.YouTubeVideoId = youtubeId;

            return SaveMoviee(movie);
        }

        public void Update(Movie movie, List<Guid> genreIds, List<Guid> actorIds, List<Guid> directorIds)
        {
            UpdateMoviee(movie, genreIds, actorIds, directorIds);
        }

        public List<Genre> GetAvailableGenres()
        {
            return GetAvailableGenress();
        }

        public List<Actor> GetAvailableActors()
        {
            return GetAvailableActorss();
        }

        public List<Director> GetAvailableDirectors()
        {
            return GetAvailableDirectorss();
        }

        public void Delete(Guid id)
        {
            DeleteMoviee(id);
        }
    }
}
