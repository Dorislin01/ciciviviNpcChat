using TMPro;
using UnityEngine;

namespace Doris01
{
  ///<summary>
  ///模型管理器
  ///</summary>
  public class ModelManager1 : MonoBehaviour
  {
        private string url = "https://g.ubitus.ai/v1/chat/completions";
        private string key = "d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==";

        private TMP_InputField inputField;

        //喚醒事件:遊戲播放後執行一次
        private void Awake()
        {
            //尋找場景上名稱為輸入介面的物件 並存放到 inputField變數內
            inputField = GameObject.Find("輸入介面").GetComponent<TMP_InputField>();
            //當玩家結束編輯輸入介面時會執行 PlayerInput 方法
            inputField.onEndEdit.AddListener(PlayerInput);
        }

        private void PlayerInput(string input)
        {
            print($"<color=#363>{input}</color>");
        }
  }
}
