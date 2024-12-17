using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Doris01
{
    public class ModelManager : MonoBehaviour
    {
            private string url = "https://g.ubitus.ai/v1/chat/completions";
            private string key = "d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==";

            private TMP_InputField inputField;
            private string prompt;

            public ModelManager target;

            private void Awake()
            {
                inputField = GameObject.Find("輸入介面").GetComponent<TMP_InputField>();
                inputField.onEndEdit.AddListener(PlayerInput);
            }

            public void PlayerInput(string input)
            {
                Debug.Log($"<color=#3f3>玩家輸入: {input}</color>");
                prompt = input;
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
                        new { role = "system", content = "你是一個嚴肅的面試官" },
                        new { role = "user", content = prompt }
                    },
                    stop = new string[] { "<|eot_id|>", "<|end_of_text|>" },
                    frequency_penalty = 0,
                    max_tokens = 2000,
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

                Debug.Log("發送請求...");

                // 發送請求並等待回應
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"回應成功: {request.downloadHandler.text}");
                    // 解析 JSON 回應
                    var response = JsonConvert.DeserializeObject<ResponseData>(request.downloadHandler.text);
                    Debug.Log($"回應內容: {response.choices[0].message.content}");

                    target.PlayerInput(response.choices[0].message.content);
                }
                else
                {
                    Debug.LogError($"錯誤: {request.responseCode} - {request.error}");
                    Debug.LogError($"回應: {request.downloadHandler.text}");
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
}
