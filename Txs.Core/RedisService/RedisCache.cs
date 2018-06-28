using System;
using System.Collections.Generic;
using System.Text;

namespace Txs.Core.RedisService
{
    public class RedisCache
    {
     

        public static ICache GetCache()
        {
           

            return new RedisService();
        }
    }
}
