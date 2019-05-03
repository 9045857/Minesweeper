using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    [Serializable]
    class UserResult
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public UserResult()
        {
        }

        public UserResult(string name, int time)
        {
            Name = name;
            Time = time;
        }
    }
}
