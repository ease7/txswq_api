using System;
using System.Collections.Generic;
using System.Text;

namespace Txs.Core.RedisService
{
    public interface ICache
    {
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        int TimeOut { set; get; }
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        object Get(string key);
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        T Get<T>(string key);
        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);
        /// <summary>
        /// 清空所有缓存对象
        /// </summary>
        //void Clear();
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        void Insert(string key, object data);
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        void Insert<T>(string key, T data);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        void Insert(string key, object data, int cacheTime);

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        void Insert<T>(string key, T data, int cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        void Insert(string key, object data, DateTime cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        void Insert<T>(string key, T data, DateTime cacheTime);


        /// <summary>
        /// 判断key是否存在
        /// </summary>
        bool Exists(string key);

        /// <summary>
        /// 集合添加元素
        /// </summary>
        /// <returns></returns>
        bool SetAdd<T>(string key, T member);

        /// <summary>
        /// 有序集合添加元素
        /// </summary>
        /// <returns></returns>
        bool SortedSetAdd<T>(string key, double score, T member);

        /// <summary>
        /// 有序集合增长
        /// </summary>
        /// <returns></returns>
        bool SortedSetIncrement(string key, string member, double score);

        /// <summary>
        /// 获取指定排名的数据，大到小排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        List<T> SortedSetRevRange<T>(string key, long min, long max);

        /// <summary>
        /// 获取指定范围的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        List<T> SortedSetRangeByScore<T>(string key, double min, double max);

        /// <summary>
        /// 删除指定范围的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        long SortedSetRemoveRangeByScore(string key, double min, double max);

        /// <summary>
        /// 返回score
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        double? SortedSetScore(string key, string member);
    }
}
