using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_logic
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllAsync();
        }

        public async Task<NewsDto> GetNewsByIdAsync(int id)
        {
            return await _newsRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<NewsDto>> GetLatestNewsAsync(int count)
        {
            return await _newsRepository.GetLatestAsync(count);
        }
    }
}
