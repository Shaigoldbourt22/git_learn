using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

class TCPServer
{
    static void Main()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        var port = 8080;
        TcpListener listener = new TcpListener(ipAddress, port);
        while (true)
        {
            listener.Start();
            Console.WriteLine("Listening");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");

            var recvMsg = "";
            NetworkStream stream = client.GetStream();
            var sendMsg = "got it";
            var writeBuffer = Encoding.ASCII.GetBytes(sendMsg);

            while (recvMsg != "Bye")
            {

                var readBuffer = new byte[1024];
                var bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                recvMsg = Encoding.ASCII.GetString(readBuffer, 0, bytesRead);
                Console.WriteLine("the client sent: " + recvMsg);
                if (recvMsg != "Bye")
                {
                    stream.Write(writeBuffer, 0, writeBuffer.Length);
                }

            }

            sendMsg = "done";
            writeBuffer = Encoding.ASCII.GetBytes(sendMsg);
            stream.Write(writeBuffer, 0, writeBuffer.Length);

            Console.WriteLine("closing the connection");
            client.Close();
        }
    }
}