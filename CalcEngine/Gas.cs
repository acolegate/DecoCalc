using System;

namespace CalcEngine
{
    public class Gas
    {
        public Gas(int litres, double o2Percent, double? hePercent = null)
        {
            if (litres <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(litres), "litres must be >= 1");
            }

            double hePercentToUse;

            double n2Percent;

            if (o2Percent < 0.01 || o2Percent > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(o2Percent), "o2Percent must be in the range 0.01 to 1.00");
            }

            if (hePercent != null && (hePercent < 0.01 || hePercent > 1.0))
            {
                throw new ArgumentOutOfRangeException(nameof(hePercent), "hePercent must be in the range 0.01 to 1.00");
            }

            if (hePercent == null)
            {
                hePercentToUse = 0;
                n2Percent = Math.Abs(1 - o2Percent);
            }
            else
            {
                hePercentToUse = (double)hePercent;

                n2Percent = Math.Abs(1 - o2Percent - hePercentToUse);
            }

            if (o2Percent + hePercentToUse + n2Percent != 1)
            {
                throw new ArgumentException("Mixes should add up to 1.0");
            }

            O2Percent = o2Percent;
            HePercent = hePercentToUse;
            N2Percent = n2Percent;
            Litres = litres;
        }

        public int Litres { get; set; }

        public double O2Percent { get; }

        public double HePercent { get; }

        public double N2Percent { get; }
    }
}