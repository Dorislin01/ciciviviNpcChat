using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doris01
{
    /// <summary>
    /// NPC 控制器
    /// </summary>
public class NPCController : MonoBehaviour
{
        [SerializeField, Header("NPC 資料")]
        private DataNPC dataNPC;
        [SerializeField, Header("動畫參數")]
        private string[] parameters =
        {
            "觸發開心","觸發頭暈","觸發攻擊"
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
