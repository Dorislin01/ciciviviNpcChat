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
        [SerializeField, Header("�ʵe�Ѽ�")]
        private string[] parameters =
        {
            "Ĳ�o�}��","Ĳ�o�Y�w","Ĳ�o����"
        };
        private Animator ani;
        public DataNPC data => dataNPC;
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        public void PlayAinmation(int index)
        {
            ani.SetTrigger(parameters[index]);
        }

    }
}
