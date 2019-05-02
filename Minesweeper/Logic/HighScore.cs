using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minesweeper.Logic
{
    class HighScore
    {
        private UserResult[] beginners;
        private UserResult[] mediums;
        private UserResult[] experts;

        private readonly int usersCount = 10;

        private int beginnersCount;
        private int mediumsCount;
        private int expertsCount;

        private const string beginnerCaption = "beginners";
        private const string mediumCaption = "mediums";
        private const string expertCaption = "experts";

        //образец строки в файле рекорда
        //beginners: "userName" 10

        private string highScoreFileName = "highScore.txt";

        public HighScore()
        {
            beginners = new UserResult[usersCount];
            mediums = new UserResult[usersCount];
            experts = new UserResult[usersCount];

            LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            using (StreamReader reader = new StreamReader(highScoreFileName, Encoding.Default))
            {
                beginnersCount = 0;
                mediumsCount = 0;
                expertsCount = 0;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(beginnerCaption))
                    {
                        //beginners: "userName" 10

                        FillUserResult(line, beginners, beginnersCount);
                    }
                    else if (line.Contains(mediumCaption))
                    {
                        //mediums: "userName" 10

                        FillUserResult(line, mediums, mediumsCount);
                        // mediumsCount++;
                    }
                    else if (line.Contains(expertCaption))
                    {
                        //experts: "userName" 10

                        FillUserResult(line, experts, expertsCount);
                        // expertsCount++;
                    }
                }
            }
        }

        private void FillUserResult(string line, UserResult[] userResults, int usersCount)
        {
            int firstQuotesIndex = line.IndexOf('"');
            int lastQuotesIndex = line.LastIndexOf('"');

            int lenght = lastQuotesIndex - firstQuotesIndex;
            int nextSymbol = 1;

            string userName = line.Substring(firstQuotesIndex + nextSymbol, lenght);
            userResults[usersCount].Name = userName;

            int firstTimeIndex = line.LastIndexOf('"') + nextSymbol + nextSymbol;
            lenght = line.Length - firstTimeIndex - nextSymbol - nextSymbol;

            string time = line.Substring(firstQuotesIndex, lenght);
            userResults[usersCount].Time = Convert.ToInt16(time);

            usersCount++;
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

        private bool IsTimeInFirstTenResults(string userName, int time, UserResult[] users, int usersCount)
        {
            if (usersCount < this.usersCount)
            {
                if (time < users[usersCount - 1].Time)
                {
                    InsertUserResultInUsers(userName, time, users, usersCount);
                }
                else
                {
                    users[usersCount] = new UserResult(userName, time);
                }

                usersCount++;
                return true;
            }
            else
            {
                if (time < users[usersCount - 1].Time)
                {
                    InsertUserResultInUsers(userName, time, users, usersCount);
                    return true;
                }
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

        private bool IsTimeInFirstTenResults(string userName, int time, int rowCount, int columnCount, int minesCount)
        {
            GameParameters.GameTypeLevel gameType = GetGameType(rowCount, columnCount, minesCount);

            switch (gameType)
            {
                case GameParameters.GameTypeLevel.Beginner:
                    return IsTimeInFirstTenResults(userName, time, beginners, beginnersCount);

                case GameParameters.GameTypeLevel.Medium:
                    return IsTimeInFirstTenResults(userName, time, mediums, mediumsCount);

                case GameParameters.GameTypeLevel.Expert:
                    return IsTimeInFirstTenResults(userName, time, experts, expertsCount);

                default:
                    return false;
            }
        }

        private void WriteHighScoreInFile()
        {
            //пример
            //beginners: "userName" 10

            using (StreamWriter writer = new StreamWriter(highScoreFileName, false, Encoding.Default))
            {
                foreach (UserResult user in beginners)
                {
                    string line = string.Format("beginners: \"{0}\" {1}", user.Name, user.Time);
                    writer.WriteLine(line);
                }

                foreach (UserResult user in mediums)
                {
                    string line = string.Format("mediums: \"{0}\" {1}", user.Name, user.Time);
                    writer.WriteLine(line);
                }

                foreach (UserResult user in experts)
                {
                    string line = string.Format("experts: \"{0}\" {1}", user.Name, user.Time);
                    writer.WriteLine(line);
                }
            }
        }
        
        public void AddResultIfItHighScore(string userName, int time, int rowCount, int columnCount, int minesCount)
        {



        }





        //public void AddBeginer(string name, int time)
        //{
        //    PersonalResult personalResult = CreateAndFillNewPersonalResult(name, time);
        //    beginers.Add(personalResult);
        //}

        //public void AddMedium(string name, int time)
        //{
        //    PersonalResult personalResult = CreateAndFillNewPersonalResult(name, time);
        //    mediums.Add(personalResult);
        //}

        //public void AddExpert(string name, int time)
        //{
        //    PersonalResult personalResult = CreateAndFillNewPersonalResult(name, time);
        //    experts.Add(personalResult);
        //}

        //private static PersonalResult CreateAndFillNewPersonalResult(string name, int time)
        //{
        //    PersonalResult personalResult = new PersonalResult();
        //    personalResult.Name = name;
        //    personalResult.Time = time;

        //    return personalResult;
        //}



    }
}
