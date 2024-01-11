using CodeBattleBackend.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using WebSocketSharp.Server;

namespace CodeBattleBackend.Connections.Messages
{
  public class Request
  {
    public StatusCode status;
    public string message;
    public string PlayerUUID;

    public static Request Parse(string json)
    {
      JObject jobject = JObject.Parse(json);
      JToken token = jobject["User"];
      Request res = new();
      res.status = (StatusCode)int.Parse((string)token["Status"]);
      res.message = (string)token["Message"];
      res.PlayerUUID = (string)token["UUID"];
      return res;
    }

    public string ToJSON()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class JoinRequest : Request
  {
    public string DisplayName;
    public string Password;
    public string Code;

    public static new JoinRequest Parse(string json)
    {
      JObject jobject = JObject.Parse(json);
      JToken token = jobject["User"];
      JoinRequest res = new();
      res.status = (StatusCode)int.Parse((string)token["Status"]);
      res.message = (string)token["Message"];
      res.Code = (string)token["Code"];
      res.DisplayName = (string)token["DisplayName"];
      res.Password = (string)token["Password"];
      res.PlayerUUID = (string)token["UUID"];
      return res;
    }

    public new string ToJSON()
    {
      /*
      return string.Format(@"
        {
          'User': {
            'Status':{0},
            'Message':{1},
            'Code':{2},
            'DisplayName':{3},
            'Password':{4},
            'UUID':{5}
          }
        }
      ", (int)status, message, Code, DisplayName, Password, PlayerUUID);
      */
      return JsonConvert.SerializeObject(this);
    }

  }


}
