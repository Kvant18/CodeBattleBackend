using UnityEngine;

namespace CodeBattleBackend.Types
{
  public enum StatusCode
  {
    JoinRequest,
    Connected,
    StartGame,
    TimeOut,
    TextMessage,
    Result,
    Exit,
    Error,
  }

  public class PlayerObject
  {
    public string PlayerName;
    public string Status;
    public string UUID;
    public GameObject Prefab;
    public GameObject Parent;

    public PlayerObject(string name, string status, string uuid, GameObject paernt)
    {
      PlayerName = name;
      Status = status;
      UUID = uuid;
      Parent = paernt;
    }
  }
}
