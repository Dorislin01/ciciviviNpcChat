using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Doris01
{
    /// <summary>
    /// NPC ���
    /// </summary>
    [CreateAssetMenu(menuName = "Doris01/NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("NPC AI �n���R���y�l")]
        public string[] sentences;

    }
}