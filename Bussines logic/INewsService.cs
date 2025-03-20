using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_logic
{
    public interface INewsService
    {
        void AddNews(string title, string content);
        void RemoveNews(int newsId);
        void ModifyNews(int newsId, string title, string content);
        IEnumerable<NewsDto> GetAllNews();
        NewsDto GetNewsById(int newsId);
    }
}