using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class FPD_HideOnBoolAttribute : PropertyAttribute
{
    public string BoolVarName = "";
    public bool HideInInspector = false;

    public void ConditionalHideAttribute(string boolVariableName)
    {
        BoolVarName = boolVariableName;
        HideInInspector = false;
    }

    public void ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        BoolVarName = conditionalSourceField;
        HideInInspector = hideInInspector;
    }
}