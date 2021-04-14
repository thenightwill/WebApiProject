using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Newtonsoft.Json;
using NReJSON;
using StackExchange.Redis;

namespace Data
{
    public class ClientData
    {
        public Client GetClient(string clientid)
        {
            string host = "localhost";
            string port = "5008";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
            IDatabase db = redis.GetDatabase();
            string[] returnkeys = new string[] { "IdClient", "Credits", "Identification", "Names", "LastNames" };
            try
            {
                var s = "client:" + clientid;
                var JSONclient = db.JsonGet("client:" + clientid, returnkeys);
                Client client1 = JsonConvert.DeserializeObject<Client>(JSONclient.ToString());

                return client1;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public bool UpdateClient(Client client)
        {
            string host = "localhost";
            string port = "5008";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
            IDatabase db = redis.GetDatabase();
            try
            {
                string jsonZipPlusFour = JsonConvert.SerializeObject(client);
                db.JsonSet("client:" + client.IdClient, jsonZipPlusFour);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public string CreateClient(Client client)
        {
            try
            {
                string host = "localhost";
                string port = "5008";
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ":" + port);
                IDatabase db = redis.GetDatabase();
                string jsonZipPlusFour = JsonConvert.SerializeObject(client);
                db.JsonSet("client:" + client.IdClient, jsonZipPlusFour);
                return client.IdClient;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }


    }
}
