using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;

    public float waitTime;
    public float t;

    float speed = 0.5f;
    Vector3[] path;
    int targetIndex;
    Vector3 left2 = new Vector3(-4, 0, 0);
    Vector3 up2 = new Vector3(0, 1, 0);

    void Start()
    {
        PathRequestManager.instance.RequestPath(transform.position+left2, target.position+up2, OnPathFound);
        StartCoroutine("pathing");
    }
    private void Update()
    {
        waitTime += Time.deltaTime;

        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y,
            transform.localEulerAngles.z + 90.0f);
        //PathRequestManager.instance.RequestPath(transform.position+left2, target.position+up2, OnPathFound);
    }
    IEnumerator pathing()
    {
        while(true)
        {
            t += Time.deltaTime;
            PathRequestManager.instance.RequestPath(transform.position + left2, target.position + up2, OnPathFound);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            if(waitTime >= 4.0f)
            { 
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed*0.012f);
            yield return null;
        }
    }
}
