using System;
using System.Text;

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
            else if (rowCount == GameLogicConstants.MediumLevelRowCount && columnCount == GameLogicConstants.MediumLevelColumnCount && minesCount == GameLogicConstants.MediumLevelMinesCount)
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
            while ((usersCount - 1 - i >= 0) && (time < users[usersCount - 1 - i].Time))
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

        private string GetCheckedUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return GameLogicConstants.DefaultUserName;
            }
            else if (userName.Length > GameLogicConstants.HighScoreUserNameMaxLengh)
            {
                return userName.Substring(0, GameLogicConstants.HighScoreUserNameMaxLengh);
            }
            else
            {
                return userName;
            }
        }

        public void Clear()
        {
            beginnersCount = 0;
            mediumsCount = 0;
            expertsCount = 0; 
        }

        public void AddHighScore(string userName, int time, int rowCount, int columnCount, int minesCount)
        {
            userName = GetCheckedUserName(userName);

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
            string space = "  | ";
            string totalCaption = GetFormatCaption(GameLogicConstants.HighScoreBeginnerCaption) + space + GetFormatCaption(GameLogicConstants.HighScoreMediumCaption) + space + GetFormatCaption(GameLogicConstants.HighScoreExpertCaption);

            StringBuilder builder = new StringBuilder();
            builder.Append(totalCaption);
            builder.AppendLine();
            builder.AppendLine();

            for (int i = 0; i < usersCount; i++)
            {
                string beginer = GetHighScoreLinePart(i, beginners, beginnersCount);
                string medium = GetHighScoreLinePart(i, mediums, mediumsCount);
                string expert = GetHighScoreLinePart(i, experts, expertsCount);

                string totalLine = beginer + space + medium + space + expert;

                builder.Append(totalLine);
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static string GetFormatCaption(string caption)
        {
            string beforText = "";
            int beforCaptionSpace = beforText.Length;
            int captionSpace = GameLogicConstants.NumberSpaceHighScore + GameLogicConstants.UserNameSpaceHighScore + GameLogicConstants.UserTimeSpaceHighScore;

            SetSpaces(ref beforCaptionSpace, ref captionSpace, caption.Length);

            return beforText.PadRight(beforCaptionSpace, ' ') + caption.PadRight(captionSpace, ' ');
        }

        private static string GetHighScoreLinePart(int index, UserResult[] users, int usersCount)
        {
            string userData;

            int numberSpace = GameLogicConstants.NumberSpaceHighScore;
            int userNameSpace = GameLogicConstants.UserNameSpaceHighScore;
            int userTime = GameLogicConstants.UserTimeSpaceHighScore;

            int correctionIndexForPrint = 1;

            if (usersCount > index)
            {
                int userNameLength = users[index].Name.Length;
                SetSpaces(ref numberSpace, ref userNameSpace, userNameLength);

                userData = (index + correctionIndexForPrint).ToString().PadRight(numberSpace, ' ') + users[index].Name.PadRight(userNameSpace, ' ') + users[index].Time.ToString().PadLeft(userTime, ' ');
            }
            else
            {
                string freeName = "-";
                int userNameLength = freeName.Length;
                SetSpaces(ref numberSpace, ref userNameSpace, userNameLength);

                userData = (index + correctionIndexForPrint).ToString().PadRight(numberSpace, ' ') + freeName.PadRight(userNameSpace, ' ') + freeName.PadLeft(userTime, ' ');
            }

            return userData;
        }

        private static void SetSpaces(ref int numberSpace, ref int userNameSpace, int userNameLength)
        {
            int userNameFreeSpace = userNameSpace - userNameLength;
            int userNameFreeSpaceBeforName = userNameFreeSpace / 2;
            int userNameFreeSpaceAfterName = userNameFreeSpace - userNameFreeSpaceBeforName;

            numberSpace += userNameFreeSpaceBeforName;
            userNameSpace = userNameLength + userNameFreeSpaceAfterName;
        }
    }
}
