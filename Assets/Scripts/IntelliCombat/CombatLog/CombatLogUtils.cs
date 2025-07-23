using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public static class CombatLogUtils
{
    private static string folderName = "combat_logs";

    public static void SaveLogToDisk(CombatLog log)
    {
        string json = JsonUtility.ToJson(log, true);
        string path = GetFolderPath();
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string filename = $"log_{System.DateTime.Now:yyyyMMdd_HHmmssfff}.json";
        File.WriteAllText(Path.Combine(path, filename), json);
        Debug.Log("Log stored at: " + Path.Combine(path, filename));
    }

    private static string GetFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, folderName);
    }
}