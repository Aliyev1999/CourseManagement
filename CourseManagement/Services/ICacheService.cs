using CourseManagementEFCore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public interface ICacheService
    {
        object Get(string key);
        void Set(CacheRequest data);
        Task ClearCache(int id);

    }
}
