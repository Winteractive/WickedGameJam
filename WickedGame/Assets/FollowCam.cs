using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        Vector3 targetPos = Vector3.zero;
        targetPos = target.transform.position;
        targetPos += Vector3.up * 7;
        targetPos += Vector3.back * 4;
        // transform.LookAt(target);
        transform.rotation = Quaternion.Euler(60, 0, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = Vector3.zero;
        targetPos = target.transform.position;
        targetPos += Vector3.up * 7;
        targetPos += Vector3.back * 4;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.fixedDeltaTime * 2);

    }
}
