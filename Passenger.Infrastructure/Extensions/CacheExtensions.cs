using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenId, JwtDto jwtToken)
           => cache.Set(GetJwtKey(tokenId), jwtToken, TimeSpan.FromSeconds(5));

        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDto>(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId)
            => $"jwt-{tokenId}";
    }
}
