namespace HausMeterApp
{
    public class MeterInFile : MeterBase
    {
        public string FileName { get; private set; }

        public MeterInFile(string address, MeterTypes.MeterType typeMeter) : base(address, typeMeter)
        {
            this.FileName = $"Haus_{address.Replace(" ","_")}_{typeMeter}.txt"; 
        }

        public override event MeterReadingAddedDelegate MeterReadingAdded;

        public override void AddMeterReading(float meterReading)
        {
            if (meterReading >= 0)
            {
                if (GetMeterReadingPrecision(meterReading) <= MeterMaxPrecision)
                {
                    using (var writer = File.AppendText($"{this.FileName}"))
                    {
                        writer.WriteLine(meterReading);
                    }
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
            try
            {
                var meterReadingsFromFile = this.MeterReadingsFromFile();
                foreach (var meterReading in meterReadingsFromFile)
                {
                    statistics.AddMeterReading(meterReading);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File \"{this.FileName}\" not found");
            }
            return statistics;
        }

        private List<float> MeterReadingsFromFile()
        {
            var meterReadings = new List<float>();
            if (File.Exists($"{this.FileName}"))
            {
                using (var reader = File.OpenText($"{this.FileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        float valueFloat = float.Parse(line);
                        meterReadings.Add(valueFloat);
                        line = reader.ReadLine();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return meterReadings;
        }
    }
}
