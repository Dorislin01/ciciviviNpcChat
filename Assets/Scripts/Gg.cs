using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GoogleGeminiAPI : MonoBehaviour
{
    private string apiKey = userdata.get('2615')
    private string apiUrl = https://g.ubitus.ai/v1/chat/completions;

    // 發送請求至 Google Gemini API
    public IEnumerator GenerateContent(string prompt, System.Action<string> onSuccess)
    {
        // 設置請求的 JSON 內容
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"" + prompt + "\"}]}]}";
        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        // 設置 HTTP 請求
        UnityWebRequest request = new UnityWebRequest(apiUrl + "?key=" + apiKey, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);

            // 解析回傳內容（簡單處理）
            string generatedText = ExtractGeneratedText(responseText);
            onSuccess?.Invoke(generatedText);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            onSuccess?.Invoke("Error in AI response");
        }
    }

    private string ExtractGeneratedText(string jsonResponse)
    {
        // 簡單的字串處理，提取 AI 回應中的 "text" 部分
        int startIndex = jsonResponse.IndexOf("\"text\":") + 8;
        int endIndex = jsonResponse.IndexOf("\"", startIndex);
        if (startIndex > 0 && endIndex > startIndex)
        {
            return jsonResponse.Substring(startIndex, endIndex - startIndex);
        }
        return "Invalid AI response";
    }
}
