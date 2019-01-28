using System;
using System.Collections.Generic;
using UnityEngine;

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
            try
            {
                if (values.TryGetValue("cycleLenghtMonths", out tmp))
                    cycleLenghtMonths = Convert.ToInt32(tmp);
                if (values.TryGetValue("dayNightCycleEnabled", out tmp))
                    dayNightCycleEnabled = Convert.ToBoolean(tmp);
                if (values.TryGetValue("nextDayNightSwitchTime", out tmp))
                    nextDayNightSwitchTime = Convert.ToInt32(tmp);
            }
            catch
            {
                Debug.Log("[MoreRealism] Couln'd load saved controller");
            }
            base.deserialize(context, values);
        }
    }
}
