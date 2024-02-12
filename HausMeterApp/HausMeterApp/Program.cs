using HausMeterApp;
using static HausMeterApp.MeterBase;

Console.WriteLine("App: HausMeter statistics ");
Console.WriteLine("==========================");
MeterSet meterSet = SetMeterParameters();

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
            AddMeterReading(true, meterSet.Address, meterSet.TypeMeter);
            break;
        case "2":
            AddMeterReading(false, meterSet.Address, meterSet.TypeMeter); 
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

void AddMeterReading(bool isInFile, string address, MeterTypes.MeterType typeMeter)
{
    IMeter meter = (isInFile) ? new MeterInFile(address, typeMeter ): new MeterInMemory(address,typeMeter);
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
            if (statistics.Count > 0 )
            {
                Console.WriteLine($"Min  Meter  Reading: {statistics.Min}");
                Console.WriteLine($"Max  Meter  Reading: {statistics.Max}");
                Console.WriteLine($"Average Consumption in {statistics.Count} mounths: {Math.Round(statistics.Average, meter.MeterMaxPrecision)}");
            }
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
    List<string> MeterTypesText = new List<string>
    {
            "q",
            "Q"
    };
    foreach (MeterTypes.MeterType typeM in Enum.GetValues(typeof(MeterTypes.MeterType)))
    {
        MeterTypesText.Add(typeM.ToString());
    }
    
    var meterAddress = "NoAddress";
    var typeMeter = MeterTypes.MeterType.Undef;
    bool containsLettersOrDigits = false;
    var meterSet = new MeterSet(meterAddress, typeMeter);
    while (meterSet.Address == "NoAddress" || meterSet.TypeMeter == MeterTypes.MeterType.Undef)
    {
        do
        {
            Console.WriteLine("Enter Haus Address:");
            meterAddress = Console.ReadLine();
            containsLettersOrDigits = false;
            if (meterAddress != null)
            {
                foreach (char c in meterAddress)
                {
                    if (Char.IsLetterOrDigit(c))
                    {
                        containsLettersOrDigits = true;
                        break;
                    }
                }
            }
        } while (!containsLettersOrDigits);
        meterSet.Address = meterAddress;
        string inputText;
        do
        {
            Console.WriteLine("Enter Meter Type:");
            foreach (MeterTypes.MeterType typeM in Enum.GetValues(typeof(MeterTypes.MeterType)))
            {
                Console.WriteLine(typeM);
            }
            inputText = Console.ReadLine();
        } while (!MeterTypesText.Contains(inputText));
        meterSet.TypeMeter = (MeterTypes.MeterType)Enum.Parse(typeof(MeterTypes.MeterType), inputText);
    }    
        
    return meterSet;
}

class MeterSet
{
    public MeterTypes.MeterType TypeMeter { get; set; }

    public string Address { get; set; }
    
    public MeterSet(string address, MeterTypes.MeterType typeMeter)
    {
        this.Address = address;
        this.TypeMeter = typeMeter;
    }
}
