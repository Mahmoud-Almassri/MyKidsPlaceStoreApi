using Domains.Models;
using Domains.SearchModels;
using Service.Interfaces.Common;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISaleService : IService<Sale, BaseSearch>
    {
        Task<Sale> ToggleStatusAsync(int Id);
        Task<Sale> PostSignleSaleAsync(SignleSaleDTO sale);
    }
}
