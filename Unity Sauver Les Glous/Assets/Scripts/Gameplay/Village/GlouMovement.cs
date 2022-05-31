using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouMovement : MonoBehaviour
{
    private float m_deltaY = 0.5f;
    private float m_xRange = 1f;
    private bool isMovingToTheRight;

    // Zone autorisé pour les déplacement
    private float m_ilandMaxX = 12.57f;
    private float m_ilandMinX = -19.39f;
    private float m_ilandMaxY = 7.41f;
    private float m_ilandMinY = -12.38f;

    private float m_timeThreshold = 2f;
    private float m_nextMoveTimer;

    private Vector3 m_destination;

    private void Awake()
    {
        m_destination = transform.position;

        DetermineOrientation();
    }

    private void Update()
    {
        if(m_destination == transform.position)
        {
            m_destination = PickRandomDestination();
            MoveTo(m_destination);
        }
    }

    // Choisi un point aléatoire dans un carré de "rayon" m_rayon
    private Vector3 PickRandomDestination()
    {
        Vector3 glouPos = transform.position;

        float xPos = glouPos.x; ;
        if (isMovingToTheRight)
        {
            xPos += m_xRange;
            if (xPos > m_ilandMaxX)
            {
                xPos = m_ilandMaxX;
                isMovingToTheRight = !isMovingToTheRight;
            }

        }
        else
        {
            xPos -= m_xRange;
            if(xPos < m_ilandMinX)
            {
                xPos = m_ilandMinX;
                isMovingToTheRight = !isMovingToTheRight;
            }
        }

        float maxY = glouPos.y + m_deltaY;
        if (maxY > m_ilandMaxY) maxY = m_ilandMaxY;
        float minY = glouPos.y - m_deltaY;
        if (minY < m_ilandMinY) minY = m_ilandMinY;

        // Le glou a une chance sur 10 de changer de direction
        int i = Random.Range(0, 10);
        if (i == 0) isMovingToTheRight = !isMovingToTheRight;

        return new Vector3(xPos, Random.Range(minY, maxY), 0);
    }

    // Déplace le glou de façon fluide vers sa destination
    private void MoveTo(Vector3 destination)
    {
        StartCoroutine(MoveTo_Coroutine(destination));
    }

    private IEnumerator MoveTo_Coroutine(Vector3 destination)
    {
        float time = 0;
        float duration = 1f;
        Vector2 glouPos = transform.position;


        while (time < duration)
        {
            transform.position = Vector2.Lerp(glouPos, destination, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;
    }

    // Permet d'orienter le glou pour qu'il se déplace vers la gauche ou vers la droite
    private void DetermineOrientation()
    {
        int i = Random.Range(0, 2);
        if (i == 1) isMovingToTheRight = true;
        else isMovingToTheRight = false;
    }
}
