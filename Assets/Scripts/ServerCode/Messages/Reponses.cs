using CodeBattleBackend.Types;
using Newtonsoft.Json;

namespace CodeBattleBackend.Messages
{
  public class Response
  {
    public string message;
    public StatusCode status;

    public string ToJSON()
    {
      return JsonConvert.SerializeObject(this);
    }
  }


  public class StartGame : Response
  {
    public int Time;
    public new string ToJSON()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
