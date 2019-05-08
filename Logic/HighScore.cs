using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Logic
{
    [Serializable]
    class HighScore
    {
        private UserResult[] beginners;
        private UserResult[] mediums;
        private UserResult[] experts;

        private int beginnersCount;
        private int mediumsCount;
        private int expertsCount;

        private readonly int usersCount = 10;

        public HighScore()
        {
            beginners = new UserResult[usersCount];
            mediums = new UserResult[usersCount];
            experts = new UserResult[usersCount];
        }

        private GameParameters.GameTypeLevel GetGameType(int rowCount, int columnCount, int minesCount)
        {
            if (rowCount == GameLogicConstants.LowLevelRowCount && columnCount == GameLogicConstants.LowLevelColumnCount && minesCount == GameLogicConstants.LowLevelMinesCount)
            {
                return GameParameters.GameTypeLevel.Beginner;
            }
            else if (rowCount == GameLogicConstants.MediumLevelRowCount && columnCount == GameLogicConstants.MediumLevelColumnCount && minesCount == GameLogicConstants.LowLevelMinesCount)
            {
                return GameParameters.GameTypeLevel.Medium;
            }
            else if (rowCount == GameLogicConstants.HighLevelRowCount && columnCount == GameLogicConstants.HighLevelColumnCount && minesCount == GameLogicConstants.HighLevelMinesCount)
            {
                return GameParameters.GameTypeLevel.Expert;
            }
            else
            {
                return GameParameters.GameTypeLevel.Custom;
            }
        }

        private static bool IsTimeInFirstTenResults(int time, UserResult[] users, int usersCount, int maxUsersCount)
        {
            if (usersCount < maxUsersCount)
            {
                return true;
            }
            else if (time < users[maxUsersCount - 1].Time)
            {
                return true;
            }

            return false;
        }

        private static void InsertUserResultInUsers(string userName, int time, UserResult[] users, int usersCount)
        {
            int i = 0;
            while ((usersCount - 1 - i > 0) && (time < users[usersCount - 1 - i].Time))
            {
                users[usersCount - i] = users[usersCount - i - 1];
                i++;
            }

            users[usersCount - i] = new UserResult(userName, time);
        }

        public bool IsHighScoreGameResult(int time, int rowCount, int columnCount, int minesCount)
        {
            GameParameters.GameTypeLevel gameType = GetGameType(rowCount, columnCount, minesCount);

            switch (gameType)
            {
                case GameParameters.GameTypeLevel.Beginner:
                    return IsTimeInFirstTenResults(time, beginners, beginnersCount, usersCount);

                case GameParameters.GameTypeLevel.Medium:
                    return IsTimeInFirstTenResults(time, mediums, mediumsCount, usersCount);

                case GameParameters.GameTypeLevel.Expert:
                    return IsTimeInFirstTenResults(time, experts, expertsCount, usersCount);

                default:
                    return false;
            }
        }

        public void AddHighScore(string userName, int time, int rowCount, int columnCount, int minesCount)
        {
            GameParameters.GameTypeLevel gameType = GetGameType(rowCount, columnCount, minesCount);

            switch (gameType)
            {
                case GameParameters.GameTypeLevel.Beginner:
                    InsertUserResultInFirstTenResults(userName, time, beginners, ref beginnersCount, usersCount);
                    break;

                case GameParameters.GameTypeLevel.Medium:
                    InsertUserResultInFirstTenResults(userName, time, mediums, ref mediumsCount, usersCount);
                    break;

                case GameParameters.GameTypeLevel.Expert:
                    InsertUserResultInFirstTenResults(userName, time, experts, ref expertsCount, usersCount);
                    break;

                default:
                    break;
            }
        }

        private static void InsertUserResultInFirstTenResults(string userName, int time, UserResult[] users, ref int usersCount, int maxUserCount)
        {
            if (usersCount < maxUserCount)
            {
                if (usersCount != 0 && (time < users[usersCount - 1].Time))
                {
                    InsertUserResultInUsers(userName, time, users, usersCount);
                }
                else
                {
                    users[usersCount] = new UserResult(userName, time);
                }

                usersCount++;
            }
            else if (time < users[usersCount - 1].Time)
            {
                InsertUserResultInUsers(userName, time, users, usersCount);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("{0,5}{1,-25}{2,5}{3,-25}{4,5}{5,-25}", " ", "Простой", " ", "Средний", " ", "Сложный"));
            builder.AppendLine();
            builder.AppendLine();

            for (int i = 0; i < usersCount; i++)
            {
                string beginer = GetHighScoreLinePart(i, beginners, beginnersCount);
                string medium = GetHighScoreLinePart(i, mediums, mediumsCount);
                string expert = GetHighScoreLinePart(i, experts, expertsCount);

                string totalLine = beginer + medium + expert;

                builder.Append(totalLine);
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static string GetHighScoreLinePart(int index, UserResult[] users, int usersCount)
        {
            string user;

            if (usersCount > index)
            {
                // user = string.Format("{0,-7}{1,-4}", users[i].Name, users[i].Time);
                user = string.Format("{0,-5}{1,-13}{2,-7}{3,5}", index + 1, users[index].Name, users[index].Time, " ");
            }
            else
            {
                user = string.Format("{0,-5}{1,-13}{2,-7}{3,5}", index + 1, "-", "-", " ");
                // user = string.Format("{0}\t{1}\t{2,-15}", i + 1, "-", "-");
            }

            return user;
        }
    }
}
