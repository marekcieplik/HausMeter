using static HausMeterApp.MeterBase;
namespace HausMeterApp
{
    public interface IMeter
    {
        MeterType TypeMeter { get; }

        string Address { get; }
        
        void AddMeterReading(float meterReading);

        void AddMeterReading(double meterReading);

        void AddMeterReading(string meterReading);

        event MeterReadingAddedDelegate MeterReadingAdded;

        Statistics GetStatistics();
    }
}
