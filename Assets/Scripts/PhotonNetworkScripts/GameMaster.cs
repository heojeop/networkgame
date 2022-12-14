using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviourPunCallbacks
{
    private List<Transform> SpawnPointList = new List<Transform>();


    public GameObject SpawnPoint;

    PhotonView pv;
    public InputField Bots;
    public GameObject MainMenu;
    public GameObject LobbyUI;
    public GameObject MasterUI;

    // Start is called before the first frame update
    void Start()
    {

            Transform[] SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
            foreach (Transform pos in SpawnPoints)
            {
                SpawnPointList.Add(pos);
            }

    }

    // Update is called once per frame
    void Update()
    {
       
    }



    public void GameStartBtn()
    {

        photonView.RPC("SpawnPlayer", RpcTarget.All);
        photonView.RPC("BotSpawn", RpcTarget.All);
    }


    [PunRPC]
    void BotSpawn()
    {

        int a = int.Parse(Bots.text);
        if (a > 30)
        {
            Bots.text = "25";
        }
        for (int i = 0; i < a; i++)
        {
            int idx = Random.Range(1, SpawnPointList.Count);
            PhotonNetwork.Instantiate("Enemy", SpawnPointList[idx].position, SpawnPointList[idx].rotation, 0);
            SpawnPointList.RemoveAt(idx);
        }
    }
    [PunRPC]
    void SpawnPlayer()
    {
        LobbyUI.SetActive(false);
        MasterUI.SetActive(false);
        MainMenu.SetActive(false);
        foreach (var Player in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log($"{Player.Value.NickName},{Player.Value.ActorNumber}");
            }
            int idx = Random.Range(1, SpawnPointList.Count);
            PhotonNetwork.Instantiate("Player", SpawnPointList[idx].position, SpawnPointList[idx].rotation, 0);
    }

    void CheckPlayer()
    {

    }



}
