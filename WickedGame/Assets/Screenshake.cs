using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{

    public static Screenshake INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Vector3.zero;
        Camera.main.transform.SetParent(this.transform, true);
        Camera.main.transform.localPosition = Vector3.zero;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DoScreenshake();
        }
    }

    public void DoScreenshake()
    {
        if (GetComponent<iTween>())
        {
            Destroy(this.gameObject);
            this.transform.position = Vector3.zero;
        }

        iTween.PunchPosition(this.gameObject, Random.insideUnitSphere * 1, 0.2f);
    }
}
