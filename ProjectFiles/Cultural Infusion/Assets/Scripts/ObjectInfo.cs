using UnityEngine;
using System.Collections;

public class ObjectInfo : MonoBehaviour
{
    public string m_name;
    public string m_description;

    void Start()
    {
        ObjectManager.AddObject(gameObject);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
