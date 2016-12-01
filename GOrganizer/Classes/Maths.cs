using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public static class Maths
    {
        static public class NormalDistribution
        {
            //public static float GetCumulativeDistribution(float x, float mean = 0, float sd = 1)
            public static float GetCumulativeDistribution(float mean, float x = 0, float sd = 1)
            {
                float F;

                F = 0.5f * (1 + ERF((x - mean) / (float)(sd * Math.Sqrt(2))));

                return F;
            }
        }

        static float ERF(float x)
        {
            //https://en.wikipedia.org/wiki/Error_function#Approximation_with_elementary_functions
            double erf;
            float p = 0.3275911f;
            float t = 1 / (1 + p * x);
            float a1 = 0.254829592f;
            float a2 = -0.284496736f;
            float a3 = 1.421413741f;
            float a4 = -1.453152027f;
            float a5 = 1.061405429f;

            erf = 1 - 
                (
                    a1 * t + a2 * t * t + a3 * Math.Pow(t,3) + a4 * Math.Pow(t,4) + a5 * Math.Pow(t, 5)
                ) * Math.Exp(-x * x);

            //erf = 1 - 1 /
            //    (float)Math.Pow(1 + a1 * x + a2 * x * x + a3 * x * x * x + a4 * x * x * x * x, 4);

            return (float)erf;
        }
    }
}
