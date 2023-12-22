using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CodeBattleBackend.Core;
using CodeBattleBackend.Messages;
using CodeBattleBackend.Types;
//using TMPro;
using UnityEngine;

namespace CodeBattleBackend
{
  public class Server
  {
    private int port;
    private IPAddress localAddrs;
    private string password;
    private Dictionary<string, NetworkStream> PlayersStream = new Dictionary<string, NetworkStream>();
    private PanelControl creator;

    public Server(ServerProps props)
    {
      this.port = props.Port;
      this.localAddrs = props.IPAddress;
      this.password = props.Password;
      this.creator = props.CreatorPlayer;
    }

    public void Run()
    {
      try
      {
        TcpListener server = new TcpListener(localAddrs, port);

        Debug.LogFormat("Server listen on: {0}:{1}", localAddrs, port);
        server.Start();
        Debug.Log(password);

        for (; ; )
        {
          Debug.Log("Waiting for a connection... ");

          // Perform a blocking call to accept JoinRequests.
          // You could also use server.AcceptSocket() here.
          using TcpClient client = server.AcceptTcpClient();
          Debug.Log("Connected!");

          byte[] bytes = new byte[1024];

          // Get a stream object for reading and writing
          NetworkStream stream = client.GetStream();
          // Loop to receive all the recivedmessage sent by the client.
          for (int i; (i = stream.Read(bytes, 0, bytes.Length)) != 0;)
          {
            // Translate recivedmessage bytes to a ASCII string.
            string RecivedMessage = Encoding.UTF8.GetString(bytes, 0, i);
            //Debug.LogFormat("Received: {0}", RecivedMessage);

            // Process the recivedmessage sent by the client.
            //recivedmessage = recivedmessage.ToUpper();

            //byte[] msg = Encoding.UTF8.GetBytes(RecivedMessage);

            // Send back a response.
            //stream.Write(msg, 0, msg.Length);

            Request msg = Request.Parse(RecivedMessage);

            switch (msg.status)
            {
              default:
                Error(stream, "Invalid type message or status");
                break;

              case StatusCode.JoinRequest:
                Join(RecivedMessage, stream);
                break;

              case StatusCode.Exit:
                Exit(stream, Request.Parse(RecivedMessage).PlayerUUID);
                break;
            }

            //Debug.LogFormat("Sent: {0}", recivedmessage);

            //Debug.LogFormat("Code: {0}, DisplayName: {1}, Message: {2}, Status: {3}", request.Code, request.DisplayName, request.message, request.status.ToString());

          }
        }
      }
      catch (SocketException e)
      {
        Debug.LogErrorFormat("SocketException: {0}", e);
      }
    }

    private void Join(string json, NetworkStream stream)
    {
      JoinRequest request = JoinRequest.Parse(json);
      if (request.Password != password)
      {
        Error(stream, "Invalid password");
        return;
      }

      PlayersStream.Add(request.PlayerUUID, stream);
      PlayerObject panel = new PlayerObject(request.DisplayName, request.status.ToString(), request.PlayerUUID);
      creator.Create(panel);
      //Create?.Invoke(panel);

      Response response = new();
      response.message = "Successfully connected and joined to game";
      response.status = StatusCode.Connected;
      byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      stream.Write(message, 0, message.Length);


      Debug.Log(PlayersStream[request.PlayerUUID].CanWrite);
    }

    public static void Error(NetworkStream stream, string ErrorMessage)
    {
      Response response = new();
      response.message = $"Error: {ErrorMessage}";
      response.status = StatusCode.Error;
      byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      stream.Write(message, 0, message.Length);
    }


    public void Exit(NetworkStream stream, string uuid = "null")
    {
      Response response = new();
      response.message = "Exit";
      response.status = StatusCode.Exit;
      byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      stream.Write(message, 0, message.Length);

      /*
      if (uuid == "null")
      {
        Delete?.Invoke(uuid);
      }
      */
    }
  }
}
