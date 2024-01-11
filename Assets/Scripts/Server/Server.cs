//using System.Collections.Generic;
using System.Net;
using WebSocketSharp;
using CodeBattleBackend.Connections;
using WebSocketSharp.Server;
//using TMPro;
using UnityEngine;
using System;

namespace CodeBattleBackend
{
  public class Server
  {
    public static string password;
    private int port;
    private IPAddress localAddrs;
    //private Dictionary<string, NetworkStream> PlayersStream = new Dictionary<string, NetworkStream>();

    public Server(ServerProps props)
    {
      this.port = props.Port;
      this.localAddrs = props.IPAddress;
      Controller.creator = props.CreatorPlayer.GetComponent<PanelControl>();
      Controller.creatorObject = props.CreatorPlayer;
      password = props.Password;
    }

    public void Run()
    {
      try
      {
        WebSocketServer server = new WebSocketServer(localAddrs, port);

        server.AddWebSocketService<Controller>("/");

        server.Start();

        Debug.LogFormat("Server listen on: {0}:{1}", server.Address, server.Port);


        Debug.Log(password);
        Debug.Log("Waiting for a connection... ");

        /*
        Request msg = Request.Parse(RecivedMessage);
        Debug.Log("da");
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
        */
      }
      catch (Exception e)
      {
        Debug.LogErrorFormat("Exception: {0}", e);
      }
    }


  }
}
