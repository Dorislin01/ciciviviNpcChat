using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;


namespace Doris01
{
    /// <summary>
    /// Hugging Face 管理器
    /// </summary>
    public class HuggingFaceManager : MonoBehaviour
    {
        [SerializeField, Header("要互動的 NPC")]
        private NPCController npc;

        private string key = "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";
        private string model = "https://api-inference.huggingface.co/models/sentence-transformers/all-MiniLM-L12-v2";
        
        private TMP_InputField inputFieldPlayer;


        private string inputText;
        private string[] npcSentences;

        private void Awake()
        {
            inputFieldPlayer = GameObject.Find("輸入欄位1").GetComponent<TMP_InputField>();
            inputFieldPlayer.onEndEdit.AddListener(PlayerInput);
            npcSentences = npc.data.sentences;
        }
        private void PlayerInput(string input)
        { 
            print($"<color=#3f3>玩家輸入 :{input}</color>");
            inputText = input;
            StartCoroutine(GetSimilarity());
        }
    }
}
            private IEnumerator GetSimilarity()
{
    var requestData = new
    {
        inputs = new
        {
            source_sentence = inputText,
            sentences = npcSentences
        }
    };

    string jsonBody = JsonConvert.SerializeObject(requestData);
    byte[] postData = Encoding.UTF8.GetBytes(jsonBody);

    UnityWebRequest request = new UnityWebRequest(model, "POST");
    request.uploadHandler = new UploadHandlerRaw(postData);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    request.SetRequestHeader("Authorization", "Bearer " + key);

    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
        print($"<color=#f33>要求失敗:(request.error)</color>");
    }
    else
    {
        string responseText = request.downloadHandler.text;
        var response = JsonConvert.DeserializeObject<List<float>>(responseText);

        print($"<color=#3f3>分數:(responseText)</color>");

        if (response != null && response.Count > 0)
        {
            int best = response.Select (value, index) => new
            Value = value, Index = index
        子).OrderByDescending(x => x.Value).First().Index;
            print($"<color=#77f>最佳結果:[npcSentences[best]]</color>");

            npc.PlayAinmation(best);
        }
     }
   }
  }
}
