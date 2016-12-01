using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    class RaceTab
    {
        public int Id { get; set; }
        public int SeasonTrackIndex { get; set; }
        public int CustomLap1 { get; set; }
        public int CustomLap2 { get; set; }
        public int Compound { get; set; }
        public int CT { get; set; }

        public int Temp { get; set; }
        public int Hum {get; set; }
        public bool Rain { get; set; }
    }
}
