using static HausMeterApp.MeterBase;
namespace HausMeterApp.Tests
{
    public class Tests
    {
        [Test]
        public void NameOfFile_FromAdressAndTypeMeter()
        {
            string nameOfFile = "Haus_Partyzantów_4m13_Gas.txt";
            var meterType = MeterBase.MeterType.Gas;
            var meterAdress = "Partyzantów 4m13";
            var meter = new MeterInFile(meterAdress, meterType);
            Assert.That(nameOfFile, Is.EqualTo(meter.FileName));
        }

        [Test]
        public void GetStatistisc_ShouldReturnCorrectMaxMin()
        {
            var meterType = MeterBase.MeterType.Gas;
            var meterAdress = "Partyzantów 4m13";
            var meter = new MeterInMemory(meterAdress, meterType);
            meter.AddMeterReading(0.001f);
            meter.AddMeterReading(0.10f);            
            var statistisc = meter.GetStatistics();
            Assert.That(statistisc.Max, Is.EqualTo(0.10f));
            Assert.That(statistisc.Min, Is.EqualTo(0.001f));
        }
        
        [Test]
        public void AddMeterReadingGas_ThrowsExceptionWhenWrrongPrecisionIsUsed()
        {
            var meterType = MeterBase.MeterType.Gas;
            var meter = new MeterInMemory("meterAdress", meterType);
            Assert.That(() => meter.AddMeterReading(0.0001f), Throws.Exception.With.Message.EqualTo($"the meter reading must have a maximum of 3 decimal places"));
        }
        
        [Test]
        public void AddMeterReadingPower_ThrowsExceptionWhenWrrongPrecisionIsUsed()
        {
            var meterType = MeterBase.MeterType.Power;
            var meter = new MeterInMemory("meterAdress", meterType);
            Assert.That(() => meter.AddMeterReading(0.001f), Throws.Exception.With.Message.EqualTo($"the meter reading must have a maximum of 1 decimal places"));
        }
        
        [Test]
        public void AddMeterReadingWaterCold_ThrowsExceptionWhenWrrongPrecisionIsUsed()
        {
            var meterType = MeterBase.MeterType.WaterCold;
            var meter = new MeterInMemory("meterAdress", meterType);
            Assert.That(() => meter.AddMeterReading(0.001f), Throws.Exception.With.Message.EqualTo($"the meter reading must have a maximum of 2 decimal places"));
        }
    }
}