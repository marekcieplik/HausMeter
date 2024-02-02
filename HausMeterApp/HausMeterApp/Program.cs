using HausMeterApp;
using static HausMeterApp.MeterBase;

Console.WriteLine("App: HausMeter statistics ");
Console.WriteLine("==========================");
MeterSet meterSet = new MeterSet("NoAddress", MeterType.Undef);
bool CloseApp = false;
while (!CloseApp)
{
    Console.WriteLine($"For \taddress: {meterSet.Address} \tmeter type: {meterSet.TypeMeter}\nselect an option: ");
    Console.WriteLine("\t\t1 - Add counter readings to the txt file and show statistics");
    Console.WriteLine("\t\t2 - Add counter readings to the program memory and show statistics");
    Console.WriteLine("\t\t0 - Set new Address and/or new Type of Meter");
    Console.WriteLine("\t\tx - Close app");
    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":            
            AddMeterReadingToTxtFile(meterSet.Address, meterSet.TypeMeter);
            break;
        case "2":
            AddMeterReadingToMemory(meterSet.Address, meterSet.TypeMeter); 
            break;
        case "0":
            meterSet = SetMeterParameters();
            break;
        case "x":
            CloseApp = true;
            Console.WriteLine("Exit Application, Thanks, Bye");
            break;
        default: 
            Console.WriteLine("Invalid operation");
            continue;
    }
}

void AddMeterReadingToMemory(string address, MeterBase.MeterType typeMeter)
{
    var meter = new MeterInMemory(address, typeMeter);
    meter.MeterReadingAdded += MeterReadingAdded;
    string input;
    do
    {
        Console.WriteLine("Add meter reading, or 'q' - quit, or 's' - statistics");
        input = Console.ReadLine();
        if (input.ToLower() == "q")
        {
            break;
        }
        else if (input.ToLower() == "s")
        {
            var statistics = meter.GetStatistics();
            Console.WriteLine($"Haus  a d d r e s s: {meter.Address}");
            Console.WriteLine($"M e t e r   t y p e: {meter.TypeMeter}");
            Console.WriteLine($"Meter Max Precision: {meter.MeterMaxPrecision}");
            Console.WriteLine($"Max  Meter  Reading: {statistics.Max}");
            Console.WriteLine($"Min  Meter  Reading: {statistics.Min}");
            Console.WriteLine($"Average Consumption in {statistics.Count} mounths: {Math.Round(statistics.Average,meter.MeterMaxPrecision)}");
        }
        else
        {
            try
            {
                meter.AddMeterReading(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
        }
    }
    while(true);
}

void AddMeterReadingToTxtFile(string address, MeterType typeMeter)
{
    var meter = new MeterInFile(address, typeMeter );
    meter.MeterReadingAdded += MeterReadingAdded;
    string input;
    do
    {
        Console.WriteLine("Add meter reading, or 'q' - quit, or 's' - statistics");
        input = Console.ReadLine();
        if (input.ToLower() == "q")
        {
            Console.WriteLine("Quit");
            break;
        }
        else if (input.ToLower() == "s")
        {
            var statistics = meter.GetStatistics();
            Console.WriteLine($"Haus  a d d r e s s: {meter.Address}");
            Console.WriteLine($"M e t e r   t y p e: {meter.TypeMeter}");
            Console.WriteLine($"Meter Max Precision: {meter.MeterMaxPrecision}");
            Console.WriteLine($"Min  Meter  Reading: {statistics.Min}");
            Console.WriteLine($"Max  Meter  Reading: {statistics.Max}");
            Console.WriteLine($"Average Consumption in {statistics.Count} mounths: {Math.Round(statistics.Average, meter.MeterMaxPrecision)}");
        }
        else
        {
            try
            {
                meter.AddMeterReading(input);
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
                continue;
            }
        }
    }while(true);
}

void MeterReadingAdded(object sender, EventArgs args)
{
    Console.WriteLine("New value added successfully");
}

MeterSet SetMeterParameters()
{
    Console.WriteLine("Enter Haus Address:");
    var meterAddress = "NoAddress";
    var typeMeter = MeterType.Undef;
    meterAddress = Console.ReadLine();
    List<string> MeterTypesText = new List<string>
    {
        "q",
        "Q"
    };
    foreach (MeterBase.MeterType typeM in Enum.GetValues(typeof(MeterBase.MeterType)))
    {
        MeterTypesText.Add(typeM.ToString());
    }
    string inputText;
    do
    {
        Console.WriteLine("Enter Meter Type:");
        foreach (MeterBase.MeterType typeM in Enum.GetValues(typeof(MeterBase.MeterType)))
        {
            Console.WriteLine(typeM);
        }
        inputText = Console.ReadLine();
    } while (!MeterTypesText.Contains(inputText));

    typeMeter = (MeterBase.MeterType)Enum.Parse(typeof(MeterBase.MeterType), inputText);

    var meterSet = new MeterSet(meterAddress, typeMeter);
        
    return meterSet;
}

class MeterSet
{
    public MeterType TypeMeter { get; private set; }

    public string Address { get; private set; }
    
    public MeterSet(string address, MeterBase.MeterType typeMeter)
    {
        this.Address = address;
        this.TypeMeter = typeMeter;
    }
}
