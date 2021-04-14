using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public class RouletteService
    {
        public Response<Roulette> CreateRoulette()
        {
            Response<Roulette> response = new Response<Roulette>();
            Guid guid = Guid.NewGuid();
            Roulette newroulette = new Roulette();
            RouletteData rouletteData = new RouletteData();
            InsertObject<Roulette> isnew = new InsertObject<Roulette>();
            newroulette.IdRoulette = guid.ToString();
            isnew.objiD = guid.ToString();
            isnew.Objects = newroulette;
            string returnstring = rouletteData.CreateRoulette(isnew);
            if (returnstring.Equals(isnew.objiD))
            {
                response.RequestState = true;
                response.Register = newroulette;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = returnstring;
            }

            return response;
        }

        public Response<Roulette> GetRoulettes()
        {
            RouletteData rouletteData = new RouletteData();
            List<Roulette> listroulettes= rouletteData.GetRoulettes();
            Response<Roulette> response = new Response<Roulette>();
            if (listroulettes!=null)
            {
                response.RequestState = true;
                response.Registers = listroulettes;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";
            }

            return response;
        }

        public Response<Roulette> UpdateGameStateRoulette(RouletteRequest request)
        {
            Response<Roulette> response = new Response<Roulette>();
            Roulette oldroulette = new Roulette();
            RouletteData rouletteData = new RouletteData();
            oldroulette.IdRoulette = request.RouletteID;
            Roulette actualroulette = rouletteData.GetRoulette(oldroulette:oldroulette);
            actualroulette.RouletteOpenState = true;
            actualroulette.DateOpenState = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            bool state = rouletteData.UpdateRoulette(actualroulette);
            if (state)
            {
                response.RequestState = true;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";
            }

            return response;
        }

        public Response<Roulette> UpdateBetRoulette(RouletteRequest request, string clientId)
        {
            Response<Roulette> response = new Response<Roulette>();
            Roulette oldroulette = new Roulette();
            RouletteData rouletteData = new RouletteData();
            ClientService clientService = new ClientService();
            bool clientupdatestate = clientService.RemoveCreditsClient(clientId: clientId, money: request.Betmoney);
            if (!clientupdatestate)
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";

                return response;
            }
            oldroulette.IdRoulette = request.RouletteID;
            Roulette actualroulette = rouletteData.GetRoulette(oldroulette: oldroulette);
            Bet newbet = new Bet();
            newbet.IdClient = clientId;
            newbet.Number = request.BetNumber;
            newbet.Color = request.BetColor;
            newbet.BetMoney = request.Betmoney;
            if (actualroulette.BetsList == null)
                actualroulette.BetsList = new List<Bet>();
            actualroulette.BetsList.Add(newbet);
            actualroulette.TotalMoney = actualroulette.TotalMoney + newbet.BetMoney;
            bool state = rouletteData.UpdateRoulette(actualroulette);
            if (state)
            {
                response.RequestState = true;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";
            }

            return response;
        }

        public Response<Roulette> CloseRoulette(string RouletteId)
        {
            RouletteData rouletteData = new RouletteData();
            Roulette oldroulette = new Roulette();
            Response<Roulette> response = new Response<Roulette>();
            oldroulette.IdRoulette = RouletteId;
            Roulette actualroulette = rouletteData.GetRoulette(oldroulette: oldroulette);
            actualroulette.DateEndGame= DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            actualroulette.RouletteEndGameState = true;
            actualroulette.RouletteOpenState = false;
            AsignPrize(actualroulette.BetsList, actualroulette.TotalMoney);
            actualroulette.WinnerNumber= AsignPrize(actualroulette.BetsList, actualroulette.TotalMoney);
            actualroulette.WinnerColor = "Negro";
            if(actualroulette.WinnerNumber%2==0)
                actualroulette.WinnerColor = "Rojo";
            bool state = rouletteData.UpdateRoulette(actualroulette);
            if (state)
            {
                response.RequestState = true;
                response.Register = actualroulette;
            }
            else
            {
                response.RequestState = false;
                response.ErrorMessage = "Error de Base de datos";
            }

            return response;
        }

        public int AsignPrize(List<Bet> bets, float totalbets)
        {
            Random rnd = new Random();
            int winner = rnd.Next(0, 36);
            ClientService clientService = new ClientService();
            foreach (var bet in bets)
            {
                if (bet.Number==winner)
                {
                    clientService.AddCreditsClient(bet.IdClient, totalbets * 5);
                }
                if ((winner % 2)==0 && bet.Color!=null && bet.Color.ToLower().Equals("rojo"))
                {
                    clientService.AddCreditsClient(bet.IdClient, totalbets * 1.8);
                }
                if ((winner % 2) != 0 && bet.Color != null && bet.Color.ToLower().Equals("negro"))
                {
                    clientService.AddCreditsClient(bet.IdClient, totalbets * 1.8);
                }
            }

            return winner;
        }


    }
}
