using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Txs.Core.RedisService
{
    public class RedisService : ICache
    {
        int Default_Timeout = 600;//默认超时时间（单位秒）
        string address;
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };

        IDatabase database;

        public RedisService()
        {
            database = MRedisConnection.Instance.GetDatabase();
        }

        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return Default_Timeout;
            }
            set
            {
                Default_Timeout = value;
            }
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {

            DateTime begin = DateTime.Now;
            var cacheValue = database.StringGet(key);
            DateTime endCache = DateTime.Now;
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                value = JsonConvert.DeserializeObject<T>(cacheValue, jsonConfig);
            }
            DateTime endJson = DateTime.Now;
            return value;

        }

        public void Insert(string key, object data)
        {
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData);
        }

        public void Insert(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert(string key, object data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert<T>(string key, T data)
        {
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData);
        }

        public void Insert<T>(string key, T data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert<T>(string key, T data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now.ToUniversalTime();
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        string GetJsonData(object data)
        {
            return JsonConvert.SerializeObject(data, jsonConfig);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }

        /// <summary>
        /// 集合添加元素
        /// </summary>
        /// <returns></returns>
        public bool SetAdd<T>(string key, T member)
        {
            var jsonData = GetJsonData(member);

            return database.SetAdd(key, jsonData);
        }

        /// <summary>
        /// 有序集合添加元素
        /// </summary>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, double score, T member)
        {
            var jsonData = GetJsonData(member);

            return database.SortedSetAdd(key, jsonData, score);
        }

        /// <summary>
        /// 有序集合增长
        /// </summary>
        /// <returns></returns>
        public bool SortedSetIncrement(string key, string member, double score )
        {


            database.SortedSetIncrement(key, member, score);

            return true;
        }

        /// <summary>
        /// 获取指定排名的数据，大到小排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<T> SortedSetRevRange<T>(string key, long min, long max)
        {
            var array = database.SortedSetRangeByRank(key, min, max, Order.Descending);
            List<T> result = new List<T>();

            foreach (var item in array)
            {
                var value = JsonConvert.DeserializeObject<T>(item, jsonConfig);

                result.Add(value);
            }

            return result;
        }

        /// <summary>
        /// 获取指定范围的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByScore<T>(string key, double min, double max)
        {
            var array = database.SortedSetRangeByScore(key, min, max);
            List<T> result = new List<T>();

            foreach (var item in array)
            {
                var value = JsonConvert.DeserializeObject<T>(item, jsonConfig);

                result.Add(value);
            }

            return result;
        }

        /// <summary>
        /// 获取指定范围的数据,
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByScore<T>(string key, double min, double max, long offset, long count)
        {
            var array = database.SortedSetRangeByScore(key, min, max, Exclude.None, Order.Descending, offset, count);
            List<T> result = new List<T>();

            foreach (var item in array)
            {
                var value = JsonConvert.DeserializeObject<T>(item, jsonConfig);

                result.Add(value);
            }

            return result;
        }

        public double? SortedSetScore(string key, string member)
        {
            var result = database.SortedSetScore(key, member);
            

            return result;
        }

        /// <summary>
        /// 删除指定范围的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByScore(string key, double min, double max)
        {
            return database.SortedSetRemoveRangeByScore(key, min, max);
        }

        
    }
}
