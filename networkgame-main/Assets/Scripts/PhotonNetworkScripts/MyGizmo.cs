using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{

    public Color Colr = Color.yellow;
    public float radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = Colr;

        Gizmos.DrawSphere(transform.position, radius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
