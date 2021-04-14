using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using System.Linq;
using NReJSON;
using Newtonsoft.Json;

namespace Data
{
    public class RouletteData
    {
        public string CreateRoulette(InsertObject<Roulette> io)
        {
            try
            {
                //Environment.CurrentDirectory = Environment.GetEnvironmentVariable("redishost");
                string host = "localhost";
                string port = "5008";
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
                IDatabase db = redis.GetDatabase();
                string jsonZipPlusFour = JsonConvert.SerializeObject(io.Objects);
                db.ListRightPush("listroulette", new RedisValue(io.objiD));
                db.JsonSet("roulette:"+io.objiD,jsonZipPlusFour);
                return io.objiD;
            }
            catch (Exception e)
            {
                return e.Message;
            }
           
        }
       
        public List<Roulette> GetRoulettes()
        {
            string host = "localhost";
            string port = "5008";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
            IDatabase db = redis.GetDatabase();
            List<Roulette> listroulettes = new List<Roulette>();
            string[] returnkeys = new string[] { "IdRoulette", "RouletteOpenState", "RouletteEndGameState" };
            try
            {
                var jsonlist = db.ListRange("listroulette", 0, -1);
                foreach (var rouletteid in jsonlist)
                {
                    var JSONroulette = db.JsonGet("roulette:" + rouletteid.ToString(), returnkeys);
                    Roulette roulette = JsonConvert.DeserializeObject<Roulette>(JSONroulette.ToString());
                    listroulettes.Add(roulette);
                }

                return listroulettes;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public Roulette GetRoulette(Roulette oldroulette)
        {
            string host = "localhost";
            string port = "5008";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
            IDatabase db = redis.GetDatabase();
            string[] returnkeys = new string[] { "IdRoulette", "RouletteOpenState", "RouletteEndGameState", "BetsList", "WinnerColor", "WinnerNumber", "TotalMoney", "DateOpenState", "DateEndGame" };
            try
            {
                    var JSONroulette = db.JsonGet("roulette:" + oldroulette.IdRoulette, returnkeys);
                    Roulette roulette = JsonConvert.DeserializeObject<Roulette>(JSONroulette.ToString());

                return roulette;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public bool UpdateRoulette(Roulette Roulette)
        {
            string host = "localhost";
            string port = "5008";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
            IDatabase db = redis.GetDatabase();
            try
            {
                string jsonZipPlusFour = JsonConvert.SerializeObject(Roulette);
                db.JsonSetAsync("roulette:" + Roulette.IdRoulette, jsonZipPlusFour);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
