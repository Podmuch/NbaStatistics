using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaStatistics
{
    public class SingleRecord
    {
        private const char PARAMETERS_SEPARATOR = ',';
        private const char STATISTICS_SEPARATOR = '*';
        public string TeamName { get; set; }
        public int WonGames { get; set; }
        public int LostGames { get; set; }
        public int WonGamesAsBidded { get; set; }
        public int LostGamesAsBidded { get; set; }
        public int WonGamesAsFavourite { get; set; }
        public int LostGamesAsFavourite { get; set; }
        public float PercentOfCorrectFavourities { get { return ((float)CorrectFavourities)/((float)AllFavourities); } }
        public float PercentOfCorrectBidded { get { return ((float)CorrectBidded) / ((float)AllBidded); } }
        public int CorrectFavourities { get; set; }
        public int AllFavourities { get; set; }
        public int CorrectBidded { get; set; }
        public int AllBidded { get; set; }

        public SingleRecord(string recordLine)
        {
            string[] parameters = recordLine.Split(PARAMETERS_SEPARATOR);
            TeamName = parameters[0];
            WonGames = Convert.ToInt32(parameters[1]);
            LostGames = Convert.ToInt32(parameters[2]);
            WonGamesAsBidded = Convert.ToInt32(parameters[3]);
            LostGamesAsBidded = Convert.ToInt32(parameters[4]);
            WonGamesAsFavourite = Convert.ToInt32(parameters[5]);
            LostGamesAsFavourite = Convert.ToInt32(parameters[6]);
            string[] favouritiesStatistics = parameters[7].Split(STATISTICS_SEPARATOR);
            CorrectFavourities = Convert.ToInt32(favouritiesStatistics[0]);
            AllFavourities = Convert.ToInt32(favouritiesStatistics[1]);
            string[] biddedStatistics = parameters[8].Split(STATISTICS_SEPARATOR);
            CorrectBidded = Convert.ToInt32(biddedStatistics[0]);
            AllBidded = Convert.ToInt32(biddedStatistics[1]);
        }

        public void AddGame(bool isWon, bool isBidAsWinner, bool isGameHadFavourite)
        {
            if(isWon)
            {
                WonGames++;
                if(isBidAsWinner)
                {
                    CorrectBidded++;
                    if(isGameHadFavourite)
                    {
                        CorrectFavourities++;
                        AllFavourities++;
                        WonGamesAsFavourite++;
                    }
                }
                else
                {
                    if (isGameHadFavourite)
                    {
                        AllFavourities++;
                    }
                }
            }
            else
            {
                LostGames++;
                if (!isBidAsWinner)
                {
                    CorrectBidded++;
                    if (isGameHadFavourite)
                    {
                        CorrectFavourities++;
                        AllFavourities++;
                        LostGamesAsFavourite++;
                    }
                }
                else
                {
                    if (isGameHadFavourite)
                    {
                        AllFavourities++;
                    }
                }
            }
            if (isBidAsWinner)
            {
                WonGamesAsBidded++;
            }
            else
            {
                LostGamesAsBidded++;
            }
            AllBidded++;
        }

        public string Serialize()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(TeamName.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(WonGames.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(LostGames.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(WonGamesAsBidded.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(LostGamesAsBidded.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(WonGamesAsFavourite.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(LostGamesAsFavourite.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(CorrectFavourities.ToString() + STATISTICS_SEPARATOR + AllFavourities.ToString() + PARAMETERS_SEPARATOR);
            builder.Append(CorrectBidded.ToString() + STATISTICS_SEPARATOR + AllBidded.ToString() + PARAMETERS_SEPARATOR);
            return builder.ToString();
        }
    }
}
