using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    [SerializeField]
    private RuleSet _ruleSet;

    public static RuleSet ruleSet;

    private void Awake()
    {
        if (_ruleSet == null)
        {
            Debug.LogError("no rules set");
            return;
        }

        SetRules();
    }

    public void SetRules()
    {
        if (ruleSet != null)
        {
            return;
        }
        ruleSet = _ruleSet;
    }
}
