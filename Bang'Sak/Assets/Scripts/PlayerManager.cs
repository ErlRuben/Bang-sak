using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Start()
    {
        if(PV.IsMine){
            CreateController();
        }
    }
    void CreateController(){
        Debug.Log("Instantiated Player Controller");
    }
}
