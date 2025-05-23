﻿using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface ICatalog
    {
        List<Movie> GetAll(string genre = null);
        Movie GetById(Guid id);
        List<Genre> GetGenres();
    }
}