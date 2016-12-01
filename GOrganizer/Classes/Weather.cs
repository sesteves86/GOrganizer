using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    class Weather
    {
        public int Id { get; set; }

        /*public int Q1Temp { get; set; }
        public int Q1Hum { get; set; }
        public int Q1Rain { get; set; }
        public int Q2Temp { get; set; }
        public int Q2Hum { get; set; }
        public int Q2Rain { get; set; }
        public int R1Temp { get; set; }
        public int R1Hum { get; set; }
        public int R1Rain { get; set; }
        public int R2Temp { get; set; }
        public int R2Hum { get; set; }
        public int R2Rain { get; set; }
        public int R3Temp { get; set; }
        public int R3Hum { get; set; }
        public int R3Rain { get; set; }
        public int R4Temp { get; set; }
        public int R4Hum { get; set; }
        public int R4Rain { get; set; }*/

        public int[] Q1 {get;set;}
        public int[] Q2 { get; set; }
        public int[] R1 { get; set; }
        public int[] R2 { get; set; }
        public int[] R3 { get; set; }
        public int[] R4 { get; set; }

        public Weather()
        {
            Q1 = new int[3];
            Q2 = new int[3];
            R1 = new int[3];
            R2 = new int[3];
            R3 = new int[3];
            R4 = new int[3];
        }

        public int GetRaceAvgTemp()
        {
            int temp = 0;

            temp = (int)(
                    R1[(int)WeatherEnum2.Temp] + 
                    R2[(int)WeatherEnum2.Temp] + 
                    R3[(int)WeatherEnum2.Temp] + 
                    R4[(int)WeatherEnum2.Temp]
                    ) / 4;

            return temp;
        }
        public int GetRaceAvgHum()
        {
            int hum = 0;

            hum = (int)(
                    R1[(int)WeatherEnum2.Hum] +
                    R2[(int)WeatherEnum2.Hum] +
                    R3[(int)WeatherEnum2.Hum] +
                    R4[(int)WeatherEnum2.Hum]
                    ) / 4;

            return hum;
        }
        public int GetRaceAvgRain()
        {
            int rain = 0;

            rain = (int)(
                    R1[(int)WeatherEnum2.Rain] +
                    R2[(int)WeatherEnum2.Rain] +
                    R3[(int)WeatherEnum2.Rain] +
                    R4[(int)WeatherEnum2.Rain]
                    ) / 4;

            return rain;
        }

        //Needed?
        static internal int ConvertWeatherEnumToNumber(WeatherEnum2 w)
        {
            int code = -1;

            switch (w)
            {
                case WeatherEnum2.Temp:
                    code = 0;
                    break;
                case WeatherEnum2.Hum:
                    code = 1;
                    break;
                case WeatherEnum2.Rain:
                    code = 2;
                    break;
                default:
                    throw new Exception("Error on WeatherNumber method: 'Invalid argument'");
            }
            return code;
        }
    }
    
    enum WeatherEnum2
    {
        Temp,
        Hum,
        Rain
    }

    //Obsolete?
    enum WeatherEnum
    {
        Q1Temp,
        Q1Hum,
        Q1Rain,
        Q2Temp,
        Q2Hum,
        Q2Rain,
        R1Temp,
        R1Hum,
        R1Rain,
        R2Temp,
        R2Hum,
        R2Rain,
        R3Temp,
        R3Hum,
        R3Rain,
        R4Temp,
        R4Hum,
        R4Rain
    }
}
