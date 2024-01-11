using System.Linq;
using CodeBattleBackend.Types;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using CodeBattleBackend.Connections.Messages;
using System.Text;
using System.Collections.Generic;

namespace CodeBattleBackend.Connections
{
  public class Controller : WebSocketBehavior
  {
    public static Dictionary<string, string> ConnectionsList = new();
    public static GameObject creatorObject;
    public static PanelControl creator;

    // protected override void OnOpen()
    // {
    //   Debug.LogFormat("New Clinet connected: {0}", Sessions.ActiveIDs.Last());

    // }

    protected override void OnMessage(MessageEventArgs e)
    {
      //Debug.LogFormat("New message is contain: {0}", e.Data);
      //string messgae = Encoding.UTF8.GetString(e.RawData);
      Request msg = Request.Parse(e.Data);

      if (ConnectionsList.ContainsKey(msg.PlayerUUID))
      {
        Error("This UUID is already registered");
        return;
      }


      switch (msg.status)
      {
        default:
          Error("Invalid type message or status");
          break;

        case StatusCode.JoinRequest:
          Join(e.Data);
          break;

        case StatusCode.Exit:
          Exit(Request.Parse(e.Data).PlayerUUID);
          break;
      }

    }

    private void Join(string json)
    {
      JoinRequest request = JoinRequest.Parse(json);

      if (request.Password != Server.password)
      {
        Error("Invalid password");
        return;
      }

      PlayerObject panel = new PlayerObject(request.DisplayName, request.status.ToString(), request.PlayerUUID, creatorObject);
      //creator.Create(panel);
      //Create?.Invoke(panel);
      CreateServer.mainThreadQueuedCallbacks += creator.Create;
      CreateServer.param = panel;

      Response response = new();
      response.message = "Successfully connected and joined to game";
      response.status = StatusCode.Connected;
      //byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      Send(response.ToJSON());

      ConnectionsList.Add(request.PlayerUUID, Sessions.ActiveIDs.Last());

      Debug.Log(ConnectionsList.GetValueOrDefault(request.PlayerUUID));
    }

    private void Error(string ErrorMessage)
    {
      Response response = new();
      response.message = $"Error: {ErrorMessage}";
      response.status = StatusCode.Error;
      //byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      Send(response.ToJSON());
    }


    private void Exit(string uuid = null)
    {
      Response response = new();
      response.message = "Exit";
      response.status = StatusCode.Exit;
      //byte[] message = Encoding.UTF8.GetBytes(response.ToJSON());
      Send(response.ToJSON());

      /*
      if (uuid == "null")
      {
        Delete?.Invoke(uuid);
      }
      */
    }
  }
}
