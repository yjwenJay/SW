using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SW
{
    public class SocketHelper
    {

        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }


        // This method requests the home page content for the specified server.
        public static string SocketSendReceive(string server, int port, string content)
        {
            string page = "";

            try
            {
                Byte[] bytesSent = Encoding.GetEncoding("gb2312").GetBytes(content);
                Byte[] bytesReceived = new Byte[2048];

                // Create a socket connection with the specified server and port.
                Socket s = ConnectSocket(server, port);

                if (s == null)
                    return ("Connection failed");

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;


                // The following will block until te page is transmitted.
                do
                {
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    page = page + Encoding.GetEncoding("gb2312").GetString(bytesReceived, 0, bytes);
                }
                while (bytes > 0);
            }
            catch (Exception ex)
            { 
                
            }

            return page;
        }
    }
}
