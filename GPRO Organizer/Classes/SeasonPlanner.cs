using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public class SeasonPlannerTab
    {
        public float StartingBalanceM { get; set; }
        public int CurrentPoints { get; set; }
        public int TargetPoints { get; set; }
        public int Division { get; set; }
        public int nRuns { get; set; }
    }

    class SeasonPlannerForDataTable
    {
        //variables for the dataTable 
        public int SeasonRaceNumber { get; set; }
        public int TrackId { get; set; } //foreign key to Track's Id
        public int QualPosition { get; set; }
        public int RacePosition { get; set; }

        public float BalanceAfterRaceM { get; set; }
        public int[] CarWearAfterRace { get; set; }
        public int[] CarPartsChanged { get; set; } //new parts, downgrades
        public float[] FacilitiesLevel { get; set; }
    }

    /// <summary>
    /// The SeasonPlannerForOptimizer, SeasonPlannerForDataTable and SeasonPlannerDecisions totally define a complete line for optimization.
    /// The SeasonPlannerForOptimizer has all initial values 
    /// </summary>
    class SeasonPlannerForOptimizer
    {
        public int Id { get; set; }
        public Car car { get; set; }
        public Driver driver { get; set; }
        public TechnicalDirector technicalDirector { get; set; }
        public RaceTab raceTab { get; set; }
        public StaffFacilities staffFacilities { get; set; }
        public SeasonPlannerTab spTab { get; set; }
    }

    class SeasonPlannerDecision:ICloneable
    {
        public int SeasonRace { get; set; }
        public DriverTrainning Training { get; set; }
        public bool Testing { get; set; }
        public int TargetCarLevelEngBra { get; set; }
        public int TargetCarLevelOthers { get; set; }
        public int CT { get; set; }

        public void GenerateRandomDecisionValues()
        {
            Random r = new Random();
            Training = (DriverTrainning)(r.Next(7));
            Testing = bool.Parse(r.Next(1).ToString());
            TargetCarLevelEngBra = r.Next(9);
            CT = r.Next(100);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public bool IsNull()
        {
            if (SeasonRace == 0 && Training == DriverTrainning.Nothing && Testing == false &&
                TargetCarLevelEngBra == 0 && TargetCarLevelOthers == 0 && CT == 0)
                return true;
            else
                return false;
        }
        public static bool IsNull(SeasonPlannerDecision spDecision)
        {
            if (spDecision.SeasonRace == 0 && spDecision.Training == DriverTrainning.Nothing && spDecision.Testing == false &&
                spDecision.TargetCarLevelEngBra == 0 && spDecision.TargetCarLevelOthers == 0 && spDecision.CT == 0)
                return true;
            else
                return false;
        }
    }

    class SeasonPlannerFullLine
    {
        public SeasonPlannerDecision spDecisions { get; set; }
        public SeasonPlannerForDataTable spTable { get; set; }
        public SeasonPlannerForOptimizer spOptimizer { get; set; }
        public SeasonPlannerTab spTab { get; set; }

        public SeasonPlannerFullLine() { }
        public SeasonPlannerFullLine(SeasonPlannerDecision _spDecisions, 
            SeasonPlannerForDataTable _spTable, SeasonPlannerForOptimizer _spOptimizer,
            SeasonPlannerTab _spTab)
        {
            spDecisions = _spDecisions;
            spTable = _spTable;
            spOptimizer = _spOptimizer;
            spTab = _spTab;
        }
    }

    class SeasonPlannerResult
    {
        public float finalBalanceM;
        public int finalPoints;
        public SeasonPlannerDecision[] spDecisions;
        public Random random;
    }

    public enum Divisions
    {
        Elite,
        Master,
        Pro,
        Amateur,
        Rookie
    }
}
