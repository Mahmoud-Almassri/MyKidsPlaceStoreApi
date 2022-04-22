using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using Repository.UnitOfWork;
using Service.Interfaces;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SaleService : ISaleService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public SaleService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<Sale> AddAsync(Sale entity)
        {
            entity.CreatedDate = DateTime.Now;

            await _repositoryUnitOfWork.Sale.Value.AddAsync(entity);
            await LinkSaleWithAllActiveProductAsync (entity);
            return entity;
        }
         public async Task<Sale> PostSignleSaleAsync(SignleSaleDTO entity)
        {
            entity.CreatedDate = DateTime.Now;
            Sale sale = new Sale
            {
                CreatedDate = entity.CreatedDate,
                EndDate = entity.EndDate,
                NewPrice = entity.NewPrice,
                OldPrice = entity.OldPrice,
                Status = entity.Status
            };

            sale = await _repositoryUnitOfWork.Sale.Value.AddAsync(sale);
            Item item = await _repositoryUnitOfWork.Item.Value.GetAsync(entity.ItemId);
            item.SaleId = sale.Id;
            await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(item);
            return sale;
        }

        private async Task LinkSaleWithAllActiveProductAsync(Sale sale)
        {
            List<Item> items = _repositoryUnitOfWork.Item.Value
                               .Find(x => x.Price == sale.OldPrice && x.Status == (int)GlobalStatusEnum.Active)
                               .ToList();

            foreach(var item in items)
            {
                item.SaleId = sale.Id;
            }
            await _repositoryUnitOfWork.Item.Value.UpdateRangeAsync(items);
        }

        public async Task<Sale> UpdateAsync(Sale entity)
        {
            
            return await _repositoryUnitOfWork.Sale.Value.UpdateAsync(entity);
        }

        public async Task<Sale> GetAsync(int Id)
        {
            Sale Sale = await _repositoryUnitOfWork.Sale.Value.GetAsync(Id);
            return Sale;
        }

        public async Task<Sale> ToggleStatusAsync(int Id)
        {
            Sale Sale = await _repositoryUnitOfWork.Sale.Value.GetAsync(Id);
            if(Sale.Status == 1)
            {
                Sale.Status = 2;
            }
            else
            {
                Sale.Status = 1;
            }
            await _repositoryUnitOfWork.Sale.Value.UpdateAsync(Sale);
            return Sale;
        }

        public async Task<BaseListResponse<Sale>> ListAsync(BaseSearch search)
        {
            BaseListResponse<Sale> Sales = await _repositoryUnitOfWork.Sale.Value.List(x=> true, search.PageSize, search.PageNumber);
            return Sales;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Sale.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<Sale> AddRange(IEnumerable<Sale> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> UpdateRange(IEnumerable<Sale> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> RemoveRange(IEnumerable<Sale> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> GetAll()
        {
            return _repositoryUnitOfWork.Sale.Value.GetAll();

        }
    }
}
