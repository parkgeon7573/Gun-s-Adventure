using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Color m_color = Color.white;
    private void OnDrawGizmos()
    {
        Gizmos.color = m_color;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
