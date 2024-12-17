using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GoogleGeminiAPI : MonoBehaviour
{
    private string apiKey = userdata.get('2615')
    private string apiUrl = https://g.ubitus.ai/v1/chat/completions;

    // �o�e�ШD�� Google Gemini API
    public IEnumerator GenerateContent(string prompt, System.Action<string> onSuccess)
    {
        // �]�m�ШD�� JSON ���e
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"" + prompt + "\"}]}]}";
        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        // �]�m HTTP �ШD
        UnityWebRequest request = new UnityWebRequest(apiUrl + "?key=" + apiKey, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);

            // �ѪR�^�Ǥ��e�]²��B�z�^
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
        // ²�檺�r��B�z�A���� AI �^������ "text" ����
        int startIndex = jsonResponse.IndexOf("\"text\":") + 8;
        int endIndex = jsonResponse.IndexOf("\"", startIndex);
        if (startIndex > 0 && endIndex > startIndex)
        {
            return jsonResponse.Substring(startIndex, endIndex - startIndex);
        }
        return "Invalid AI response";
    }
}
