using CodeBattleBackend.Core;
using CodeBattleBackend.Types;
using TMPro;
using UnityEngine;

public class PanelControl : Creator
{
    private Vector2 FullOffset = new Vector2(300, -150);

    public override void Create(PlayerObject player)
    {
        GameObject TempPlayer = Instantiate(_PlayerPrefab, _Parent.transform);
        TempPlayer.transform.position = FullOffset;
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
        FullOffset.y -= 150f / 2f + _offset;
    }

    public override void Delete(string uuid)
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
