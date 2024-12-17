using System.Collections;
using System.Collections.Generic;
using Doris01;
using Newtonsoft.Json;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CICI_Model : MonoBehaviour
{
    private string url = "https://g.ubitus.ai/v1/chat/completions";
    private string key = "d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==";

    private TMP_InputField inputField;
    private string prompt;

    public Text text02;

    public VIVI_Model target;


    public void PlayerInput(string input)
    {
        Debug.Log($"<color=#3f3>���a��J: {input}</color>");
        prompt = input;
        StartCoroutine(GetResult());
    }

    private IEnumerator GetResult()
    {
        // �w�q�ǰe����Ƶ��c
        var requestData = new
        {
            model = "llama-3.1-70b",
            messages = new List<object>
                {
                    new { role = "system", content = "�A�O�@���Y�ª����թx" },
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

        // �ǦC�Ƹ�Ƭ� JSON �r�Ŧ�
        string json = JsonConvert.SerializeObject(requestData);
        byte[] postData = Encoding.UTF8.GetBytes(json);

        // �]�m HTTP �ШD
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();

        // �K�[ Header
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + key);

        Debug.Log("�o�e�ШD...");

        // �o�e�ШD�õ��ݦ^��
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"�^�����\: {request.downloadHandler.text}");
            // �ѪR JSON �^��
            var response = JsonConvert.DeserializeObject<ResponseData>(request.downloadHandler.text);
            Debug.Log($"�^�����e: {response.choices[0].message.content}");

            target.PlayerInput(response.choices[0].message.content);
            text02.text = response.choices[0].message.content;
        }
        else
        {
            Debug.LogError($"���~: {request.responseCode} - {request.error}");
            Debug.LogError($"�^��: {request.downloadHandler.text}");
        }
    }

    // �w�q�Ω�ѪR API �^�������O
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
