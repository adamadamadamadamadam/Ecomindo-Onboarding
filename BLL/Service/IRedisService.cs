using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, object value);
        Task<bool> DeleteAsync(string key);
    }
}
