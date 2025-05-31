using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IMovie
    {
    
        List<MovieModel> GetAll();

   
        MovieModel Get(Guid id);

        MovieModel Create(CreateMovieModel model);

        void Update(Guid id, CreateMovieModel model); 

        void Delete(Guid id);

 
        List<GenreModel> GetAvailableGenres();
        List<ActorModel> GetAvailableActors();
        List<DirectorModel> GetAvailableDirectors();
    }
}
