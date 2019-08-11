using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public void DoJump(float str, float time)
    {
        ServiceLocator.GetDebugProvider().Log("Jump");
        iTween.PunchPosition(this.gameObject, Vector3.up * str, time);
    }
}
