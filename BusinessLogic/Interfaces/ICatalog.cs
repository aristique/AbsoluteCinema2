using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface ICatalog
    {
        List<Movie> GetAll(string genre = null);
        Movie GetById(Guid id);
        List<Genre> GetGenres();
        void RecordDetailsView(Guid movieId);
        
        List<Movie> GetPopular(int top = 4);
        void IncrementDetailsViewCount(Guid movieId);
    }
}