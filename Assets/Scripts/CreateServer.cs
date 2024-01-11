using System.Threading;
using CodeBattleBackend;
using CodeBattleBackend.Types;
using UnityEngine;

public class CreateServer : MonoBehaviour
{
  [SerializeField] private ServerProps Props;

  [SerializeField] private Server Server;

  [SerializeField] private GameObject CreateServerPanel;

  [SerializeField] private PanelProps PanelProps;

  [SerializeField] private GameObject ControlPanel;

  public delegate void Creating(PlayerObject player);
  public static event Creating mainThreadQueuedCallbacks;
  public static PlayerObject param;
  private event Creating eventsClone;

  private void Awake()
  {
    PanelControl tempPanel = ControlPanel.AddComponent<PanelControl>();
    tempPanel.Parse(PanelProps);
    Props.CreatorPlayer = ControlPanel;
    ControlPanel.SetActive(false);
    //GameObject test = new GameObject("Test");
    //test.AddComponent<RectTransform>();
    //test.AddComponent<Image>().color = Color.gray;

  }

  private void FixedUpdate()
  {
    if (mainThreadQueuedCallbacks != null && param != null)
    {
      eventsClone = mainThreadQueuedCallbacks;
      mainThreadQueuedCallbacks = null;
      param.Prefab = PanelProps.PlayerPrefab;
      eventsClone.Invoke(param);
      eventsClone = null;
      param = null;
    }
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
