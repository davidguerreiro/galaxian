using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalVars : ScriptableObject
{
    [Serializable]
    public struct LVars {
        public string name;
        public bool value;

        public LVars(string name, bool value) {
            this.name = name;
            this.value = value;
        }
    } 

    public LVars[] variables;

    /// <summary>
    /// Set all variables to false.
    /// </sumamry>
    public void Reset()
    {
        for (int i = 0; i < variables.Length; i++) {
            variables[i].value = false;
        }
    }

    /// <summary>
    /// Clear all variables.
    /// </summaru>
    public void Clear()
    {
        variables = new LVars[20];
    }

    /// <summary>
    /// Get variable value.
    /// </summary>
    /// <param name="name">string - variable name.</param>
    public bool GetVar(string name)
    {
        foreach (LVars variable in variables) {
            if (variable.name == name) {
                return variable.value;
            }
        }

        return false;
    }

    /// <summary>
    /// Update variable.
    /// </sumamry>
    /// <param name="name">string - variable name</param>
    /// <praam name="value">bool - variable value</param>
    public void SetVar(string name, bool value)
    {
        for (int i = 0; i < variables.Length; i++) {
            if (variables[i].name == name) {
                variables[i].value = value;
                break;
            }
        }
    }

}
