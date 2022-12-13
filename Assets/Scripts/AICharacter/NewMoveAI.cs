using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class NewMoveAI : MonoBehaviourPun
{

    public enum AICharacterState { idle, trace, die };
    public AICharacterState AIState = AICharacterState.idle;
    private Transform aiCharTr;
    private Transform targetTr;
    private Transform finalTargetTr;
    private NavMeshAgent nvAgent;
    private Animator animator;


    public float traceDist = 10.0f;
    public float stopDist = 2.0f;
    public bool isDie = false;

    public float moveSpeed = 10f;





    // Start is called before the first frame update
    void Start()
    {
        aiCharTr = GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        {
            StartCoroutine(this.CheckAiCharState());
            StartCoroutine(this.AiCharAction());
        }
    }


    IEnumerator CheckAiCharState()
    {
        yield return new WaitForSeconds(0.2f);


        if (targetTr)
        {
            float dist = Vector3.Distance(targetTr.position, aiCharTr.position);

            if (AIState == AICharacterState.die)
            {
                ;
            }

            if (dist <= stopDist)
            {
                AIState = AICharacterState.idle;
                Debug.Log("CheckAiState(): idle");

                targetTr = null;
            }
            else
            {
                AIState = AICharacterState.trace;
                Debug.Log("CheckAiState(): trace");
            }

            SetTargeyPoint();
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, traceDist);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Target")
                {
                    targetTr = colliders[i].GetComponent<Transform>();
                    Debug.Log("Target!");
                    break;
                }
            }
        }
    }

    IEnumerator AiCharAction()
    {
        while (!isDie)
        {
            switch (AIState)
            {
                case AICharacterState.idle:
                    //nvAgent.isStopped = true;
                    nvAgent.destination = finalTargetTr.position;
                    nvAgent.isStopped = false;

                    Debug.Log("IdleAni");
                    //animator.SetBool("IsTrace", false);

                    break;
                case AICharacterState.trace:
                    nvAgent.destination = targetTr.position;
                    nvAgent.isStopped = false;


                    Debug.Log("TraceAni");
                    //animator.SetBool("IsTrace", true);

                    break;
            }


            yield return null;
        }
    }




    // Update is called once per frame
    void Update()
    {

    }

    void SetTargeyPoint()
    {
        if (!isDie)
        {
            float dirX = Random.Range(-5f, 5f);
            float dirZ = Random.Range(-5f, 5f);

            Vector3 targetPos = new Vector3(dirX, 0, dirZ);

            targetTr.position += targetPos * Time.deltaTime;
        }
    }

    public void SetTarget(Transform tr)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            finalTargetTr = tr;
        }
    }
}
