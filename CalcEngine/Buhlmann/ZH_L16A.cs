using System;
using System.Collections.Generic;

namespace CalcEngine.Buhlmann
{
    public interface IDiveCalcAlgorithm
    {
        GasPlan Generate(List<Gas> availableGasses, List<DivePlan.DivePlanPart> divePlan, Personal personal);
    }

    /// <summary>
    /// ftp://squiplanche.hd.free.fr/pub/Plongee/Articles/ZH-L16.pdf
    /// </summary>
    /// <seealso cref="CalcEngine.Buhlmann.IDiveCalcAlgorithm" />
    public class Zh_L16A : IDiveCalcAlgorithm
    {
        private const int Compartments = 16;

        internal readonly double[] HtNMinutes = { 0, 4, 8, 12.5, 18.5, 27, 38.3, 54.3, 77, 109, 146, 187, 239, 305, 390, 498, 635 };
        private double[] _n2AValue = { 0, 1.2599210498948732, 1, 0.86177387601275357, 0.75620478226714372, 0.66666666666666674, 0.59333104279134785, 0.528157420362247, 0.47011028632085433, 0.41868541237233853, 0.37982106198575216, 0.34974334561894677, 0.32227802635985892, 0.297118743106386, 0.27374222525548308, 0.2523210876661755, 0.23268698225807122 };
        private double[] _n2BValue = { 0, 0.50499999999999989, 0.65144660940672616, 0.72215728752538089, 0.77250472251236135, 0.81254991027012458, 0.84341515982490012, 0.869293676412961, 0.89103942354036192, 0.90921737147788473, 0.92223941113976315, 0.93187275758728683, 0.94031537726468484, 0.947740166568613, 0.9543630316458166, 0.96018892850517779, 0.96531621049337268 };

        internal readonly double[] HtHeMinutes = { 0, 1.5, 3, 4.7, 7, 10.2, 14.5, 20.5, 29.1, 41.1, 55.1, 70.6, 90.2, 115.1, 147.2, 187.9, 239.6 };
        private double[] _heAValue = { 0, 1.7471609294725978, 1.3867225487012695, 1.1939808913978389, 1.0455159171494204, 0.92221023617500564, 0.82017651263138547, 0.73076663218297111, 0.65022799839672785, 0.57953907290408946, 0.52558883303815107, 0.48390684122158423, 0.44595853812463876, 0.41115434843416182, 0.37878612208232287, 0.349184053600252, 0.3220087881172739 };
        private double[] _heBValue = { 0, 0.18850341907227386, 0.42764973081037416, 0.54373439598555739, 0.62703552699077258, 0.69188785445742518, 0.74238713428055481, 0.78413694785030685, 0.81962400055998375, 0.84901634623041733, 0.87028244239640185, 0.88598611026855512, 0.89970767121433459, 0.91179003644089407, 0.92257744082552651, 0.93204809956912116, 0.94039641879502711 };

        private List<Gas> _availableGasses;
        private List<DivePlan.DivePlanPart> _divePlan;
        private Personal _personal;

        public GasPlan Generate(List<Gas> availableGasses, List<DivePlan.DivePlanPart> divePlan, Personal personal)
        {
            _availableGasses = availableGasses;
            _divePlan = divePlan;
            _personal = personal;

            GasPlan gasPlan = new GasPlan();

            // initialise compartments for sea level
            double[] n2Compartment = { 0, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 };
            double[] heCompartment = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            bool finished = false;
            
            gasPlan.Parts.Add(CalculateDescent(divePlan[0], personal));

            foreach (DivePlan.DivePlanPart part in divePlan)
            {
                gasPlan.Parts.Add(CalculateGasPart());
            }

            gasPlan.Parts.AddRange(CalculateAscent());

            return gasPlan;
        }

        private IEnumerable<GasPlan.GasPlanPart> CalculateAscent()
        {
            throw new NotImplementedException();
        }

        private GasPlan.GasPlanPart CalculateGasPart()
        {
            throw new NotImplementedException();
        }

        private GasPlan.GasPlanPart CalculateDescent(DivePlan.DivePlanPart divePlanPart, Personal personal)
        {
            GasPlan.GasPlanPart part = new GasPlan.GasPlanPart
                                           {
                                               Action = GasPlan.GasPlanPart.ActionTypeEnum.Descend,
                                               Minutes = divePlanPart.Depth / personal.DescentRate,
                                               Depth = divePlanPart.Depth
                                           };

            // TODO: add bestgas, decoceiling and remaininggas

            return part;
        }

        /// <summary>
        /// Calculates the compartment.
        /// </summary>
        /// <param name="pBegin">Inert gas pressure in the compartment before the exposure time in bar.</param>
        /// <param name="pGas">Inert gas pressure in the mixture being breathed in bar.</param>
        /// <param name="exposure">The exposure.</param>
        /// <param name="tHt">Half time of the compartment.</param>
        /// <returns> Inert gas pressure in the compartment after the exposure time in bar </returns>
        internal  double CalcCompartment(double pBegin, double pGas, double exposure, double tHt)
        {
            return pBegin + (pGas - pBegin) * (1 - Math.Pow(2, -(exposure / tHt)));
        }

        internal  double CalcAValue(double tHt)
        {
            return 2 * Math.Pow(tHt, -((double)1 / 3));
        }

        internal  double CalcBValue(double tHt)
        {
            return 1.005 - Math.Pow(tHt, -((double)1 / 2));
        }

        internal  double CalcSafeAscentDepth(double pComp, double tHt)
        {
            return (pComp - CalcAValue(tHt)) * CalcBValue(tHt);
        }

        internal double CalcSafeAscentDepth(double pComp, double a, double b)
        {
            return (pComp - a) * b;
        }
    }


}
