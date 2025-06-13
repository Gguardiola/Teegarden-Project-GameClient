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
        Debug.Log($"[CombatLogUtils] Log guardado: {filename}");
    }

    public static List<CombatLog> LoadAllLogs()
    {
        string path = GetFolderPath();
        List<CombatLog> logs = new List<CombatLog>();

        if (!Directory.Exists(path)) return logs;

        foreach (string filePath in Directory.GetFiles(path, "*.json"))
        {
            string json = File.ReadAllText(filePath);
            CombatLog log = JsonUtility.FromJson<CombatLog>(json);
            logs.Add(log);
        }

        return logs;
    }

    public static List<string> GetAllLogFilePaths()
    {
        string path = GetFolderPath();
        if (!Directory.Exists(path)) return new List<string>();
        return new List<string>(Directory.GetFiles(path, "*.json"));
    }

    public static IEnumerator UploadAllLogsInBulk(string url, System.Action<bool> onComplete = null)
    {
        List<CombatLog> logs = LoadAllLogs();
        if (logs.Count == 0)
        {
            Debug.Log("[CombatLogUtils] No hay logs para enviar.");
            onComplete?.Invoke(false);
            yield break;
        }

        string json = JsonUtility.ToJson(new CombatLogBulkWrapper(logs));
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("[CombatLogUtils] Todos los logs enviados correctamente.");
                DeleteAllLogsFromDisk();
                onComplete?.Invoke(true);
            }
            else
            {
                Debug.LogError($"[CombatLogUtils] Error al enviar logs: {request.error}");
                onComplete?.Invoke(false);
            }
        }
    }

    public static void DeleteAllLogsFromDisk()
    {
        string path = GetFolderPath();
        if (!Directory.Exists(path)) return;

        foreach (string file in Directory.GetFiles(path, "*.json"))
        {
            File.Delete(file);
        }
        Debug.Log("[CombatLogUtils] Logs eliminados del disco.");
    }

    private static string GetFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, folderName);
    }
}