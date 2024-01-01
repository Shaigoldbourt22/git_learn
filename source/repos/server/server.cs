using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

class TCPServer
{
    static async Task Main()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        var port = 8080;
        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine("Listening");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");
            _= HandleClient(client);
        }
    }


    static async Task HandleClient(TcpClient client) { 

        var recvMsg = "";
        NetworkStream stream = client.GetStream();
        var sendMsg = "got it";
        var writeBuffer = Encoding.ASCII.GetBytes(sendMsg);

        while (recvMsg != "Bye")
        {

            var readBuffer = new byte[1024];
            var bytesRead = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
            recvMsg = Encoding.ASCII.GetString(readBuffer, 0, bytesRead);
            Console.WriteLine("the client sent: " + recvMsg);
            if (recvMsg != "Bye")
            {
                await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
            }

        }

        sendMsg = "done";
        writeBuffer = Encoding.ASCII.GetBytes(sendMsg);
        await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);

        Console.WriteLine("closing the connection");
        client.Close();
        }
    }
