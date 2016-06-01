using System.Collections.Generic;

namespace CalcEngine
{
    public class GasPlan
    {
        public List<GasPlanPart> Parts { get; set; }

        public GasPlan()
        {
            Parts = new List<GasPlanPart>();
        }

        public class GasPlanPart
        {
            public enum ActionTypeEnum
            {
                Descend,
                Ascend,
                DecoStop,
                GasSwitch,
                Level
            }

            public double Minutes { get; set; }
            public double Depth { get; set; }
            public ActionTypeEnum Action { get; set; }

            public Gas BestGas { get; set; }

            public double DecoCeiling { get; set; }

            public List<Gas> RemainingGas { get; set; }
        }
    }
}