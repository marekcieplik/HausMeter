namespace HausMeterApp
{
    public abstract class MeterBase : IMeter
    {
        public delegate void MeterReadingAddedDelegate(object sender, EventArgs args);
        
        public abstract event MeterReadingAddedDelegate MeterReadingAdded;
        
        public MeterType TypeMeter { get; private set; }
        
        public string Address {get; private set;}
        
        public enum MeterType { Undef, Gas, Power, WaterCold }
        
        public int MeterMaxPrecision 
        {
            get
            {
                switch (this.TypeMeter)
                {
                    case MeterType.Undef:
                        return 7;
                    case MeterType.Gas:
                        return 3;
                    case MeterType.Power:
                        return 1;
                    case MeterType.WaterCold:
                        return 2;
                    default:
                        Console.WriteLine("MeterMaxPrecision Error");
                        throw new NotImplementedException();
                }
            }
        }
        
        public MeterBase (string address, MeterType typeMeter )
        {
            this.TypeMeter = typeMeter;
            this.Address = address;
        }

        public abstract void AddMeterReading(float meterReading);
        
        public abstract void AddMeterReading(double meterReading);
        
        public abstract void AddMeterReading(string meterReading);
        
        public abstract Statistics GetStatistics();
    }
}
