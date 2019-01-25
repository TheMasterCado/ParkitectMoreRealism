using System.Collections.Generic;

namespace MoreRealism
{
    public class MoreRealismSettings : SerializedRawObject
    {
        public int cycleLenghtMonths = 2;
        public bool dayNightCycleEnabled = false;

        public int nextDayNightSwitchTime = 0;

        public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {
            values.Add("cycleLenghtMonths", cycleLenghtMonths);
            values.Add("dayNightCycleEnabled", dayNightCycleEnabled);
            values.Add("nextDayNightSwitchTime", nextDayNightSwitchTime);

            base.serialize(context, values);
        }

        public override void deserialize(SerializationContext context, Dictionary<string, object> values)
        {
            object tmp;

            if (values.TryGetValue("cycleLenghtMonths", out tmp))
                cycleLenghtMonths = (int)tmp;
            if (values.TryGetValue("dayNightCycleEnabled", out tmp))
                dayNightCycleEnabled = (bool)tmp;
            if (values.TryGetValue("nextDayNightSwitchTime", out tmp))
                nextDayNightSwitchTime = (int)tmp;

            base.deserialize(context, values);
        }
    }
}
