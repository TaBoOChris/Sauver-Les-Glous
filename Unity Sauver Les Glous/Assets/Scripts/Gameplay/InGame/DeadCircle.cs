using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCircle : MonoBehaviour
{
    [SerializeField] private GameObject m_glouGhost;
    [SerializeField] private GameObject m_glousGhostParent;

    // Start is called before the first frame update
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.parent.tag != "Glou") return;
        
        Debug.Log("Kill Glou");

        if (GameManager.Instance)
        {
            GameManager.Instance.GlouDie();
            AudioManager.Instance.PlayGlouDie();
            StartCoroutine(GlouDieAnimation_Coroutine(other));
        }

        Destroy(other.transform.parent.gameObject,0.2f);
    }

    IEnumerator GlouDieAnimation_Coroutine(Collider2D other)
    {
        GameObject newGlou = Instantiate(m_glouGhost, other.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, m_glousGhostParent.transform);
        yield return new WaitForSeconds(1.0f);
        Destroy(newGlou);
    }
}
