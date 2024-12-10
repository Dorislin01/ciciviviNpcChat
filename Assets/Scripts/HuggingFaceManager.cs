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
    /// Hugging Face �޲z��
    /// </summary>
    public class HuggingFaceManager : MonoBehaviour
    {
        [SerializeField, Header("�n���ʪ� NPC")]
        private NPCController npc;

        private string key = "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";
        private string model = "https://api-inference.huggingface.co/models/sentence-transformers/all-MiniLM-L12-v2";
        
        private TMP_InputField inputFieldPlayer;
        private void Awake()
        {
            inputFieldPlayer = GameObject.Find("��J���1").GetComponent<TMP_InputField>();


            inputFieldPlayer.onEndEdit.AddListener(PlayerInput);
        }
        private void PlayerInput(string input)
        {
            print($"<color=#3f3>���a��J :{input}</color>");

        }

    }
}

