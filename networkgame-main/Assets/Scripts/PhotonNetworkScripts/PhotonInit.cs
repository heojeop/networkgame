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

    public override void OnConnectedToMaster() // ���� ���� �� Callback
    {
        Debug.Log(" Connected On Master ");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // Lobby ���� �� Callback
    {
        Debug.Log(" Connect On Lobby ");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();  // Random Match Making
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // Room ���� ���� �� CallBack
    {
        Debug.Log($"JoinRandomFailed {returnCode}:{message}");

        RoomOptions roomopstion = new RoomOptions();
        roomopstion.MaxPlayers = 20;  // �ִ� ���� ���� �÷��̾� ��
        roomopstion.IsOpen = true; // �� ���� ����
        roomopstion.IsVisible = true; // �� �κ� ���� ���� ���� / ����� 

        PhotonNetwork.CreateRoom(" My Room ", roomopstion);        
    }

    public override void OnCreatedRoom() // �� ���� �Ϸ� ��
    {
        Debug.Log(" Created Room ");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom() // �� ���� ��
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
