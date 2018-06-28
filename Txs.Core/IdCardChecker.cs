using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Txs.Core.RedisService;

namespace Txs.Core
{
    /// <summary>
    /// 身份证号码验证
    /// </summary>
    public class IdCardChecker
    {
        /// <summary>
        /// 检测身份证号码是否有效
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>验证结果</returns>
        public static bool Check(string id)
        {
            var cache = RedisCache.GetCache();
            var key = GetKey(id);
            // 缓存中是否有值
            var isExist = cache.Exists(key);
            
            if (isExist)
            {
                // 缓存的判断结果直接返回
                var result = cache.Get<int>(key);

                return result == 1;
            }
            else
            {
                // 远程服务器接口判断
                var result = HttpCheck(id);
                // 存储判断结果
                SaveCheckResult(id, result);

                return result;
            }
        }

    
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>缓存key</returns>
        public static string GetKey(string id)
        {
            return $"Check:Id:${id}";
        }

        /// <summary>
        /// 存储检验结果
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <param name="result">验证结果</param>
        public static void SaveCheckResult(string id, bool result)
        {
            try
            {
                string key = GetKey(id);

                var cache = RedisCache.GetCache();

                cache.Insert<int>(key, result ? 1 : 0);
            }
            catch(Exception ex)
            {

            }

        }

        /// <summary>
        /// ip138网站检测身份证号码
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>验证结果</returns>
        private static bool HttpCheck(string id)
        {
            string url = $"http://qq.ip138.com/idsearch/index.asp?action=idcard&userid={id}";
            var result = WebTools.GetRequestData(url, "get", "", Encoding.Default);

            if (!string.IsNullOrEmpty(id) && id.Length == 18)
            {
                if (result.Contains("身份证号校验位不正确"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
