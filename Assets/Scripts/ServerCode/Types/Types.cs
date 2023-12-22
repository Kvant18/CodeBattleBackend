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

    public PlayerObject(string name, string status, string uuid)
    {
      PlayerName = name;
      Status = status;
      UUID = uuid;
    }
  }
}
