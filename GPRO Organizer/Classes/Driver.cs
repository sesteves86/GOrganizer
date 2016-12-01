using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public class Driver
    {
        public int Id { get; set; }
        public int Energy { get; set; }
        public int Concentration { get; set; }
        public int Talent { get; set; }
        public int Aggressiveness { get; set; }
        public int Experience { get; set; }
        public int TechnicalInsight { get; set; }
        public int Stamina { get; set; }
        public int Charisma { get; set; }
        public int Motivation { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }

        public int Salary { get; set; }
        public int RacesLeft { get; set; }
        public int Reputation { get; set; }

        //Is explicit Constructor needed?

        //Handling Trainning
        public int TrainningCost(DriverTrainning trainning)
        {
            int cost = 0;

            switch (trainning)
            {
                case DriverTrainning.FitnessClass:
                    cost = 700;
                    break;
                case DriverTrainning.Yoga:
                    cost = 700;
                    break;
                case DriverTrainning.PRTraining:
                    cost = 500;
                    break;
                case DriverTrainning.TechnicalTraining:
                    cost = 600;
                    break;
                case DriverTrainning.SportsPsychologist:
                    cost = 400;
                    break;
                case DriverTrainning.NinjaClass:
                    cost = 550;
                    break;
                case DriverTrainning.Spa:
                    cost = 300;
                    break;
            }

            return cost;
        }
        public void DoTrainning(DriverTrainning trainning)
        {
            switch (trainning)
            {
                case DriverTrainning.FitnessClass:
                    Stamina += 2;
                    Motivation -= 2;
                    Weight -= 1;
                    break;
                case DriverTrainning.Yoga:
                    Concentration += 5;
                    Aggressiveness -= 2;                    
                    Stamina -= 2;
                    Motivation += 7;
                    break;
                case DriverTrainning.PRTraining:
                    Concentration -= 3;
                    Charisma += 5;
                    break;
                case DriverTrainning.TechnicalTraining:
                    TechnicalInsight += 5;
                    Motivation -= 5;
                    break;
                case DriverTrainning.SportsPsychologist:
                    Motivation += 17;
                    break;
                case DriverTrainning.NinjaClass:
                    Concentration += 1;
                    Aggressiveness += 4;
                    break;
            }

            //Assure all values are within 0-250
            AssureDriverSkillsValid();
        }

        void AssureDriverSkillsValid()
        {
            if (Concentration > 250) Concentration = 250;
            if (Talent > 250) Talent = 250;
            if (Aggressiveness > 250) Aggressiveness = 250;
            if (Experience > 250) Experience = 250;
            if (TechnicalInsight > 250) TechnicalInsight = 250;
            if (Stamina > 250) Stamina = 250;
            if (Charisma > 250) Charisma = 250;
            if (Motivation > 250) Motivation = 250;

            if (Concentration < 0) Concentration = 0;
            if (Talent < 0) Talent = 0;
            if (Aggressiveness < 0) Aggressiveness = 0;
            if (Experience < 0) Experience = 0;
            if (TechnicalInsight < 0) TechnicalInsight = 0;
            if (Stamina < 0) Stamina = 0;
            if (Charisma < 0) Charisma = 0;
            if (Motivation < 0) Motivation = 0;
        }
        public int GetOA()
        {
            Double oa = Concentration/6 + Talent/4 + Aggressiveness / 6.86 + Experience/12 + TechnicalInsight/8;
            oa += Stamina / 6.86 + Charisma / 12 + Motivation / 12 - Weight / 12;

            return (int)Math.Round(oa, 0);
        }
        public void DriverUpdateAfterRace(int racePosition)
        {
            Experience++;
            TechnicalInsight++;
            RacesLeft--;

            Motivation += (int)(31.3 - 2.1 * racePosition);

            AssureDriverSkillsValid();
        }
    }

    public enum DriverTrainning
    {
        Nothing = 0,
        FitnessClass = 1,
        Yoga, 
        PRTraining,
        TechnicalTraining, 
        SportsPsychologist, 
        NinjaClass,
        Spa
    }
    public enum DriverSkills
    {
        Energy,
        Concentration,
        Talent,
        Aggressiveness,
        Experience,
        TI,
        Stamina,
        Charisma,
        Motivation,
        Reputation,
        Weight,
        Age
    }
    
}
