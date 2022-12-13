using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCtrl : MonoBehaviourPunCallbacks, IPunObservable 
{

    private float h = 0f;
    private float v = 0f;


    private PhotonView pv;
    private Transform tr;
    public float speed = 10.0f;
    public float rotSpeed = 100.0f;
    public GameObject Weapon;
    private Animator animator;


    private Vector3 currPos;
    private Quaternion currRot;
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        pv.ObservedComponents[0] = this;

        if (pv.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Camera.main.GetComponent<FollowCam>().targetTr = tr.Find("Cube").gameObject.transform;
            //Camera.main.GetComponent<FollowCam>().targetTr = tr;
        }

        
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
        else
        {
            if(tr.position != currPos)
            {
                animator.SetFloat("Speed", 1.0f);
            }
            else
            {
                animator.SetFloat("Speed", 0.0f);
            }
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
            tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
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


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            animator.SetTrigger("Death");
        }
    }



    IEnumerator Attacking()
    {

        yield return new WaitForSeconds(5.0f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
