using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace humidicConsoleApp
{
    class Program
    {
        private const int Port = 12345;
        static void Main(string[] args)
        {
            User u = new User();
            DateTime time = DateTime.Now;

            if (u.UpdateInterval == 15) // this if we use the user update interval 
            {
                DoIt();
                Thread.Sleep(60 * 15 * 1000);
            }
            else if (time.Minute == 30) // this if we are going to use the time 
            {
                DoIt();
                Thread.Sleep(60 * 30 * 1000);
            }
            else if( time.Minute== 60)
            {
                DoIt();
                Thread.Sleep(60 * 60 * 1000);
            }
            
            
        }


        public static void DoIt()
        {
            using (UdpClient socket = new UdpClient(new IPEndPoint(IPAddress.Any, Port)))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(0, 0);
               
                while (true)
                {   
                    Console.WriteLine("Waiting for broadcast {0}", socket.Client.LocalEndPoint);
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                    string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    Console.WriteLine("Receives {0} bytes from {1} port {2} message {3}", datagramReceived.Length,
                                                        remoteEndPoint.Address, remoteEndPoint.Port, message);
                    //Parse(message);
                }
            }
        }
    }
}

