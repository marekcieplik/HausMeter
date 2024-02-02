namespace HausMeterApp
{
    public class Statistics
    {
        public float Min { get; private set; }
        
        public float Max { get; private set; }
        
        public float Sum { get; private set; }
        
        public int Count { get; private set; }
        
        public float Average 
        {
            get
            {
                return this.Sum / this.Count;
            }
        }
        
        public void AddMeterReading(float meterReading)
        {
            this.Count++;
            this.Sum += meterReading;
            this.Min = Math.Min(this.Min, meterReading);
            this.Max = Math.Max(this.Max, meterReading);
        }
        
        public Statistics()
        {
            this.Min = float.MaxValue;
            this.Max = float.MinValue;
            this.Sum = 0;
            this.Count = 0;
        }
    }
}
