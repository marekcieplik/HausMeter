namespace HausMeterApp
{
    public class MeterInMemory : MeterBase
    {
        private List<float> meterReadings = new List<float>();
        
        public MeterInMemory(string address, MeterType typeMeter) : base(address, typeMeter)
        {
        }

        public override event MeterReadingAddedDelegate MeterReadingAdded;

        public override void AddMeterReading(float meterReading)
        {
            int meterPrecision = 0;
            if (Math.Round(meterReading) != meterReading) 
            {
                var meterReadingString = meterReading.ToString();  //decimal symbol is not dot. it is comma
                string decimalSeparator = meterReading.ToString().Contains(".") ? "." : ",";
                var meterReadingSplit = meterReadingString.Split(decimalSeparator);
                meterPrecision = meterReadingSplit[1].Length;
            }

            if (meterReading >= 0) 
            {
                if (meterPrecision <= MeterMaxPrecision)
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

        public override void AddMeterReading(double meterReading)
        {
            float floatValue = (float)meterReading;
            this.AddMeterReading(floatValue);
        }

        public override void AddMeterReading(string meterReading)
        {
            if (float.TryParse(meterReading, out float floatValue)) 
            { 
                this.AddMeterReading(floatValue); 
            }
            else 
            {
                throw new Exception("string is not float");
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
