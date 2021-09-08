
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Network_Handler : MonoBehaviourPunCallbacks
{
    
    public static Network_Handler Instance;

    [SerializeField] GameObject lobbymenu;
    [SerializeField] GameObject loadingmenu;
    [SerializeField] GameObject leavelobbymenu;
    [SerializeField] GameObject joinlobbymenu;
    [SerializeField] GameObject mainmenu;
    [SerializeField] GameObject titlegame;
    [SerializeField] GameObject loadingmenuerror;
    [SerializeField] GameObject startGameButton;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errortext;
    [SerializeField] TMP_Text roomNametext;

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;

    [SerializeField] GameObject closefindlobby;

    void Awake() {
        Instance = this;
    }
    
    void Start(){
        Debug.Log ("Connecting to Master Server");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby(){
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }
    public void CreateRoom(string roomName){
        if(string.IsNullOrEmpty(roomNameInputField.text)){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        StartCoroutine(Countdown());
    }
    
    public override void OnJoinedRoom(){
        lobbymenu.SetActive(true);
        roomNametext.text = PhotonNetwork.CurrentRoom.Name;
        
        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent){
            Destroy(child.gameObject);
        }

        for(int i = 0; i < players.Length; i++){
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient){
        Debug.Log("First Owner leaved the lobby, so you are the next one!");
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message){
        errortext.text = "Lobby Creation Failed: " + message;
        loadingmenuerror.SetActive(true);
    }
    public void StartGame(){
        Debug.Log("Game Started");
        PhotonNetwork.LoadLevel(1);
    }
    public void LeaveRoom(){
        Debug.Log("Leaving Lobby");
        PhotonNetwork.LeaveRoom();
        StartCoroutine(LeaveCountdown());
    }
    public void JoinRoom(RoomInfo info){
        PhotonNetwork.JoinRoom(info.Name);
        StartCoroutine(JoinCountdown());
    }
    public override void OnLeftRoom(){
        Debug.Log("Left the Lobby");
        mainmenu.SetActive(true);
        titlegame.SetActive(true);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        foreach(Transform trans in roomListContent){
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++){
            if(roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer){
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    private IEnumerator Countdown() {
        loadingmenu.SetActive(true);
        yield return new WaitForSeconds(3);
        loadingmenu.SetActive(false); 
    }
    private IEnumerator LeaveCountdown() {
        leavelobbymenu.SetActive(true);
        yield return new WaitForSeconds(2);
        leavelobbymenu.SetActive(false); 
    }
    private IEnumerator JoinCountdown() {
        joinlobbymenu.SetActive(true);
        yield return new WaitForSeconds(2);
        closefindlobby.SetActive(false);
        joinlobbymenu.SetActive(false); 
    }

}

