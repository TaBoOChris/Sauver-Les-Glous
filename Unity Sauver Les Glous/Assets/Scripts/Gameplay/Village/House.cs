using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private int m_identifier;
    private BoxCollider2D m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if(m_collider == null)
            m_collider = GetComponent<BoxCollider2D>();

        // Draw a yellow cube at the transform position
        Gizmos.color = Color.red;
        Vector3 pos = transform.position + new Vector3(m_collider.offset.x, m_collider.offset.y, 0);
        Gizmos.DrawWireCube(pos , m_collider.size) ;

        var restoreColor = GUI.color;
        GUI.color = Color.blue;

        UnityEditor.Handles.Label(pos - new Vector3(0.5f,-1.0f,0), "House N°" + m_identifier);
        GUI.color = restoreColor;

    }

    public int GetHouseID()
    {
        return m_identifier;
    }

}
