using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private float time;
    private float rotateTime;
    private float stopTime;
    private float v = 0.0f;
    private float h = 0.0f;

    public float speed = 5.0f;
    public float rotSpeed = 100.0f;
    public float turnSpeed = 100.0f;
    private float m_currentV = 0.0f;
    private float m_currentH = 0.0f;
    private readonly float m_interpolation = 10.0f;
    private Animator Enemy_animator;

    private int state;
    void Start()
    {
        Enemy_animator = GetComponent<Animator>();

    }

    void SetRandom()
    {
        time = UnityEngine.Random.Range(4.0f, 7.0f);
        rotateTime = UnityEngine.Random.Range(0.0f, 2.0f);
        stopTime = UnityEngine.Random.Range(1.0f, 3.0f);
        v = UnityEngine.Random.Range(1.0f, 2.0f);
    }

    void Update()
    {
        /*
        state = 0 -> 이동
        state = 1 -> 회전
        state = 2 -> 정지
        */

       

        switch (state)
        {
            case 0:
                if (time > 0)
                {
                    if (v < 0)
                    {
                        v *= 0.9f;
                    }

                    m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);

<<<<<<< HEAD
                    this.transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
=======
                    this.transform.Translate(Vector3.forward * v * Time.deltaTime);
>>>>>>> 6e24936345188136d47975009b2c7fb604e261b3
                    this.transform.Rotate(Vector3.up * v * Time.deltaTime);
                    Enemy_animator.SetBool("IsTrace", true); //애니메이션 갱신
                    time -= Time.deltaTime;
                }
                else
                {
                    SetRandom();
                    state = 2;
                    Enemy_animator.SetBool("IsTrace", false);
                }
                break;
            case 1:
                if (rotateTime > 0)
                {
                    this.transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    rotateTime -= Time.deltaTime;
                }
                else
                {
                    SetRandom();
                    state = 0;
                }
                break;
            case 2:
                if (stopTime > 0)
                    stopTime -= Time.deltaTime;
                else
                {
                    SetRandom();
                    state = UnityEngine.Random.Range(0, 2);
                }
                break;
        }
    }
}
