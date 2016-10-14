using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform m_Hinge;
    public float m_Speed;
    Camera m_Camera;

    void Start()
    {
        gameObject.transform.SetParent(m_Hinge);
        m_Camera = GetComponent<Camera>();
    }
	
	void Update()
    {
        CheckRotate();
        CheckSelection();
    }

    void CheckRotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Hinge.transform.Rotate(Vector3.up * (Time.deltaTime * m_Speed * 10));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Hinge.transform.Rotate(Vector3.down * (Time.deltaTime * m_Speed * 10));
        }
    }

    void CheckSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<ObjectInfo>() != null)
                {
                    ObjectManager.Hit(hit.transform.gameObject);
                }
            }
        }
    }
}
