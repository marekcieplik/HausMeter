namespace HausMeterApp
{
    public abstract class MeterBase : MeterTypes, IMeter
    {
        public delegate void MeterReadingAddedDelegate(object sender, EventArgs args);
        
        public abstract event MeterReadingAddedDelegate MeterReadingAdded;

        public MeterType TypeMeter { get; private set; }
        
        public int MeterMaxPrecision
        {
            get
            {
                switch (this.TypeMeter)
                {
                    case MeterTypes.MeterType.Undef:
                        return 7;
                    case MeterTypes.MeterType.Gas:
                        return 3;
                    case MeterTypes.MeterType.Power:
                        return 1;
                    case MeterTypes.MeterType.WaterCold:
                        return 2;
                    default:
                        Console.WriteLine("MeterMaxPrecision Error");
                        throw new NotImplementedException();
                }
            }
        }

        public string Address {get; private set;}  
        
        public MeterBase (string address, MeterTypes.MeterType typeMeter )
        {
            this.TypeMeter = typeMeter;
            this.Address = address;
        }

        public abstract void AddMeterReading(float meterReading);

        public void AddMeterReading(double meterReading)
        {
            float floatValue = (float)meterReading;
            this.AddMeterReading(floatValue);
        }

        public void AddMeterReading(string meterReading)
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

        public abstract Statistics GetStatistics();

        public int GetMeterReadingPrecision(float meterReading)
        {
            int meterPrecision = 0;
            if (Math.Round(meterReading) != meterReading)
            {
                var meterReadingString = meterReading.ToString();  //decimal symbol is not dot. it is comma
                string decimalSeparator = meterReading.ToString().Contains(".") ? "." : ",";
                var meterReadingSplit = meterReadingString.Split(decimalSeparator);
                meterPrecision = meterReadingSplit[1].Length;
            }
            return meterPrecision;
        }
    }
}
