using System.Collections.Generic;
using CodeBattleBackend.Types;
using TMPro;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public GameObject _Parent;
    public GameObject _PlayerPrefab;
    public float _offset = 30f;
    public static Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
    private static Vector2 FullPos = new Vector2(300f, -150f);

    public void Parse(PanelProps panelProps)
    {
        _Parent = panelProps.Parent;
        _PlayerPrefab = panelProps.PlayerPrefab;
        _offset = panelProps.offset;
    }


    public void Create(PlayerObject player)
    {
        GameObject TempPlayer = Instantiate(player.Prefab, player.Parent.transform);
        RectTransform rectTransform = TempPlayer.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = FullPos;
        //rectTransform.anchorMax.Set(0f, 1f);
        //TempPlayer.transform.position = FullOffset;
        TextMeshProUGUI[] Textes = TempPlayer.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var obj in Textes)
        {
            if (obj.gameObject.name == "DisplayName")
            {
                obj.text = player.PlayerName;
            }
            if (obj.gameObject.name == "Status")
            {
                obj.text = $"Status: {player.Status}";
            }
            if (obj.gameObject.name == "UUID")
            {
                obj.text = player.UUID;
            }
        }

        playerList.Add(player.UUID, TempPlayer);
        FullPos.y -= 150f + _offset;
    }

    public void Delete(string uuid)
    {
        playerList.TryGetValue(uuid, out GameObject temp);
        if (temp != null)
        {
            Destroy(temp);
            playerList.Remove(uuid);
        }
        else
        {
            Debug.LogErrorFormat("Player({0}) not found in list!", uuid);
        }
    }

    public PanelControl(PanelProps props)
    {
        _Parent = props.Parent;
        _PlayerPrefab = props.PlayerPrefab;
        _offset = props.offset;
    }
}
