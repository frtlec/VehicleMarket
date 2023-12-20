using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMarket.Services.Advert.Domain.Core
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> Get(int key);
        Task<List<T>> GetAll();
        Task Add(T item);
        Task AddRange(List<T> items);
        Task Update(T item);
        Task Delete(int key);
    }
}
