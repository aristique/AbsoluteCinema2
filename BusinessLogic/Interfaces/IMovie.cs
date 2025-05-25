using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IMovie
    {
        List<Movie> GetAll();
        Movie Create(Movie movie, List<Guid> genres, List<Guid> actors, List<Guid> directors, string youtubeId);
        Movie Get(Guid id);
        void Update(Movie movie, List<Guid> genres, List<Guid> actors, List<Guid> directors);
        void Delete(Guid id);
        List<Genre> GetAvailableGenres();
        List<Actor> GetAvailableActors();
        List<Director> GetAvailableDirectors();
    }
}
