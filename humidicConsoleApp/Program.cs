using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace humidicConsoleApp
{
    class Program
    {
        private const int Port = 12345;
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://humidityweb.azurewebsites.net");
            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            DateTime time = DateTime.Now;
            try
            {
                var humidity = new Humidity();
                var message = string.Empty;

                if (time.Minute = 00 || 15 || 30 || 45)
                {
                    message = AddHumidityLevel(humidity);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //User u = new User();

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
                    Thread.Sleep(60 * 1 * 1000);

                }
            }
        }


           private static string AddHumidityLevel(Humidity humidity)
        {
            var action = "api/Humidity/add"; // have to be sure about the controller's name 
            var request = client.PostAsJsonAsync(action, humidity);
            var response = request.Result.Content.ReadAsStringAsync();

            return response.Result;

        }

       
            }
        }
    


