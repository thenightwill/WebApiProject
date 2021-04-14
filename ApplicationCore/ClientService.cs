using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Data;

namespace ApplicationCore
{
    public class ClientService
    {
        public Response<Client> CreateClient(Client client)
        {
            Response<Client> response = new Response<Client>();
            Guid guid = Guid.NewGuid();
            ClientData clientData = new ClientData();
            client.IdClient = guid.ToString();
            string returnstring = clientData.CreateClient(client:client);
            if (returnstring.Equals(client.IdClient))
            {
                response.RequestState = true;
                response.Register = client;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = returnstring;
            }

            return response;
        }

        public Response<Client> AddCreditsClient(Client client)
        {
            Response<Client> response = new Response<Client>();
            ClientData clientData = new ClientData();
            if (client.Credits == null  || client.IdClient == null)
            {
                response.RequestState = false;
                response.ErrorMessage = "Request Incorrecto, verifique e intente de nuevo";

                return response;
            }
            Client oldclient = clientData.GetClient(clientid: client.IdClient);
            oldclient.Credits = oldclient.Credits + client.Credits;
            bool state = clientData.UpdateClient(client: oldclient);
            if (state)
            {
                response.RequestState = true;
                response.Register = oldclient;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";
            }

            return response;
        }
        public Response<Client> ValidateCreditsClient(string clientid, float money)
        {
            ClientData clientData = new ClientData();
            Response<Client> response = new Response<Client>();
            Client oldclient = clientData.GetClient(clientid: clientid);
            if (oldclient==null)
            {
                response.RequestState = false;
                response.ErrorMessage = "Cliente no Existente";

                return response;
            }
            if (money>oldclient.Credits)
            {
                response.RequestState = false;
                response.ErrorMessage = "Dinero Insuficiente";

                return response;
            }
            if (money > 10000)
            {
                response.RequestState = false;
                response.ErrorMessage = "Tope Sobrepasado";

                return response;
            }

            return null;
        }

        public bool RemoveCreditsClient(string clientId, float money)
        {
            ClientData clientData = new ClientData();
            Client client = clientData.GetClient(clientid: clientId);
            client.Credits = client.Credits - money;
            bool clientstate = clientData.UpdateClient(client: client);
            if (clientstate)
            {
                return true;
            }
            return false;
            
        }

        public bool AddCreditsClient(string clientId, double money)
        {
            ClientData clientData = new ClientData();
            Client client = clientData.GetClient(clientid: clientId);
            double sum = client.Credits + money;
            client.Credits = float.Parse(sum.ToString());
            bool clientstate = clientData.UpdateClient(client: client);
            if (clientstate)
            {
                return true;
            }
            return false;

        }
    }
}
