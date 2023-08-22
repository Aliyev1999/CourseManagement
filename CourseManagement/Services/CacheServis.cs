using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public class CacheServis : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;
        public CacheServis(IMemoryCache memoryCache, IUnitOfWork unitOfWork)
        {
            this._memoryCache = memoryCache;
            this._unitOfWork = unitOfWork;
        }

        public async Task ClearCache(int id)
        {
            _memoryCache.Remove(id);
        }

        [HttpGet]
        public object Get(string key)
        {
            object value;
            bool isExist = _memoryCache.TryGetValue(key, out value);
            return value;
        }

        [HttpGet]
        public void Set(CacheRequest data)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
            _memoryCache.Set(data.key, data.value, cacheExpiryOptions);
        }
    }
}
