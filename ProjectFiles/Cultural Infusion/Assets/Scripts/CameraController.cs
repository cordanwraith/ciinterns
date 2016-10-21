using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Camera m_Camera;
    Transform m_Hinge;
    public float m_Speed;
 
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_Hinge  = gameObject.transform.parent;
        m_Speed *= 10;
    }

    void Update()
    {
        CheckRotate();
    }
 
    void CheckRotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Hinge.transform.Rotate(Vector3.up * (Time.deltaTime * m_Speed));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Hinge.transform.Rotate(Vector3.down * (Time.deltaTime * m_Speed));
        }
    }
}
