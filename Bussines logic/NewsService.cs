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
            _newsRepository = newsRepository ?? throw new ArgumentNullException(nameof(newsRepository));
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllAsync();
        }

        public async Task<NewsDto> GetNewsByIdAsync(int newsId)
        {
            return await _newsRepository.GetByIdAsync(newsId);
        }

        public async Task<IEnumerable<NewsDto>> GetLatestNewsAsync(int count)
        {
            return await _newsRepository.GetLatestAsync(count);
        }

        // Реализация отсутствующих методов
        public void AddNews(string title, string content)
        {
            var news = new NewsDto
            {
                Title = title,
                Content = content,
                PublishedAt = DateTime.UtcNow
            };
            _newsRepository.Add(news);
        }

        public void RemoveNews(int newsId)
        {
            _newsRepository.Remove(newsId);
        }

        public void ModifyNews(int newsId, string title, string content)
        {
            var news = _newsRepository.GetById(newsId);
            if (news != null)
            {
                news.Title = title;
                news.Content = content;
                _newsRepository.Update(news);
            }
        }

        public IEnumerable<NewsDto> GetAllNews()
        {
            return _newsRepository.GetAll();
        }

        public NewsDto GetNewsById(int newsId)
        {
            return _newsRepository.GetById(newsId);
        }
    }
}
