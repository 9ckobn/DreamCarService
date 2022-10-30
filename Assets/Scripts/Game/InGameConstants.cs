using System.IO;
using UnityEngine;

public static class InGameConstants
{
    public static string password = "Asinaria";
    private const string DataFolder = @"Assets/Data";

    public static string dataPath(string FileName)
    {
#if UNITY_EDITOR
        return Path.Combine(DataFolder, FileName);
#elif !UNITY_EDITOR
        return Path.Combine(Application.persistentDataPath, FileName);
#endif
    }
}
