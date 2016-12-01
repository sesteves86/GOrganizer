using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public class StaffFacilities
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public int Motivation { get; set; }
        public int Technicalskill { get; set; }
        public int Stresshandling { get; set; }
        public int Concentration { get; set; }
        public int Efficiency { get; set; }
        public int Windtunnel { get; set; }
        public int Pitstoptrainingcenter { get; set; }
        public int RDworkshop { get; set; }
        public int RDdesigncenter { get; set; }
        public int Engineeringworkshop { get; set; }
        public int Alloyandchemicallab { get; set; }
        public int Commercial { get; set; }

        //Constructor? No need

        public int GetExpenses()
        {
            int staffExpenses = 1000 * (11 * Stresshandling + 6 * Concentration + 18 * Efficiency);
            int facilityExpenses = 5000 * (Windtunnel + Pitstoptrainingcenter + RDdesigncenter + RDworkshop +
                    Engineeringworkshop + Alloyandchemicallab + Commercial);

            return staffExpenses + facilityExpenses;
        }
    }

    enum StaffFacilitiesNames
    {
        Overall,
        Experience,
        Motivation,
        Technicalskill,
        Stresshandling,
        Concentration,
        Efficiency,
        Windtunnel,
        Pitstoptrainingcenter,
        RDworkshop, 	
        RDdesigncenter,
        Engineeringworkshop,
        Alloyandchemicallab,
        Commercial
    }
}
