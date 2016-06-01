using System.Collections.Generic;

namespace CalcEngine
{
    public class DivePlan
    {
        public DivePlan()
        {
            Parts = new List<DivePlanPart>();
        }

        public List<DivePlanPart> Parts { get; set; }

        public class DivePlanPart
        {
            public DivePlanPart(double depth, int minutes)
            {
                Depth = depth;
                Time = minutes;
            }

            public int Time { get; set; }
            public double Depth { get; set; }
        }
    }
}