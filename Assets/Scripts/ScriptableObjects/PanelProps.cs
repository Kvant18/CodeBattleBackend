using UnityEngine;

[CreateAssetMenu(fileName = "PanelProps", menuName = "CodeBattleBackend/PanelProps", order = 0)]
public class PanelProps : ScriptableObject
{
    public GameObject Parent;
    public GameObject PlayerPrefab;
    public float offset = 30f;
}
