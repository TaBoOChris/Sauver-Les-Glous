using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouMovement : MonoBehaviour
{
    // Zone autour du Glou (un carré de "rayon" X parceque flemme de faire un cercle ça revient au même ^^)
    private int m_rayon = 3;

    // Zone autorisé pour les déplacement
    private float m_ilandMaxX = 12.57f;
    private float m_ilandMinX = -19.39f;
    private float m_ilandMaxY = 7.41f;
    private float m_ilandMinY = -12.38f;

    private float m_timeThreshold = 0.1f;
    private float m_nextMoveTimer;

    private Vector3 m_destination;

    private void Awake()
    {
        m_nextMoveTimer = Time.time + m_timeThreshold;
    }

    private void Update()
    {
        if(Time.time > m_nextMoveTimer)
        {
            m_destination = PickRandomDestination();
            MoveTo(m_destination);



            m_nextMoveTimer = Time.time + m_timeThreshold;
        }
    }

    // Choisi un point aléatoire dans un carré de "rayon" m_rayon
    private Vector3 PickRandomDestination()
    {
        Vector3 glouPos = transform.position;

        float maxX = glouPos.x + m_rayon;
        if (maxX > m_ilandMaxX) maxX = m_ilandMaxX;
        float minX = glouPos.x - m_rayon;
        if (minX < m_ilandMinX) minX = m_ilandMinX;
        float maxY = glouPos.y + m_rayon;
        if (maxY > m_ilandMaxY) maxY = m_ilandMaxY;
        float minY = glouPos.y - m_rayon;
        if (minY < m_ilandMinY) minY = m_ilandMinY;

        return new Vector3(Random.Range(minX, minY), Random.Range(minY, maxY), 0);
    }

    private void MoveTo(Vector3 destination)
    {
        transform.position = destination;
    }
}
