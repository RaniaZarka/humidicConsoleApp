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

            DateTime time;
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

                    time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    try
                    {
                        var humidity = new Humidity(time);
                        humidity.Level = Convert.ToInt32(message);
                        //humidity.Level = 85;
                        int currentMinute = time.Minute;
                        Console.WriteLine(time.Hour);


                        if (currentMinute % 1 == 0)
                        {
                            Console.WriteLine(AddHumidityLevel(humidity));
                            Thread.Sleep(60 * 1 * 1000);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    //Parse(message);
                    //
                }
            }

        }

        private static string AddHumidityLevel(Humidity humidity)
        {
            var action = "api/Humidity/";
            var request = client.PostAsJsonAsync(action, humidity);

            var response = request.Result.Content.ReadAsStringAsync();
            return response.Result;


        }
    }
}

