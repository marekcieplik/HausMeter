namespace HausMeterApp
{
    public class MeterInFile : MeterBase
    {
        public string FileName { get; private set; }

        public MeterInFile(string address, MeterType typeMeter) : base(address, typeMeter)
        {
            this.FileName = $"Haus_{address.Replace(" ","_")}_{typeMeter}.txt"; 
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

        public override void AddMeterReading(double meterReading)
        {
            float valueFloat = (float)meterReading;
            AddMeterReading(valueFloat);
        }

        public override void AddMeterReading(string meterReading)
        {
            if (float.TryParse(meterReading, out float valueFloat))
            {
                AddMeterReading(valueFloat);
            }
            else
            {
                throw new Exception("string is not float value");
            }
        }

        public override Statistics GetStatistics()
        {
            var meterReadingsFromFile = this.MeterReadingsFromFile();
            var statistics = new Statistics();
            foreach (var meterReading in meterReadingsFromFile)
            {
                statistics.AddMeterReading(meterReading);
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
