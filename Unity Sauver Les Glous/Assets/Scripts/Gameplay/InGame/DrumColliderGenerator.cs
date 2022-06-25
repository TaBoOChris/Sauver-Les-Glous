using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumColliderGenerator : MonoBehaviour
{

    [SerializeField] int m_nbPoints = 8;
    [SerializeField] float m_radius = 5;

    // Start is called before the first frame update
    void Start()
    {
        DrawCollider();

    }

    private void DrawCollider()
    {
        EdgeCollider2D drumCollider = GetComponent<EdgeCollider2D>();

        List<Vector2> points = new List<Vector2>();

        for(int i = 0; i<m_nbPoints+1; i++)
        {
            float angle = Mathf.Deg2Rad * (360 * i) / m_nbPoints;
            points.Add(m_radius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
        }


        drumCollider.SetPoints(points);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
