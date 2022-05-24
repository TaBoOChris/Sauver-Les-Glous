using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class PlatformGroup : MonoBehaviour
{
    [SerializeField]
    [OnChangedCall("OnRadiusChange")]
    private float radius = 5.0f;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        RearangePlatforms();
    }

    public void OnRadiusChange()
    {
        if(radius < 1.0f) radius = 1.0f;
        RearangePlatforms();
    }

    void RearangePlatforms()
    {
        Vector2 center = transform.position;
        foreach (var platform in transform.GetComponentsInChildren<Platform>())
        { //Iterate once over each children platform
            platform.pivotPoint = center;

            //normalize direction to put platform on the radius edge
            Vector3 diff = platform.transform.position - transform.position;
            diff.z = 0;
            diff = diff.normalized;
            platform.transform.position = transform.position + (diff * radius);
            platform.ForceRefresh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }

    private void OnDrawGizmosSelected()
    { //Recompute platforms arrangement if in selection and under special conditions
        if (Application.isEditor) {
            if (Application.isPlaying)
            {
                if(transform.position != position)
                { //Only recompute platforms positions if the group has been moved in editor
                    position = transform.position;
                    RearangePlatforms();
                }
            }
            else
            {
                //Use wisely - only in editor (consumes performance as there is no free way of telling if moved in editor)
                RearangePlatforms();
            }
        }
    }
}
