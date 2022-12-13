using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCtrl : MonoBehaviour
{

    private float h = 0f;
    private float v = 0f;


    private PhotonView pv;
    private Transform tr;
    public float speed = 10.0f;
    public float rotSpeed = 100.0f;
    public GameObject Weapon;
    private Animator animator;

    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

    }

    void Update()
    {
        if (pv.IsMine)
        {
            Move();
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
            }
           
        }
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * speed);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        if (moveDir.magnitude > 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }


    private void AttackStart()
    {
        Weapon.SetActive(true);


    }

    private void AttackEnd()
    {
        Weapon.SetActive(false);
    }



    IEnumerator Attacking()
    {

        yield return new WaitForSeconds(5.0f);
    }


}
