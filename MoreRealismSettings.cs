using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismSettings : SerializedRawObject
    {
        public static int VERSION = 1;

        public decimal cycleLenghtMonths = 4m;
        public bool dayNightCycleEnabled = true;
        public bool kickOutGuestsAtNight = false;
        public bool closeEverythingAtNight = false;

        public int nextDayNightSwitchTime = 0;
        public int nextParkStateSwitchTime = 0;
        public int nextEverythingStateSwitchTime = 0;

        public MoreRealismSettings()
        {
            try
            {
                nextDayNightSwitchTime = (int)(ParkInfo.ParkTime + cycleLenghtMonths * 300);
                nextParkStateSwitchTime = (int)(nextDayNightSwitchTime + cycleLenghtMonths * 150);
                if (cycleLenghtMonths < 1)
                    nextEverythingStateSwitchTime = (int)(nextParkStateSwitchTime + (cycleLenghtMonths * 60));
                else
                    nextEverythingStateSwitchTime = nextParkStateSwitchTime + 60;
            }
            catch
            {
                Debug.Log("[MoreRealism] Couldn't set default values for switch times");
            }
        }

        public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {
            values.Add("cycleLenghtMonths", cycleLenghtMonths.ToString().Replace(",", "."));
            values.Add("dayNightCycleEnabled", dayNightCycleEnabled);
            values.Add("kickOutGuestsAtNight", kickOutGuestsAtNight);
            values.Add("closeEverythingAtNight", closeEverythingAtNight);
            values.Add("nextDayNightSwitchTime", nextDayNightSwitchTime);
            values.Add("nextParkStateSwitchTime", nextParkStateSwitchTime);
            values.Add("nextEverythingStateSwitchTime", nextEverythingStateSwitchTime);
            values.Add("settingsVersion", VERSION);

            base.serialize(context, values);
        }

        public override void deserialize(SerializationContext context, Dictionary<string, object> values)
        {
            object tmp;
            int loadedVersion;
            try
            {
                if (values.TryGetValue("cycleLenghtMonths", out tmp))
                    cycleLenghtMonths = decimal.Parse(Convert.ToString(tmp), CultureInfo.GetCultureInfo("en-US"));
                if (values.TryGetValue("dayNightCycleEnabled", out tmp))
                    dayNightCycleEnabled = Convert.ToBoolean(tmp);
                if (values.TryGetValue("kickOutGuestsAtNight", out tmp))
                    kickOutGuestsAtNight = Convert.ToBoolean(tmp);
                if (values.TryGetValue("closeEverythingAtNight", out tmp))
                    closeEverythingAtNight = Convert.ToBoolean(tmp);
                if (values.TryGetValue("nextDayNightSwitchTime", out tmp))
                    nextDayNightSwitchTime = Convert.ToInt32(tmp);
                if (values.TryGetValue("nextParkStateSwitchTime", out tmp))
                    nextParkStateSwitchTime = Convert.ToInt32(tmp);
                if (values.TryGetValue("nextEverythingStateSwitchTime", out tmp))
                    nextEverythingStateSwitchTime = Convert.ToInt32(tmp);

                if (values.TryGetValue("settingsVersion", out tmp))
                    loadedVersion = Convert.ToInt32(tmp);
            }
            catch
            {
                Debug.Log("[MoreRealism] Controller didn't load correctly");
            }
            base.deserialize(context, values);
        }
    }
}
