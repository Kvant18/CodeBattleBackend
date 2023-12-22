using System.Threading;
using CodeBattleBackend;
using UnityEngine;

public class CreateServer : MonoBehaviour
{
  [SerializeField] private ServerProps Props;

  [SerializeField] private Server Server;

  [SerializeField] private GameObject CreateServerPanel;

  [SerializeField] private PanelProps PanelProps;

  private GameObject ControlPanel;

  //[SerializeField] private InputField InputMessage;
  //private Creator creator;

  private void Awake()
  {
    Canvas parent = FindFirstObjectByType<Canvas>();
    GameObject temp = Instantiate(PanelProps.Parent, parent.gameObject.transform);
    PanelControl tempPanel = temp.AddComponent<PanelControl>();
    PanelProps tempProps = new();
    tempProps.offset = PanelProps.offset;
    tempProps.PlayerPrefab = PanelProps.PlayerPrefab;
    ControlPanel = tempProps.Parent = temp;
    Props.CreatorPlayer = tempPanel;
    ControlPanel.SetActive(false);
    //Debug.Log(Props.CreatorPlayer);
  }


  public void StartServer()
  {

    Server = new Server(Props);
    Thread th = new Thread(Server.Run);
    //server.Start();

    th.Start();

    CreateServerPanel.SetActive(false);

    ControlPanel.SetActive(true);

    /*
    string message = @"
    {
      'User': {
        'Status': 0,
        'Message':'Connecting',
        'Code':'null',
        'DisplayName':'SS1GG_zxc',
        'Password':'da39a3ee5e6b4b0d3255bfef95601890afd80709',
        'UUID':'a0a8a81e-f04b-4c05-afe8-ff6cbb325efa'
      }
    }
    ";
    */

    //JoinRequest JoinRequest = JoinRequest.Parse(message);
    //Debug.LogFormat("Code: {0}, DisplayName: {1}, Message: {2}, Status: {3}", JoinRequest.Code, JoinRequest.DisplayName, JoinRequest.message, JoinRequest.status.ToString());
  }

}
