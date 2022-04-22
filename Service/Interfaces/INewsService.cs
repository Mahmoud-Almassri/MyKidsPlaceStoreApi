using Domains.Models;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface INewsService
    {
        News Update(News entity);
        Task<News> GetAsync();
        Task<News> GetNews();
    }
}
