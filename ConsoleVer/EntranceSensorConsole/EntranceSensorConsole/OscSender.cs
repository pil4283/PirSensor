using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OscJack;

namespace EntranceSensorConsole
{
	class OscSender
	{
		#region SenderMember

		OscClient client;
		string ipAddress;
		int port;
		string oscAddress;

		#endregion

		public OscSender(string ipAddress, int port, string oscAddress)
		{
			this.ipAddress = ipAddress;
			this.port = port;
			this.oscAddress = oscAddress;
		}

		public void Connect()
		{
			client = new OscClient(ipAddress, port);
		}

		public void Send(int data)
		{
			client.Send(oscAddress, data);
		}
	}
}
