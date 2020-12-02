using System;
using System.Collections.Generic;
using System.Text;

namespace humidicConsoleApp
{
   public  class Humidity
    {
    
      public int Level { get; set; }
      public  DateTime Date { get; set; }

        public Humidity()
        {

        }
        public Humidity( DateTime date)
        {
            Date = date;
        }
        
        public Humidity(int level, DateTime date)
        {
            
            Level = level;
            Date = date;

        }


       
    }
}
