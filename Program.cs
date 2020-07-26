using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;

namespace helloworlddocker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starte weblistener");

            Task.Run(() =>
            {
                Console.WriteLine("starte weblistener gestartet");
                SimpleListenerExample();
            });

            Console.WriteLine("laufe weiter");

            bool always = true;
            while (always == true) {
                Console.WriteLine("Hello World, Windows (dauernd from k3s 5) !!!");

                ComplexPing("8.8.8.8");
                ComplexPing("google.ch");

                ComplexPing("192.168.2.1");

                ComplexPing("rancher.cluster");

                ComplexPing("jmaster1.mei.local");

                Thread.Sleep(60000);
            }
            

        }

        public static void SimpleListenerExample()
        {
            Console.WriteLine("drin in funktion");
            var web = new HttpListener();

            Console.WriteLine("HttpListener");
            web.Prefixes.Add("http://localhost:80");

            Console.WriteLine("Listening..");

            web.Start();

            //Console.WriteLine(web.GetContext());

            var context = web.GetContext();

            var response = context.Response;

            const string responseString = "<html><body>Hello world from k3s 2</body></html>";

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            Console.WriteLine(output);

            output.Close();

            web.Stop();

            Console.ReadKey();
        }


        public static void ComplexPing(string address)
        {
            try
            {

            
            Ping pingSender = new Ping();

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 10 seconds for a reply.
            int timeout = 10000;

            // Set options for transmission:
            // The data can go through 64 gateways or routers
            // before it is destroyed, and the data packet
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);

            // Send the request.
            PingReply reply = pingSender.Send(address, timeout, buffer, options);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                    
                }
            else
            {
                Console.WriteLine(reply.Status);
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.InnerException);
            }
            finally
            {
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}
