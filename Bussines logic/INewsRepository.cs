using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_logic
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsDto>> GetAllAsync();
        Task<NewsDto> GetByIdAsync(int id);
        Task<IEnumerable<NewsDto>> GetLatestAsync(int count);
        void Add(NewsDto news);
        void Remove(int newsId);
        void Update(NewsDto news);
        IEnumerable<NewsDto> GetAll();
        NewsDto GetById(int id);
    }

}
