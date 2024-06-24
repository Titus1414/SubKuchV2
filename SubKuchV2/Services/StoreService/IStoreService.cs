using SubKuchV2.DTO_s;
using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Services.StoreService
{
   public interface IStoreService
    {
        Task<string> AddStore(AddStoreDto dto);
        Task<string> UpdateStore(UpdatestoreDto dto);
        Task<string> RemoveStore(int Id);
        Task<string> StatusChange(int Id);
        Task<string> AddRestaurant(AddStoreDto dto);
        Task<object> GetBussinesCat();

        Task<Object> GetStores();
        Task<Object> Restaurants();
        Task<object> catwiseItems(int catid);
        Task<object> GetStoreProduct(int id);
        Task<object> SearchProduct(string productname);
        Task<List<PaymentDto>> Payment();
        Task<List<PaymentDto>> Payment(DateTime strt, DateTime end);
       
    }
}
