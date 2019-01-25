using System.Collections.Generic;

namespace MoreRealism
{
    public class MoreRealismSettings : SerializedRawObject
    {
        public int cycleLenghtMonths = 4;
        public bool dayNightCycleEnabled = false;

        /*public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {

        }

        public override void deserialize(SerializationContext context, Dictionary<string, object> values)
        {

        }*/
    }
}
