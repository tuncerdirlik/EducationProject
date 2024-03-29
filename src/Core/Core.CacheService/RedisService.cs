﻿using StackExchange.Redis;

namespace Core.CacheService
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;

        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        }

        public IDatabase GetDb(int db = 0)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }

        public IServer GetServer()
        {
            return GetDb().Multiplexer.GetServer(_host, _port);
        }
    }
}
