using System;
using System.Collections.Generic;
using System.Text;

namespace humidicConsoleApp
{
   public  class Humidity
    {
    
      public float Level { get; set; }
      public  DateTime Date { get; set; }


        }
        public Humidity( float level, DateTime date)
        {
            
            Level = level;
            Date = date;

        }


       
    }
}
