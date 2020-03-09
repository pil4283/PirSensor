using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace EntranceSensorConsole
{
    
    public class SerialTransport : IDisposable
    {
        public SerialPort serial = new SerialPort();
        //int baudRate = 9600;
        //string portName = "COM3";

        public SerialTransport(int baudRate, string portName)
        {
            serial.BaudRate = baudRate;
            serial.PortName = portName;
            serial.Open();
        }
        ~SerialTransport()
        {
            Dispose();
        }

        public void Dispose()
        {
            serial.Dispose();
            serial.Close();
        }
    }
}
