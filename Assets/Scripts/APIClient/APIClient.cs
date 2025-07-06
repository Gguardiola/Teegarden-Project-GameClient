using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIClient : MonoBehaviour
{
    private string _baseUrl = "http://localhost";
    private string _accessToken;
    public bool IsLoggedIn = false;

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using UnityWebRequest www = UnityWebRequest.Post($"{_baseUrl}/login", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var response = www.downloadHandler.text;
            Debug.Log("Login response: " + response);
            _accessToken = JsonUtility.FromJson<TokenResponse>(response).access_token;
            IsLoggedIn = true;
        }
        else
        {
            Debug.LogError("Login failed: " + www.error);
        }
    }

    public IEnumerator PostCombatLog(string jsonPayload)
    {
        using UnityWebRequest www = new UnityWebRequest($"{_baseUrl}/combatlog", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {_accessToken}");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Combat log sent: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Combat log error: " + www.error);
        }
    }

    [System.Serializable]
    private class TokenResponse
    {
        public string access_token;
    }
}