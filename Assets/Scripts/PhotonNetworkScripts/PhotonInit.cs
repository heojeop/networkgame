using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public InputField Bots;
    public GameObject LobbyUI;
    public GameObject MainUI;
    public Text Status;
    public GameObject MasterUI;
    public InputField PlayerName;
    public GameObject SpawnPoint;

    private readonly string version = " 1.0 ";

    void Awake()
    {

        MainUI.SetActive(true);
        MasterUI.SetActive(false);
        LobbyUI.SetActive(false);

        Screen.SetResolution(1600, 900, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        Debug.Log(PhotonNetwork.SendRate);
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster() // 서버 접속 후 Callback
    {

        Status.text = "Online";
        Debug.Log(" Connected On Master ");


    }
    public void JoinGameBtn()
    {
        PhotonNetwork.NickName = PlayerName.text;
        PhotonNetwork.JoinLobby();
        if (string.IsNullOrEmpty(PlayerName.text))
        {
            PlayerName.text = $"USER_{Random.Range(0, 100):00}";
        }
    }

        

    public override void OnJoinedLobby() // Lobby 접속 후 Callback
    {

        Debug.Log(" Connect On Lobby ");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinOrCreateRoom(" Game Room ", new RoomOptions { MaxPlayers = 4 }, null);

    }


    public override void OnJoinRandomFailed(short returnCode, string message) // Room 입장 실패 후 CallBack
    {

        Debug.Log(" Room Failed ");
    }

    public override void OnCreatedRoom() // 룸 생성 완료 후
    {
        Debug.Log(" Created Room ");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom() // 룸 입장 후
    {
        Debug.Log($" Joined Room = {PhotonNetwork.InRoom}");
        Debug.Log($" Current Player = {PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($" Player Name = {PhotonNetwork.CurrentRoom.Players}");
        MainUI.SetActive(false);
        MasterUI.SetActive(true);
    }
    public void GameStartBtn()
    {

        MasterUI.SetActive(false);
        LobbyUI.SetActive(false);

        BotSpawn();
        foreach (var Player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{Player.Value.NickName},{Player.Value.ActorNumber}");
        }

        Transform[] SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, SpawnPoints.Length);

        PhotonNetwork.Instantiate("Player", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);
    }

    void BotSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            Transform[] SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
            int idx = Random.Range(1, SpawnPoints.Length);

            PhotonNetwork.Instantiate("Enemy", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);
        }


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
