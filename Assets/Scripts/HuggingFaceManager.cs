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
        private void Awake()
        {
            inputFieldPlayer = GameObject.Find("輸入欄位1").GetComponent<TMP_InputField>();


            inputFieldPlayer.onEndEdit.AddListener(PlayerInput);
        }
        private void PlayerInput(string input)
        {
            print($"<color=#3f3>玩家輸入 :{input}</color>");

        }

    }
}

