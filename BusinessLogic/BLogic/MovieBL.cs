using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class MovieBL : MovieApi, IMovie
    {
        public List<MovieModel> GetAll()
        {
            return GetAlll().Select(m => new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Country = m.Country,
                Description = m.Description,
                YouTubeVideoId = m.YouTubeVideoId,
                Genres = m.Genres.Select(g => new GenreModel
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name
                }).ToList(),
                Actors = m.Actors.Select(a => new ActorModel
                {
                    Id = a.Actor.Id,
                    Name = a.Actor.Name
                }).ToList(),
                Directors = m.Directors.Select(d => new DirectorModel
                {
                    Id = d.Director.Id,
                    Name = d.Director.Name
                }).ToList(),
                Comments = m.Comments?.Select(c => new CommentModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User?.Name
                }).ToList()
            }).ToList();
        }

        public MovieModel Get(Guid id)
        {
            var m = Findd(id);
            if (m == null) return null;

            return new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Country = m.Country,
                Description = m.Description,
                YouTubeVideoId = m.YouTubeVideoId,
                Genres = m.Genres.Select(g => new GenreModel
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name
                }).ToList(),
                Actors = m.Actors.Select(a => new ActorModel
                {
                    Id = a.Actor.Id,
                    Name = a.Actor.Name
                }).ToList(),
                Directors = m.Directors.Select(d => new DirectorModel
                {
                    Id = d.Director.Id,
                    Name = d.Director.Name
                }).ToList(),
                Comments = m.Comments?.Select(c => new CommentModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User?.Name
                }).ToList()
            };
        }

        public MovieModel Create(CreateMovieModel model)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Year = model.Year,
                Country = model.Country,
                Description = model.Description,
                YouTubeVideoId = model.YouTubeVideoId,
                Genres = model.SelectedGenres?.Select(g => new MovieGenre { GenreId = g }).ToList() ?? new List<MovieGenre>(),
                Actors = model.SelectedActors?.Select(a => new MovieActor { ActorId = a }).ToList() ?? new List<MovieActor>(),
                Directors = model.SelectedDirectors?.Select(d => new MovieDirector { DirectorId = d }).ToList() ?? new List<MovieDirector>()
            };

            var saved = SaveMoviee(movie);

            return new MovieModel
            {
                Id = saved.Id,
                Title = saved.Title,
                Year = saved.Year,
                Country = saved.Country,
                Description = saved.Description,
                YouTubeVideoId = saved.YouTubeVideoId
            };
        }

        public void Update(Guid id, CreateMovieModel model)
        {
            var movie = Findd(id);
            if (movie == null) return;

            movie.Title = model.Title;
            movie.Year = model.Year;
            movie.Country = model.Country;
            movie.Description = model.Description;
            movie.YouTubeVideoId = model.YouTubeVideoId;

            UpdateMoviee(movie, model.SelectedGenres, model.SelectedActors, model.SelectedDirectors);
        }

        public List<GenreModel> GetAvailableGenres()
        {
            return GetAvailableGenress()
                .Select(g => new GenreModel { Id = g.Id, Name = g.Name })
                .ToList();
        }

        public List<ActorModel> GetAvailableActors()
        {
            return GetAvailableActorss()
                .Select(a => new ActorModel { Id = a.Id, Name = a.Name })
                .ToList();
        }

        public List<DirectorModel> GetAvailableDirectors()
        {
            return GetAvailableDirectorss()
                .Select(d => new DirectorModel { Id = d.Id, Name = d.Name })
                .ToList();
        }

        public void Delete(Guid id)
        {
            DeleteMoviee(id);
        }
    }
}
