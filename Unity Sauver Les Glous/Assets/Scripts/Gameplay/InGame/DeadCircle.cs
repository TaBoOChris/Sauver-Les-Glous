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
        
        // kill glou
        Debug.Log("Kill Glou");
        other.transform.parent.GetComponent<GlouInGame>().KillGlou();

        // effects of death
        GameManager.Instance.GlouDie();
        AudioManager.Instance.PlayGlouDie();
        StartCoroutine(GlouDieAnimation_Coroutine(other));

        Destroy(other.transform.parent.gameObject);
    }

    IEnumerator GlouDieAnimation_Coroutine(Collider2D other)
    {
        GameObject newGlou = Instantiate(m_glouGhost, other.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, m_glousGhostParent.transform);
        newGlou.transform.localScale = other.transform.parent.transform.localScale;
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(newGlou);
    }
}
