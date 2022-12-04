using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    private float h = 0f;
    private float v = 0f;
    private Transform tr;
    public float speed = 10.0f;
    public float rotSpeed = 100.0f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * speed);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        if(moveDir.magnitude > 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }
}
