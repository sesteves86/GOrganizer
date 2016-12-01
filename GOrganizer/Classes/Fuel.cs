using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    static class Fuel
    {
        public static Single ConsumptionDryLap(Single lapDistanceKm, Single fuelConstant, Byte engineLvl, 
            Byte electronicsLvl, Byte experience, Byte technicalInsight, Byte aggressiveness)
        {
            float engineLvlConstant = 0.028f;
            float electronicsLvlConstant = 0.01f;
            float experienceConstant = 0.00025f;
            float technicalInsightConstant = 0.0005f;
            float aggressivenessConstant = 0.00015f;

            Single consumption = lapDistanceKm/(
                fuelConstant + 
                engineLvl * engineLvlConstant + 
                electronicsLvl * electronicsLvlConstant + 
                experience * experienceConstant + 
                technicalInsight * technicalInsightConstant - 
                aggressiveness * aggressivenessConstant
            );

            return consumption;
        }

        public static Single ConsumpionWetLap(Single lapDistanceKm, Single fuelConstant, Byte engineLvl,
            Byte electronicsLvl, Byte experience, Byte technicalInsight, Byte aggressiveness)
        {
            Single consumption = ConsumptionDryLap(lapDistanceKm, fuelConstant, engineLvl, electronicsLvl, experience, technicalInsight, aggressiveness);

            return consumption / 1.333f;
        }
    }
}
