using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RiotAPITest.Model;

namespace RiotAPITest.Data
{
    public class ApiFetcher : IApiFetcher
    {
        private readonly string apiKey = API.apiKey;


        public UserModel GetLeagueUser(string username)
        {
            return Fetcher.GetUserByName(username, apiKey);
        }

        public MatchCollection GetGamesForUser(string username)
        {
            UserModel um = Fetcher.GetUserByName(username, apiKey);
            MatchCollection mc = Fetcher.GetMatches(um.accountId, apiKey);
            return mc;
        }

        public bool UserWonLastGame(string username)
        {
            UserModel um = Fetcher.GetUserByName(username, apiKey);
            MatchCollection mc = Fetcher.GetMatches(um.accountId, apiKey);
            Match m = mc.matches[0];
            SingleMatch match = Fetcher.GetMatch(m.gameId.ToString(), apiKey);

            List<ParticipantIdentity> p = match.participantIdentities;
            
            return true; //TODO
        }
    }

    public static class Fetcher
    {
        public static UserModel GetUserByName(string name, string apiKey)
        {
            string url = string.Format("https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/"+name);
            WebRequest reqObj = WebRequest.Create(url);
            reqObj.Method = "GET";
            reqObj.Headers.Add("X-Riot-Token:" + apiKey);
            try
            {
                HttpWebResponse resObj = (HttpWebResponse)reqObj.GetResponse();

                string result = "";
                using (Stream stream = resObj.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                UserModel um = JsonConvert.DeserializeObject<UserModel>(result);
                return um;
            }
            catch (WebException e)
            {
                System.Console.WriteLine(e.Message);
                return new UserModel { };
            }
        }

        public static MatchCollection GetMatches(string accountId, string apiKey)
        {
            string url = string.Format("https://euw1.api.riotgames.com/lol/match/v4/matchlists/by-account/" + accountId);
            WebRequest reqObj = WebRequest.Create(url);
            reqObj.Method = "GET";
            reqObj.Headers.Add("X-Riot-Token:" + apiKey);
            try
            {
                HttpWebResponse resObj = (HttpWebResponse)reqObj.GetResponse();

                string result = "";
                using (Stream stream = resObj.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                MatchCollection um = JsonConvert.DeserializeObject<MatchCollection>(result);
                return um;
            }
            catch (WebException e)
            {
                System.Console.WriteLine(e.Message);
                return new MatchCollection { };
            }
        }

        public static SingleMatch GetMatch(string matchId, string apiKey)
        {
            string url = string.Format("https://euw1.api.riotgames.com/lol/match/v4/matches/" + matchId);
            WebRequest reqObj = WebRequest.Create(url);
            reqObj.Method = "GET";
            reqObj.Headers.Add("X-Riot-Token:" + apiKey);
            try
            {
                HttpWebResponse resObj = (HttpWebResponse)reqObj.GetResponse();

                string result = "";
                using (Stream stream = resObj.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                SingleMatch match = JsonConvert.DeserializeObject<SingleMatch>(result);
                return match;
            }
            catch (WebException e)
            {
                System.Console.WriteLine(e.Message);
                return new SingleMatch { };
            }
        }
    }
}
