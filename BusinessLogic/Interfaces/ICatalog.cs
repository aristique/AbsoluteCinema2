using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface ICatalog
    {
        List<MovieModel> GetAll();
        MovieDetailsModel GetById(Guid id);
        List<MovieModel> GetPopular(int count = 4);
        List<GenreModel> GetGenres();
        void IncrementDetailsViewCount(Guid id);
    }
}
