using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class MovieApi
    {
        public List<Movie> GetAlll()
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


        public Movie Findd(Guid id)
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


        public Movie SaveMoviee(Movie movie)
        {
            using (var db = new WebDbContext())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return movie;
            }
        }

        public void UpdateMoviee(Movie updated, List<Guid> genreIds, List<Guid> actorIds, List<Guid> directorIds)
        {
            using (var db = new WebDbContext())
            {
                var existing = db.Movies
                                 .Include("Genres")
                                 .Include("Actors")
                                 .Include("Directors")
                                 .FirstOrDefault(m => m.Id == updated.Id);
                if (existing == null) return;

                existing.Title = updated.Title;
                existing.Year = updated.Year;
                existing.Country = updated.Country;
                existing.Description = updated.Description;
                existing.YouTubeVideoId = updated.YouTubeVideoId;

                db.MovieGenres.RemoveRange(existing.Genres);
                db.MovieActors.RemoveRange(existing.Actors);
                db.MovieDirectors.RemoveRange(existing.Directors);

                existing.Genres = genreIds?.Select(g => new MovieGenre { GenreId = g }).ToList() ?? new List<MovieGenre>();
                existing.Actors = actorIds?.Select(a => new MovieActor { ActorId = a }).ToList() ?? new List<MovieActor>();
                existing.Directors = directorIds?.Select(d => new MovieDirector { DirectorId = d }).ToList() ?? new List<MovieDirector>();

                db.SaveChanges();
            }
        }

        public void DeleteMoviee(Guid id)
        {
            using (var db = new WebDbContext())
            {
                var movie = db.Movies.Find(id);
                if (movie != null)
                {
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                }
            }
        }

        public List<Genre> GetAvailableGenress()
        {
            using (var db = new WebDbContext())
            {
                return db.Genres.ToList();
            }
        }

        public List<Actor> GetAvailableActorss()
        {
            using (var db = new WebDbContext())
            {
                return db.Actors.ToList();
            }
        }


        public List<Director> GetAvailableDirectorss()
        {
            using (var db = new WebDbContext())
            {
                return db.Directors.ToList();
            }
        }
    }
}
