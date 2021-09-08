using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    public RoomInfo info;
    public void SetUp(RoomInfo _info){
        info = _info;
        text.text = _info.Name;
    }
    public void OnClick(){
        Network_Handler.Instance.JoinRoom(info);
    }
}
