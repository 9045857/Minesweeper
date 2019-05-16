using System;

namespace Logic
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