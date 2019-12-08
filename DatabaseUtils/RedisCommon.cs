using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;

namespace DatabaseUtils
{
    public class RedisCommon
    {
        private string _connectionString; //连接字符串
        private string _instanceName; //实例名称
        private int _defaultDB; //默认数据库
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public RedisCommon(string connectionString, string instanceName, int defaultDB = 0)
        {
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDB = defaultDB;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }


        //数据库的默认连接地址
        private static readonly string ConnectionString = "";

        //连接多工器
        private static ConnectionMultiplexer redis;

        //数据库对象
        private static IDatabase database;


        //初始化
        private ConnectionMultiplexer GetConnection()
        {

            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
            if (redis == null)
            {
                var option = ConfigurationOptions.Parse(string.IsNullOrEmpty(ConnectionString) == true ? "192.168.1.10:6379" : ConnectionString);
                option.AllowAdmin = true;
                redis = ConnectionMultiplexer.Connect(option);
            }
            return redis;
        }

        //初始化数据库对象
        public IDatabase GetData()
        {
            if (database == null)
            {
                database = GetConnection().GetDatabase(_defaultDB);
            }
            return database;
        }
        //清空
        public bool FlushAll()
        {
            try
            {

                GetConnection().GetServer(_connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString)).GetEndPoints()[0]).FlushAllDatabases();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }


    public static class Extenions
    {

        #region 扩展
        //HashEntry转化为Dcitionary
        public static Dictionary<string, string> ToDic(this HashEntry[] hash)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (hash.Length == 0)
                return dic;
            foreach (var item in hash)
            {
                dic.Add(item.Name, item.Value);
            }
            return dic;
        }

        //Dcitionary转化为HashEntry
        public static HashEntry[] ToHashEntry(this Dictionary<string, string> dic)
        {
            List<HashEntry> list = new List<HashEntry>();
            foreach (var item in dic.Keys)
            {
                list.Add(new HashEntry(item, dic[item]));
            }
            return list.ToArray();
        }

        //实体类转化成HashEntry[]
        public static HashEntry[] ModelToHashEntry(object model)
        {
            try
            {
                PropertyInfo[] proArray = model.GetType().GetProperties();
                HashEntry[] hashArray = new HashEntry[proArray.Length];
                Type type = proArray.GetType();

                for (int i = 0; i < proArray.Length; i++)
                {
                    hashArray[i] = new HashEntry(proArray[i].Name, proArray[i].GetValue(model).ToString());
                }
                return hashArray;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //HashEntry[]转化成实体类
        public static T HashEntryToModel<T>(HashEntry[] hashEntryArray) where T : new()
        {
            try
            {

                var t = new T();
                foreach (var item in hashEntryArray)
                {

                    var value = item.Value.ToString();
                    t.GetType().GetProperty(item.Name.ToString()).SetValue(t, value);

                }
                return t;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion

    }

}