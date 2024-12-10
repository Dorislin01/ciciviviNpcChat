using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doris01
{
    /// <summary>
    /// NPC ���
    /// </summary>
public class NPCController : MonoBehaviour
{
        [SerializeField, Header("NPC ���")]
        private DataNPC dataNPC;

        private Animator ani;
        public DataNPC data => dataNPC;
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

    }
}
