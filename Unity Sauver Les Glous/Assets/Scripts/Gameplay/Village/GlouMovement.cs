using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouMovement : MonoBehaviour
{
    private float m_deltaY = 0.5f;
    private float m_deltaX = 1f;
    private bool isMovingToTheRight;
    [SerializeField] private bool isMoving = true;

    // Zone autorisé pour les déplacement
    private float m_ilandMaxX = 12.57f;
    private float m_ilandMinX = -19.39f;
    private float m_ilandMaxY = 7.41f;
    private float m_ilandMinY = -12.38f;

    private float m_timeThreshold = 2f;
    private float m_nextMoveTimer;

    private Vector3 m_destination;

    public void Awake()
    {
        m_destination = transform.position;

        DetermineOrientation();
    }

    private void Update()
    {
        if (GetComponent<GlouDragSelect>().IsDragged())
        {
            StopAllCoroutines();
            isMoving = false;
            return;
        }
        if(!isMoving)
        {
            // Le glou a une chance sur 10 de se remettre en marche
            int i = Random.Range(0, 1000);
            if (i == 0)
            {
                isMoving = true;
                m_destination = PickRandomDestination();
                if (isMoving) MoveTo(m_destination);
            }
        }
        else if(m_destination == transform.position)
        {
            m_destination = PickRandomDestination();
            if(isMoving) MoveTo(m_destination);
        }
    }

    // Choisi un point aléatoire dans une zone autour du Glou
    public Vector3 PickRandomDestination()
    {
        Vector3 glouPos = transform.position;

        float xPos = glouPos.x; ;
        if (isMovingToTheRight)
        {
            xPos += m_deltaX;
            if (xPos > m_ilandMaxX)
            {
                xPos = m_ilandMaxX;
                isMovingToTheRight = !isMovingToTheRight;
            }

        }
        else
        {
            xPos -= m_deltaX;
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

        // Le glou a une chance sur 10 de s'arrêter
        i = Random.Range(0, 10);
        if (i == 0) isMoving = false;

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
        float duration = 2f;
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
