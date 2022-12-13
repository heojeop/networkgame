using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class TestAIChar : MonoBehaviourPun
{
    public enum MonsterState { idle, trace, die };
    public MonsterState monsterState = MonsterState.idle;
    private Transform monsterTr;
    private Transform playerTr;
    private Transform finalTargetTr;
    private NavMeshAgent nvAgent;
    private Animator animator;


    public float idleDist = 2.0f;
    public float traceDist = 10.0f;
    public bool isDie = false;

    private int hp = 100;



    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        {
            StartCoroutine(this.CheckMonsterState());
            StartCoroutine(this.MonsterAction());
        }
    }
    IEnumerator CheckMonsterState()
    {
        while (isDie == false)
        {
            yield return new WaitForSeconds(0.2f);

            if (playerTr)
            {
                //playertr 위치랑 monsterTr거리
                float dist = Vector3.Distance(playerTr.position, monsterTr.position);

                if (monsterState == MonsterState.die)
                {
                    ;
                }
                //그 거리가 2.0보다 낮으면 -> 공격사거리가 되면
                
                // 인식범위 내 라면 -> 공격범위와 인식범위 사이
                if (dist <= idleDist)
                {
                    monsterState = MonsterState.idle;
                    Debug.Log("CheckMonsterState(): idle!");
                    playerTr = null;
                }
                else
                {
                    monsterState = MonsterState.trace;
                    Debug.Log("CheckMonsterState(): trace!");
                    
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, traceDist);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag == "Target")
                    {
                        playerTr = colliders[i].GetComponent<Transform>();
                        break;
                    }
                    
                }
            }
        }
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:

                    nvAgent.destination = finalTargetTr.position;
                    nvAgent.isStopped = true;
                    
                    animator.SetBool("IsTrace", false);

                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    
                    animator.SetBool("IsTrace", true);
                    break;

                
            }
            yield return null;
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
    //        MonsterDie();
    //    }
    //    else
    //    {
    //        animator.SetTrigger("IsHit");
    //    }
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

    //private void OnTriggweEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "REDSTAFF")
    //    {
    //        Destroy(other.gameObject);
    //        OnDamage(10);
    //    }
    //}

    //void MonsterDie()
    //{
    //    StopAllCoroutines();
    //    isDie = true;

    //    monsterState = MonsterState.die;
    //    nvAgent.isStopped = true;
    //    animator.SetTrigger("IsDie");

    //    gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

    //    foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
    //    {
    //        coll.enabled = false;
    //    }

    //    Destroy(gameObject, 2.0f);

    //}

    public void SetTarget(Transform tr)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            finalTargetTr = tr;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}