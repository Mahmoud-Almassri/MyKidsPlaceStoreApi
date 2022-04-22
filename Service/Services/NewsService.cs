using Domains.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsService : INewsService
    {
        private MyKidsStoreDbContext _context;

        public NewsService(MyKidsStoreDbContext context)
        {
            _context = context;
        }

        public News Update(News entity)
        {
            News news = _context.News.FirstOrDefault(x => x.Id == entity.Id);
            news.EndDate = entity.EndDate;
            news.NewsDescription = entity.NewsDescription;
            _context.SaveChanges();
            return entity;
        }

        public async Task<News> GetAsync()
        {
            News News = await _context.News.FirstOrDefaultAsync();
            return News;
        }

        public async Task<News> GetNews()
        {
            News News = await _context.News.FirstOrDefaultAsync(x=>x.EndDate >= DateTime.Now);
            if(News == null)
            {
                throw new ValidationException("No new news");
            }
            return News;
        }
    }
}
