namespace HausMeterApp
{
    public class MeterInMemory : MeterBase
    {
        private List<float> meterReadings = new List<float>();
        
        public MeterInMemory(string address, MeterTypes.MeterType typeMeter) : base(address, typeMeter)
        {
        }

        public override event MeterReadingAddedDelegate MeterReadingAdded;

        public override void AddMeterReading(float meterReading)
        {
            if (meterReading >= 0) 
            {
                if (GetMeterReadingPrecision(meterReading) <= MeterMaxPrecision)
                {
                    this.meterReadings.Add(meterReading);
                    if (MeterReadingAdded != null)
                    {
                        MeterReadingAdded(this, new EventArgs());
                    }
                }
                else
                {
                    throw new Exception($"the meter reading must have a maximum of {this.MeterMaxPrecision} decimal places");
                }
            }
            else
            {
                throw new Exception("the meter reading must be greater than zero");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            foreach (var meterReading in this.meterReadings)
            {
                statistics.AddMeterReading(meterReading);
            }

            return statistics;
        }
    }
}
