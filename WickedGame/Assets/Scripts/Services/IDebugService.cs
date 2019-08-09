using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebugService
{

    void Log(System.Object message, UnityEngine.LogType type = LogType.Log);



}
