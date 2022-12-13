using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestAISpawn : MonoBehaviourPun
{
    private int hp = 100;
    private bool isDie = false;
    public GameObject spawn;
    public GameObject target;
    public GameObject mage;

    Transform spawnPoint;
    Transform targetPoint;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = spawn.GetComponent<Transform>();
        targetPoint = target.GetComponent<Transform>();

        Debug.Log("go!");
        //if (PhotonNetwork.IsMasterClient)
        //{
            Debug.Log("Spawn");
            StartCoroutine(this.CheckMonsterSpawn());
        //}
    }

    IEnumerator CheckMonsterSpawn()
    {
        Debug.Log("Spawn2");
        while (isDie == false)
        {
            yield return new WaitForSeconds(3.0f);

            if (gameObject.tag == "AI")
            {
                Debug.Log("Spawn3");

                GameObject aichar = PhotonNetwork.Instantiate("TestAIChar", spawnPoint.position, spawnPoint.rotation);
                if (PhotonNetwork.IsMasterClient)
                {
                    mage.GetComponent<TestAIChar>().SetTarget(targetPoint);
                }
            }
        }
    }

    //[PunRPC]
    //void OnDamage(int damage)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        hp -= damage;

    //        photonView.RPC("ApplyUpdateHealth", RpcTarget.Others, hp);

    //        photonView.RPC("OnDamage", RpcTarget.Others, damage);
    //    }

    //    if (hp <= 0)
    //    {
    //        HQDie();
    //    }
    //}

    //void HQDie()
    //{
    //    StopAllCoroutines();
    //    isDie = true;

    //    Destroy(gameObject, 1.0f);
    //}

    //[PunRPC]
    //public void ApplyUpdateHealth(int newhp)
    //{
    //    hp = newhp;
    //}

    //private void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.gameObject.tag == "BULLET")
    //    {
    //        Destroy(coll.gameObject);
    //        OnDamage((int)coll.gameObject.GetComponent<Bullet>().damage);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "BLUESTAFF" || other.gameObject.tag == "REDSTAFF")
    //    {
    //        OnDamage(10);
    //    }

    //}


    // Update is called once per frame
    void Update()
    {

    }
}