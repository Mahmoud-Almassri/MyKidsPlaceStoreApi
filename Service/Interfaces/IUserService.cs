using Domains.DTO;
using Domains.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService //: IService<UserDTO, UserSearch>
    {
        Task<BaseListResponse<UserDTO>> List(UserSearch search);
        Task<List<UserDTO>> GetAll();
        Task<bool> UpdateStatus(long userId);
    }
}


