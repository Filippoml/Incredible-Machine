using System.IO.Ports;

class ArduinoInterface
{
    static SerialPort port = null;

    public static void Initialize()
    {
        System.Console.WriteLine("Initializing port");
        port = new SerialPort();

        // choose name depending on operation system:
        // Call SerialPort.GetPortNames() to get all ports on your system:
        port.PortName = "COM5"; // windows
        port.BaudRate = 9600; 
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
    }

    public static void SendString(string send)
    {
        if (port == null)
            Initialize();

        port.Write(send);
    }
}