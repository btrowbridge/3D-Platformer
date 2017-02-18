

using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{
    Camera m_Camera;
    void Start()
    {
        m_Camera = FindObjectOfType<Camera>();
        
    }
    void Update()
    {
        transform.LookAt(m_Camera.transform);
    }
}

