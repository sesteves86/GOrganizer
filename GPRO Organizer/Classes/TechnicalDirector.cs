using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public class TechnicalDirector
    {
        public int Id { get; set; }
        public int Overall { get; set; }
        public int Leadership { get; set; }
        public int RDmechanics { get; set; }
        public int RDelectronics { get; set; }
        public int RDaerodynamics { get; set; }
        public int Experience { get; set; }
        public int Pitcoordination { get; set; }
        public int Motivation { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public int RacesLeft { get; set; }

        //Constructor?

        //Calculate OA
    }

    enum TDSkills
    {
        Overall,
        Leadership,
        RDmechanics,	
        RDelectronics, 	
        RDaerodynamics,	
        Experience,	
        Pitcoordination,	
        Motivation,	
        Age,
        Salary,
        RacesLeft
    }
}
