/* Florent BOGACZ
 * Deplacer la camera avec la souris dans le village
 * A Placer sur la 'Main Camera'
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float m_panSpeed = 10f;
    [SerializeField] float m_panBorderThickness = 10f;
    [SerializeField] Vector2 m_panLimit = new Vector2(10f,10f);

    // mouse detection position
    float m_maxY ;
    float m_minY ;
    float m_maxX ;
    float m_minX ;

    Vector3 m_cameraPos;

    private void Start()
    {
        m_cameraPos = transform.position;

        // Set mouse detection position to move the camera
        m_maxY = 1 - m_panBorderThickness / Screen.height;
        m_minY = m_panBorderThickness / Screen.height;
        m_maxX = 1 - m_panBorderThickness / Screen.width;
        m_minX = m_panBorderThickness / Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the curor Position
        Vector2 cursorPos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        // Set next Camera pos 
        if (cursorPos.y >= m_maxY)  m_cameraPos.y += m_panSpeed * Time.deltaTime;                  
        if (cursorPos.y <= m_minY)  m_cameraPos.y -= m_panSpeed * Time.deltaTime;                  
        if (cursorPos.x >= m_maxX)  m_cameraPos.x += m_panSpeed * Time.deltaTime;                  
        if (cursorPos.x <= m_minX)  m_cameraPos.x -= m_panSpeed * Time.deltaTime;

        // Apply Limit 
        m_cameraPos.x = Mathf.Clamp(m_cameraPos.x, -m_panLimit.x, m_panLimit.x);
        m_cameraPos.y = Mathf.Clamp(m_cameraPos.y, -m_panLimit.y, m_panLimit.y);

        // Update Camera Position
        transform.position = m_cameraPos;
    }
}
