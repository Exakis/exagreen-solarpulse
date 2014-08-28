using System;

namespace Exagreen.SolarPulse.Core.Models
{
    public class SolarPulseEntry
    {
        public Guid Id { get; set; }
      //  public Guid DeviceId { get; set; }
        public double Value { get; set; }

       public DateTime Timestamp { get; set; }
    }
}