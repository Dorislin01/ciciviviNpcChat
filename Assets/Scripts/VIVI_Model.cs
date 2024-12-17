using System.Collections;
using System.Collections.Generic;
using Doris01;
using Newtonsoft.Json;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VIVI_Model : MonoBehaviour
{
    private string url = "https://g.ubitus.ai/v1/chat/completions";
    private string key = "d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==";

    private TMP_InputField inputField;
    private string prompt;

    public CICI_Model target;

    public Text text;

    private void Start()
    {
        target.PlayerInput("你好，我是師大設計系學生芋頭。 很高興認識你");
    }


    public void PlayerInput(string input)
    {
        //Debug.Log($"<color=#3f3>玩家輸入: {input}</color>");
        var clamp = "接下來請使用口語化的聊天 你的人物設定是設計系女學生,喜歡唱歌跳舞。 \n \n";


        prompt = clamp + input;
        StartCoroutine(GetResult());
    }

    private IEnumerator GetResult()
    {
        // 定義傳送的資料結構
        var requestData = new
        {
            model = "llama-3.1-70b",
            messages = new List<object>
                {
                    new { role = "system", content = "設計系女學生,喜歡唱歌跳舞。" },
                    new { role = "user", content = prompt }
                },
            stop = new string[] { "<|eot_id|>", "<|end_of_text|>" },
            frequency_penalty = 0,
            max_tokens = 100,
            temperature = 0.2f,
            top_p = 0.5f,
            top_k = 20,
            stream = false
        };

        // 序列化資料為 JSON 字符串
        string json = JsonConvert.SerializeObject(requestData);
        byte[] postData = Encoding.UTF8.GetBytes(json);

        // 設置 HTTP 請求
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 添加 Header
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + key);

       // Debug.Log("發送請求...");

        // 發送請求並等待回應
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log($"回應成功: {request.downloadHandler.text}");
            // 解析 JSON 回應
            var response = JsonConvert.DeserializeObject<ResponseData>(request.downloadHandler.text);
            Debug.Log($"回應內容: {response.choices[0].message.content}");

            target.PlayerInput(response.choices[0].message.content);

            text.text = "VIVI:" + response.choices[0].message.content;
        }
        else
        {
            //Debug.LogError($"錯誤: {request.responseCode} - {request.error}");
            //Debug.LogError($"回應: {request.downloadHandler.text}");
        }
    }

    // 定義用於解析 API 回應的類別
    [System.Serializable]
    private class ResponseData
    {
        public List<Choice> choices;
    }

    [System.Serializable]
    private class Choice
    {
        public Message message;
    }

    [System.Serializable]
    private class Message
    {
        public string role;
        public string content;
    }
}
