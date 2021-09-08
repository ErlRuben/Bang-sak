using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text ntext;
    Player player;
    public void SetUp(Player _player){
        player = _player;
        ntext.text = _player.NickName;
    }

    // Update is called once per frame
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer){
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom(){
        Destroy(gameObject);
    }
}
