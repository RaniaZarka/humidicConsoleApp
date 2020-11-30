using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace humidicConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private const int Port = 12345;

        static void Main(string[] args)
        {

            client.BaseAddress = new Uri("https://humidityweb.azurewebsites.net");

            //We need headers for this base address 
            var val = "application/json";

            // we need to create a new media type that is valide for application jason
            var media = new MediaTypeWithQualityHeaderValue(val);

            // we need to clear all the default headers
            client.DefaultRequestHeaders.Accept.Clear();

            // we added the new headers that we defined  
            client.DefaultRequestHeaders.Accept.Add(media);

            DateTime time = DateTime.Now;
            try
            {
                var humidity = new Humidity(time);
                int currentMinute = time.Minute;


                if (currentMinute % 15 == 0)
                {
                    AddHumidityLevel(humidity);
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


                //Thread.Sleep(2000);
                while (true)
                {    //if (datetime.now().minute == (0 or 15 or 30 or 45))

                    Console.WriteLine("Waiting for broadcast {0}", socket.Client.LocalEndPoint);
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                    string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    Console.WriteLine("Receives {0} bytes from {1} port {2} message {3}", datagramReceived.Length,
                                        remoteEndPoint.Address, remoteEndPoint.Port, message);
                    //Parse(message);
                    //Thread.Sleep(60 * 15 * 1000);
                }
            }

        }

        private static string AddHumidityLevel(Humidity humidity)
        {
            var action = "api/Humidity/Post";
            var request = client.PostAsJsonAsync(action, humidity);

            var response = request.Result.Content.ReadAsStringAsync();
            return response.Result;


        }
    }
}

