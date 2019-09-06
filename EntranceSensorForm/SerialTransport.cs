using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace EntranceSensorForm
{
    
    public class SerialTransport : IDisposable
    {
        public SerialPort serial = new SerialPort();
        int baudRate = 9600;
        string portName = "COM3";

        public SerialTransport()
        {
            try
            {
                serial.BaudRate = baudRate;
                serial.PortName = portName;
                serial.Open();
            }
            catch(Exception e)
            {
                
            }
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
