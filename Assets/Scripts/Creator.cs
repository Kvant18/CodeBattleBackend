using System.Collections.Generic;
using CodeBattleBackend.Types;
using UnityEngine;

namespace CodeBattleBackend.Core
{
  public abstract class Creator : MonoBehaviour
  {
    protected GameObject _Parent;
    protected GameObject _PlayerPrefab;
    protected float _offset = 30f;
    public Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
    public abstract void Create(PlayerObject player);
    public abstract void Delete(string uuid);
  }
}
