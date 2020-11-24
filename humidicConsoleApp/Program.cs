using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace humidicConsoleApp
{
    class Program
    {
        private const int Port = 12345;

        // we have to create a reference of the HTTPClient
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        { 
            // here we define the client address 
            client.BaseAddress = new Uri("https://humidityweb.azurewebsites.net");
            //we define the headers
            var val = "application/json";

            // here we create the Media type so we make it media for application/json
            var media = new MediaTypeWithQualityHeaderValue(val);

            // here we clear all the previous headers the default ones
            client.DefaultRequestHeaders.Accept.Clear();

            // here we define the headers we need 
            client.DefaultRequestHeaders.Accept.Add(media);

            
            DateTime time = DateTime.Now;
            var currentMinute = time.Minute;


            if (currentMinute % 15 == 0)// send data every 15 minutes
            {
                try
                {
                    while (true)
                    {   // create a new object 
                        var humidity = new Humidity(time);

                        var message = string.Empty;

                        message = AddHumidityLevel(humidity);
                        
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                }
            
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

            var action = "api/Humidity/Post"; // have to be sure about the controller's name and the method
            // here we create a request 
            // we need to go to tools, nygetManager and add Microsoft Asp.net.WebApi.client 
            var request = client.PostAsJsonAsync(action, humidity);

            //now we need the response
            var response = request.Result.Content.ReadAsStringAsync();

            // we deed to return the respnse
            return response.Result;

        }

       
            }
        }
    


