using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIClient : MonoBehaviour
{
    private string baseUrl = "http://157.245.22.76";
    /*
    private string baseUrl = "http://localhost";
    */
    
    private string accessToken;
    public bool IsLoggedIn = false;
    public APIClientHandler apiClientHandler;
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using UnityWebRequest www = UnityWebRequest.Post($"{baseUrl}/login", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var response = www.downloadHandler.text;
            Debug.Log("Login response: " + response);
            accessToken = JsonUtility.FromJson<TokenResponse>(response).access_token;
            IsLoggedIn = true;
            StartCoroutine(DownloadLatestModel());
        }
        else
        {
            Debug.LogError("Login failed: " + www.error);
            ThrowError(www.error);
        }
    }

    public IEnumerator PostCombatLog(string jsonPayload)
    {
        using UnityWebRequest www = new UnityWebRequest($"{baseUrl}/combatlog", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {accessToken}");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Combat log sent: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Combat log error: " + www.error);
            ThrowError(www.error);
        }
    }
    
    IEnumerator DownloadLatestModel()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "/model?version=latest");
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        request.SetRequestHeader("Accept", "application/json"); 
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to get presigned URL: " + request.error);
            yield break;
        }

        string json = request.downloadHandler.text;
        string downloadUrl = JsonUtility.FromJson<ModelResponse>(json).download_url;

        UnityWebRequest fileRequest = UnityWebRequest.Get(downloadUrl);
        yield return fileRequest.SendWebRequest();

        if (fileRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download model: " + fileRequest.error);
            yield break;
        }

        byte[] data = fileRequest.downloadHandler.data;
        string folder = Path.Combine(Application.persistentDataPath, "AIModels");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        string savePath = Path.Combine(folder, "intellicombat_model_ready_latest.onnx");
        File.WriteAllBytes(savePath, data);

        Debug.Log("Model downloaded to: " + savePath);
    }

    [System.Serializable]
    public class ModelResponse
    {
        public string download_url;
    }
    
    private void ThrowError(string errMsg)
    {
        if (apiClientHandler != null)
        {
            apiClientHandler.ShowErrorPopUp(errMsg);
        }
    }

    [System.Serializable]
    private class TokenResponse
    {
        public string access_token;
    }
    
}