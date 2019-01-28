using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismSettings : SerializedRawObject
    {
        public int cycleLenghtMonths = 2;
        public bool dayNightCycleEnabled = true;
        public bool autoFreeRides = true;
        public bool kickOutGuestsAtNight = true;
        public bool closeEverythingAtNight = true;

        public int nextDayNightSwitchTime = 0;
        public int nextParkStateSwitchTime = 0;
        public int nextEverythingStateSwitchTime = 0;

        public MoreRealismSettings()
        {
            try
            {
                nextDayNightSwitchTime = ParkInfo.ParkTime + cycleLenghtMonths * 300;
                nextParkStateSwitchTime = nextDayNightSwitchTime + cycleLenghtMonths * 150;
                nextEverythingStateSwitchTime = nextParkStateSwitchTime + 30;
            }
            catch
            {
                Debug.Log("[MoreRealism] Couln'd set default values for switch times");
            }
        }

        public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {
            values.Add("cycleLenghtMonths", cycleLenghtMonths);
            values.Add("dayNightCycleEnabled", dayNightCycleEnabled);
            values.Add("autoFreeRides", dayNightCycleEnabled);
            values.Add("kickOutGuestsAtNight", kickOutGuestsAtNight);
            values.Add("nextDayNightSwitchTime", nextDayNightSwitchTime);
            values.Add("nextParkStateSwitchTime", nextParkStateSwitchTime);
            values.Add("nextEverythingStateSwitchTime", nextEverythingStateSwitchTime);

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
                if (values.TryGetValue("autoFreeRides", out tmp))
                    autoFreeRides = Convert.ToBoolean(tmp);
                if (values.TryGetValue("kickOutGuestsAtNight", out tmp))
                    kickOutGuestsAtNight = Convert.ToBoolean(tmp);
                if (values.TryGetValue("nextDayNightSwitchTime", out tmp))
                    nextDayNightSwitchTime = Convert.ToInt32(tmp);
                if (values.TryGetValue("nextParkStateSwitchTime", out tmp))
                    nextParkStateSwitchTime = Convert.ToInt32(tmp);
                if (values.TryGetValue("nextEverythingStateSwitchTime", out tmp))
                    nextEverythingStateSwitchTime = Convert.ToInt32(tmp);
            }
            catch
            {
                Debug.Log("[MoreRealism] Couln'd load saved controller");
            }
            base.deserialize(context, values);
        }
    }
}
