using System;
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
        port.PortName = "COM4"; // windows
        port.PortName = "/dev/tty.usbmodemHIDFG1";
        port.BaudRate = 9600; 
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();

        while (true)
        {
            string data_rx = port.ReadLine();
            Console.WriteLine(data_rx);
            
            if(data_rx.Equals("Distance = x cm"))
            {
                // signal is true
            }
        }
    }

    public static void SendString(string send)
    {
        if (port == null)
            Initialize();

        port.Write(send);
    }
}