using UnityEngine;

/// <summary>
/// Creating elegant frame for foldout feature, you need add macro [HideInInspector] for all additional variablesToStore
/// </summary>
public class FPD_FoldFrameAttribute : PropertyAttribute
{
    public string FrameTitle;
    public string[] VariablesToStore;
    public bool Folded;

    /// <summary>
    /// Creating elegant frame for foldout feature, you need add macro [HideInInspector] for all additional variablesToStore
    /// </summary>
    /// <param name="title"> Title for foldout frame </param>
    /// <param name="variablesToStore"> name of variables which have to be inside foldout, do it like "new string[3] {"variable1", "variable2", "variable3"}" to all this variables add macro [HideInInspecor] (they must be public)</param>
    /// <param name="defaultUnfold"> If foldout should be unfolded by default </param>
    public FPD_FoldFrameAttribute(string title, string[] variablesToStore, bool defaultUnfold = false)
    {
        FrameTitle = title;
        VariablesToStore = variablesToStore;
        Folded = defaultUnfold;
    }
}
