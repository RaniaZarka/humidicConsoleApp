using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace humidicConsoleApp
{
    class User
    {
       protected int Id { get; set; }
       public string Name { get; set; }
       public int HighLevel { get; set; }
       public int LowLevel { get; set; }
       public int UpdateInterval { get; set; }
       public string Alert { get; set; }
       public User()
        {

        }
       public User(int id, string name, int highLevel, int lowLevel, int updateInterval, string alert)
        {
            Id = id;
            Name = name;
            HighLevel = highLevel;
            LowLevel = lowLevel;
            UpdateInterval = updateInterval;
            Alert = alert;
        }

    }
}
