using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealDebugProvider : IDebugService
{
    public void Log(System.Object message, LogType type = LogType.Log)
    {
        Debug.unityLogger.Log(type, message.ToString());
    }
}
