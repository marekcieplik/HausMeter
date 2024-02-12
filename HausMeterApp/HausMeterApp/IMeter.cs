using static HausMeterApp.MeterBase;
using static HausMeterApp.MeterTypes;
namespace HausMeterApp
{
    public interface IMeter
    {
        MeterType TypeMeter { get; }

        int MeterMaxPrecision { get; }

        string Address { get; }
        
        void AddMeterReading(float meterReading);

        void AddMeterReading(double meterReading);

        void AddMeterReading(string meterReading);

        event MeterReadingAddedDelegate MeterReadingAdded;

        Statistics GetStatistics();
    }
}
