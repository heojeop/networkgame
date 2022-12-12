using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MoveAI : MonoBehaviour
{
    public enum AICharacterState {idle, trace, die };
    public AICharacterState AIState = AICharacterState.idle;

    private Transform AICharacterTr;
    public Rigidbody AICharacter;
    private NavMeshAgent nvAgent;
    private Animator animator;

    float randomMovingTime;
    public float speed = 10.0f;
    public bool isDie = false;
    public float idleDist = 2.0f;
    public float rotateSpeed = 10.0f;
    
    float rotH, rotV;

    // Start is called before the first frame update
    void Start()
    {
        AICharacterTr = GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        //AICharacter = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        {
            StartCoroutine(this.CheckAICharacterState());
        }

    }

    IEnumerator CheckAICharacterState()
    {
        while(isDie==false)
        {
            yield return new WaitForSeconds(0.05f);

            float dirX = Random.Range(-2f, 2f);
             float dirZ = Random.Range(-2f, 2f);
             bool dist = true;

            rotH = dirX;
            rotV = dirZ;


            if (dirX <2 && dirX> -2)
            {
                if (dirZ < 2 && dirZ > -2)
                {
                    dist = true;
                }
                else
                {
                    dist = false;
                }
            }
            else
            {
                dist = false;
            }
            

            if (AIState == AICharacterState.die)
            {
                ;
            }
            //dist 가 true면 가만히
            if (dist == true)
            {
                AIState = AICharacterState.idle;
                Debug.Log("CheckAICharacterState(): idle! " + dirX + dirZ);
            }
            else
            {
                AIState = AICharacterState.trace;
                Debug.Log("CheckAICharacterState(): trace! " + dirX + dirZ);
            }

            Vector3 rotDir = new Vector3(rotH, 0, rotV);

            if (!(rotH == 0 && rotV == 0))
            {
                AICharacter.position += rotDir * speed * Time.deltaTime;

                AICharacter.rotation = Quaternion.Lerp(AICharacter.rotation, Quaternion.LookRotation(rotDir), Time.deltaTime * rotateSpeed);
            }

        }
    }

    void Update()
    {

    }
}
