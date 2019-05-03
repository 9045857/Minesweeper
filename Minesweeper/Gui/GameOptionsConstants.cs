using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Gui
{
    class GameOptionsConstants
    {
        public const int LowLevelRowCount = 9;
        public const int LowLevelColumnCount = 9;
        public const int LowLevelMinesCount = 10;

        public const int MediumLevelRowCount = 16;
        public const int MediumLevelColumnCount = 16;
        public const int MediumLevelMinesCount = 40;

        public const int HighLevelRowCount = 16;
        public const int HighLevelColumnCount = 30;
        public const int HighLevelMinesCount = 99;

        public const int CustomLevelMinesCountMin = 0;

        public const int CustomLevelColumnCountMin = 9;
        public const int CustomLevelColumnCountMax = 50;

        public const int CustomLevelRowCountMin = 1;
        public const int CustomLevelRowCountMax = 50;

        public static bool IsMinesCountInPermissibleRange(int rowCount, int columnCount, int minesCount)
        {
            return (minesCount >= CustomLevelMinesCountMin) && (minesCount < rowCount * columnCount);
        }

        public static bool IsRowCountInPermissibleRange(int rowCount)
        {
            return IsValueInPermissibleRange(rowCount, CustomLevelRowCountMin, CustomLevelRowCountMax);
        }

        public static bool IsColumnCountInPermissibleRange(int columnCount)
        {
            return IsValueInPermissibleRange(columnCount,CustomLevelColumnCountMin,CustomLevelColumnCountMax);
        }

        private static bool IsValueInPermissibleRange(int value, int minValue, int maxValue)
        {
            return (value >= minValue) && (value <= maxValue);
        }

        public const string CaptionAboutMessage = "Информация об игре \"Сапер\"";
        public const string CaptionHighScore = "Таблица рекордов";

        public const string WarningCorrectlySetRowCount = "Некорректно задана высота.";
        public const string WarningCorrectlySetColumnCount = "Некорректно задана ширина.";
        public const string WarningCorrectlySetMinesCount = "Некорректно задано количество мин.";

        public static string GetAboutText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Игра \"сапёр\" очень проста.");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("Начните с открытия одной ячейки.");
            builder.AppendLine();
            builder.Append("Число в ячейке показывает, сколько мин скрыто вокруг данной ячейки.");
            builder.Append("Это число поможет понять вам, где находятся безопасные ячейки, ");
            builder.Append("а где находятся бомбы.");
            builder.AppendLine();
            builder.Append("Если рядом с открытой ячейкой есть пустая ячейка, ");
            builder.Append("то она откроется автоматически.");
            builder.AppendLine();
            builder.Append("Если вы открыли ячейку с миной, то игра проиграна.");
            builder.Append("Что бы пометить ячейку, в которой находится бомба, ");
            builder.Append("нажмите её правой кнопкой мыши.");
            builder.AppendLine();
            builder.Append("После того, как вы отметите все мины, можно навести курсор ");
            builder.Append("на открытую ячейку и нажать правую и левую кнопку мыши одновременно.");
            builder.Append("Тогда откроются все свободные ячейки вокруг неё");
            builder.AppendLine();
            builder.Append("Если в ячейке указано число, оно показывает, сколько мин скрыто в ");
            builder.Append("восьми ячейках вокруг данной. Это число помогает понять, ");
            builder.Append("где находятся безопасные ячейки.");
            builder.AppendLine();
            builder.Append("Игра продолжается до тех пор, пока вы не откроете все ");
            builder.Append("не заминированные ячейки.");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("Удачной игры");

            return builder.ToString();
        }

        public static string HighScoreCongratulation = string.Format("Отличный результат!{0}Добавим его в таблицу рекордов?", Environment.NewLine);

        public const int UserNameMaxLengh = 6;

    }
}
