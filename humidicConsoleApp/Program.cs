using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace humidicConsoleApp
{
    class Program
    {
        private const int Port = 12345;
        static void Main(string[] args)
        {
           

            using (UdpClient socket = new UdpClient(new IPEndPoint(IPAddress.Any, Port)))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(0, 0);
                

                    //Thread.Sleep(2000);
                while (true)
                {    //if (datetime.now().minute == (0 or 15 or 30 or 45))
                    
                    Console.WriteLine("Waiting for broadcast {0}", socket.Client.LocalEndPoint);
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                    string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    Console.WriteLine("Receives {0} bytes from {1} port {2} message {3}", datagramReceived.Length,
                                        remoteEndPoint.Address, remoteEndPoint.Port, message);
                    //Parse(message);
                    Thread.Sleep(60 * 15 * 1000);
                }
            }

        }
    }
}
    

