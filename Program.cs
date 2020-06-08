using System;
using System.Threading;
using System.Net.NetworkInformation;
using System.Text;

namespace helloworlddocker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool always = true;
            while (always == true) {
                Console.WriteLine("Hello World, Windows (dauernd from k3s 4) !!!");

                ComplexPing("8.8.8.8");
                ComplexPing("google.ch");

                ComplexPing("192.168.2.1");

                ComplexPing("rancher.cluster");

                ComplexPing("jmaster1.mei.local");

                Thread.Sleep(20000);
            }
            

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
