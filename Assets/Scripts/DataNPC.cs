using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Doris01
{
    /// <summary>
    /// NPC 資料
    /// </summary>
    [CreateAssetMenu(menuName = "Doris01/NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("NPC AI 要分析的句子")]
        public string[] sentences;

    }
}