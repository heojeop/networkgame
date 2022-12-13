using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiCtrl : MonoBehaviour
{
    public enum AiState { idle , trace, stop, die };
    public AiState aiState = AiState.idle;
    private Transform aiTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 10.0f;
    public float stopDist = 2.0f;
    private bool isDie = false;

    private int hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        aiTr = GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        StartCoroutine(this.CheckAiState());
        StartCoroutine(this.AiAction());

    }

    IEnumerator CheckAiState()
    {
        while(isDie == false)
        {
            yield return new WaitForSeconds(0.2f);

            if(playerTr)
            {
                float dist = Vector3.Distance(playerTr.position, aiTr.position);

                if(aiState == AiState.die)
                {
                    ;
                }
                else if(dist <= stopDist)
                {
                    aiState = AiState.stop;
                    Debug.Log("CheckAiState(): stop!");
                }
                else if (dist <= traceDist)
                {
                    aiState = AiState.trace;
                    Debug.Log("CheckAiState(): trace!");
                }
                else
                {
                    aiState = AiState.idle;
                    Debug.Log("CheckAiState(): idle!");
                    playerTr = null;
                }



            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, traceDist);

                for(int i =0; i< colliders.Length; i++)
                {
                    if(colliders[i].tag == "Player")
                    {
                        playerTr = colliders[i].GetComponent<Transform>();
                        break;
                    }
                }
            }
        }
    }

    IEnumerator AiAction()
    {
        while(!isDie)
        {
            switch(aiState)
            {
                case AiState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);

                    break;
                case AiState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;

                    animator.SetBool("IsTrace", true);

                    break;
                case AiState.stop:
                    nvAgent.isStopped = true;

                    break;
            }
            yield return null;
        }
    }

    

    //private void OnTriggweEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Weapon")
    //    {
    //        Destroy(other.gameObject);

    //        animator.SetTrigger("IsHit");

    //        AiDie();

    //    }
    //}

    //void AiDie()
    //{
    //    StopAllCoroutines();
    //    isDie = true;

    //    monsterState = AiState.die;

    //    nvAgent.isStopped = true;
    //    animator.SetTrigger("IsDie");

    //    gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

    //    foreach(Collider coll in gameObject.GetComponentInChildren<SphereCollider>())
    //    {
    //        coll.enabled = false;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
