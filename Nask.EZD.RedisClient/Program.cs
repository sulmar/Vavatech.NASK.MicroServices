using System;
using System.Threading;
using StackExchange.Redis;

namespace Nask.EZD.RedisClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            KeyTest();

            IncrTest();

            TtlTest();
            
        
        }

        private static void TtlTest()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379, server1:6379");

            string key = "token";

            // select 0
            IDatabase db = redis.GetDatabase();

            // set token boo ttl 120
            db.StringSet(key, "boo", TimeSpan.FromSeconds(120));

            Thread.Sleep(TimeSpan.FromSeconds(5));

             TimeSpan? ttl =  db.KeyTimeToLive(key);

             System.Console.WriteLine(ttl);



            // get foo
            string value = db.StringGet(key);

            System.Console.WriteLine(value);
        }

        private static void IncrTest()
        {
             ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379, server1:6379");

            string key = "points";

            // select 0
            IDatabase db = redis.GetDatabase();

            // incr points
            db.StringIncrement(key);

            // get foo
            string value = db.StringGet(key);

            System.Console.WriteLine(value);
        }

        // dotnet add package StackExchange.Redis
        private static void KeyTest()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379, server1:6379");

            string key = "foo";

            // select 0
            IDatabase db = redis.GetDatabase();

            // set foo boo
            db.StringSet(key, "boo");

            // get foo
            string value = db.StringGet(key);

            System.Console.WriteLine(value);



        }
    }
}
