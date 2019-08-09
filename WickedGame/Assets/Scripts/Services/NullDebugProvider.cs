using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullDebugProvider : IDebugService
{
    public void Log(object message, LogType type = LogType.Log)
    {
        return;
    }
}
