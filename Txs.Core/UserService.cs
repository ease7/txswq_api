using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txs.Core.Models;
using Txs.Core.RedisService;

namespace Txs.Core
{
    public class UserService
    {
        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo info)
        {
            try
            {
                info.CreateTime = DateTime.Now.ToMilliseconds();
                var service = RedisCache.GetCache();

                var key = $"USER_{info.ID}";

                var exist = GetUser(info.ID);

                if (exist != null)
                {
                    if(exist.Amount > 0 && !string.IsNullOrEmpty(exist.Province))
                    {
                        service.SortedSetIncrement("amount_total", "total", (0 - exist.Amount));
                        service.SortedSetIncrement("amount_total", exist.Province, (0 - exist.Amount));
                    }
                }

                // 更新值
                service.Insert<UserInfo>(key, info);
                // 排序
                service.SortedSetAdd("user_sort", info.CreateTime, info.ID);
                // 汇总
                service.SortedSetIncrement("amount_total", "total", info.Amount);
                service.SortedSetIncrement("amount_total", info.Province, info.Amount);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 用户身份证号码获取信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserInfo GetUser(string ID)
        {
            try
            {
                var service = RedisCache.GetCache();

                var key = $"USER_{ID}";
                return service.Get<UserInfo>(key);

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 取最新的10条数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<UserInfo> Top(int count)
        {
            try
            {
                var service = RedisCache.GetCache();

                List<string> list = service.SortedSetRevRange<string>("user_sort", 0, count);

                List<UserInfo> result = new List<UserInfo>();

                list.ForEach(item =>
                {

                    var exist = GetUser(item);

                    if (exist != null)
                    {
                        result.Add(exist);
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                return new List<UserInfo>();
            }

        }

        public double Total()
        {
            try
            {
                var service = RedisCache.GetCache();

                var count = service.SortedSetScore("amount_total", "total");

                return count ?? 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
