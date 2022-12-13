using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnAICharcter : MonoBehaviourPun
{

    public GameObject AICharacter;
    public GameObject SpawnPoint;
    public GameObject target;
    public GameObject spawn;
    int spawnCount = 0;

    Transform targetPoint;
    Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = target.GetComponent<Transform>();
        spawnPoint = spawn.GetComponent<Transform>();


        //StartCoroutine(this.CheckAiCharacterSpawn());


        //Transform[] SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
        //int idx = Random.Range(1, SpawnPoints.Length);

        //PhotonNetwork.Instantiate("Player", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);

        GameObject AICharacter = PhotonNetwork.Instantiate("AiChar", spawnPoint.position, spawnPoint.rotation);

        AICharacter.GetComponent<NewMoveAI>().SetTarget(targetPoint);
    }

    //IEnumerator CheckAiCharacterSpawn()
    //{
        
    //    for(; spawnCount <4;spawnCount++ )
    //    {
           
                
            

    //    }

    //    yield return null;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
