using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveAI : MonoBehaviour
{


    public Rigidbody AiCharacter;
    float randomMovingTime;


    private PhtonView pv;
    private Transform tr;
    public float speed = 10.0f;
    public float rotSpeed = 100.0f;
    private Animation animator;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<animator>();
        pv = GetComponent<PhotonView>();

        StartCoroutine(MoveObject());



    }

    IEnumerator MoveObject()
    {
        AiCharacter = GetComponent<Rigidbody>();

        while (true)
        {
            float dirX = Random.Range(-6f, 6f);
            float dirZ = Random.Range(-6f, 6f);

            Vector3 moveDir = (Vector3.forward * dirX) + (Vector.rightt * dirZ);

            tr.Translate(moveDir.normailized * randomMovingTime.deltaTime * speed);
            //tr.Rotate(Vector3.up  * Time.deltaTime * rotSpeed * )


            randomMovingTime = Random.Range(0.1f, 1f);

            yield return new WaitForSeconds(randomMovingTime);
            AiCharacter.velocity = new Vector3(dirX, 0, dirZ);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
