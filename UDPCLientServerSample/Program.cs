using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UDPCLientServerSample
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var server = new UDPServer();
			server.Initialize();
			server.StartMessageLoop();
			Console.WriteLine("Hello World!");

			var client = new UDPClient();
			client.Initialize(IPAddress.Loopback, UDPServer.PORT);
			client.StartMessageLoop();
			await client.Send(Encoding.UTF8.GetBytes("Hello"));
			Console.WriteLine("Message sent!");
		}
	}
}
