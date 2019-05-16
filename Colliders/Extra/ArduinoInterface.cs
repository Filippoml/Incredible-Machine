using System;
using System.IO.Ports;
using System.Management;

class ArduinoInterface
{
    static SerialPort port = null;

    public static void Initialize()
    {
        string pornName = AutodetectArduinoPort();
        port = new SerialPort();

        // choose name depending on operation system:
        // Call SerialPort.GetPortNames() to get all ports on your system:
        port.PortName = "COM15"; // windows
        port.BaudRate = 9600; 

        port.Open();
    }

    public static void SendString(string send)
    {
        if (port == null)
            Initialize();

        port.Write(send);
    }

    private static string AutodetectArduinoPort()
    {
        ManagementScope connectionScope = new ManagementScope();
        SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

        try
        {
            foreach (ManagementObject item in searcher.Get())
            {
                string desc = item["Description"].ToString();
                string deviceId = item["DeviceID"].ToString();

                if (desc.Contains("Arduino"))
                {
                    return deviceId;
                }
            }
        }
        catch (ManagementException e)
        {
            /* Do Nothing */
        }

        return null;
    }
}