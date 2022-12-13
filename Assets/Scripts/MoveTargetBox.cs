using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetBox : MonoBehaviour
{

    public Rigidbody AiCharacter;
    float randomMovingTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObject());


    }

    IEnumerator MoveObject()
    {
        AiCharacter = GetComponent<Rigidbody>();

        while (true)
        {
            float dirX = Random.Range(-5f, 5f);
            float dirZ = Random.Range(-5f,5f);

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