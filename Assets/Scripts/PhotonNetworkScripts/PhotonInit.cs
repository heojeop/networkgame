using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonInit : MonoBehaviourPunCallbacks
{
    public GameObject SpawnPoint;
    private readonly string version = " 1.0 ";
    private string PlayerName = "";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = version;

        PhotonNetwork.NickName = PlayerName;

        Debug.Log(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() // 서버 접속 후 Callback
    {
        Debug.Log(" Connected On Master ");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // Lobby 접속 후 Callback
    {
        Debug.Log(" Connect On Lobby ");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();  // Random Match Making
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // Room 입장 실패 후 CallBack
    {
        Debug.Log($"JoinRandomFailed {returnCode}:{message}");

        RoomOptions roomopstion = new RoomOptions();
        roomopstion.MaxPlayers = 20;  // 최대 접속 가능 플레이어 수
        roomopstion.IsOpen = true; // 룸 오픈 여부
        roomopstion.IsVisible = true; // 룸 로비 노출 여부 공개 / 비공개 

        PhotonNetwork.CreateRoom(" My Room ", roomopstion);        
    }

    public override void OnCreatedRoom() // 룸 생성 완료 후
    {
        Debug.Log(" Created Room ");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom() // 룸 입장 후
    {
        Debug.Log($" Joined Room = {PhotonNetwork.InRoom}");
        Debug.Log($"Current Player = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var Player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{Player.Value.NickName},{Player.Value.ActorNumber}");
        }

        Transform[] SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, SpawnPoints.Length);

        PhotonNetwork.Instantiate("Player", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
