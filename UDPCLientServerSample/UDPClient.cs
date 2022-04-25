using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPCLientServerSample
{
    public class UDPClient
    {
        private Socket _socket;
        private EndPoint _ep;

        private byte[] _buffer_recv;
        private ArraySegment<byte> _buffer_recv_segment;

        public void Initialize(IPAddress address, int port)
        {
            _buffer_recv = new byte[4096];
            _buffer_recv_segment = new(_buffer_recv);

            _ep = new IPEndPoint(address, port);

            _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
        }

        public void StartMessageLoop()
        {
            _ = Task.Run(async () =>
            {
                SocketReceiveMessageFromResult res;
                while (true)
                {
                    res = await _socket.ReceiveMessageFromAsync(_buffer_recv_segment, SocketFlags.None, _ep);
                }
            });
        }

        public async Task Send(byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            await _socket.SendToAsync(s, SocketFlags.None, _ep);
        }
    }
}
