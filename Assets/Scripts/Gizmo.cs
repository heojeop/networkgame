using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public enum Type { NORMAL, WAYPOINT }
    private const string wayAPointFile = "Enemy";
    public Type type = Type.NORMAL;

    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        if (type == Type.NORMAL)
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;
            Gizmos.DrawIcon(transform.position + Vector3.up * 1.0f, wayAPointFile, true);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        // 기즈모 색상 설정
        Gizmos.color = _color;

        //구체 모양의 기즈모 생성. 인자는 (생성 위치, 반지름)
        Gizmos.DrawSphere(this.transform.position, _radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
