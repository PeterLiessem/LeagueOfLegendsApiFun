using System;
using RiotAPITest.Model;

namespace RiotAPITest.Data
{
    public interface IApiFetcher
    {
        UserModel GetLeagueUser(string username);
        bool UserWonLastGame(string username);
        MatchCollection GetGamesForUser(string accountId);
    }
}
