using TMPro;
using UnityEngine;

namespace Doris01
{
  ///<summary>
  ///�ҫ��޲z��
  ///</summary>
  public class ModelManager1 : MonoBehaviour
  {
        private string url = "https://g.ubitus.ai/v1/chat/completions";
        private string key = "d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==";

        private TMP_InputField inputField;

        //����ƥ�:�C����������@��
        private void Awake()
        {
            //�M������W�W�٬���J���������� �æs��� inputField�ܼƤ�
            inputField = GameObject.Find("��J����").GetComponent<TMP_InputField>();
            //���a�����s���J�����ɷ|���� PlayerInput ��k
            inputField.onEndEdit.AddListener(PlayerInput);
        }

        private void PlayerInput(string input)
        {
            print($"<color=#363>{input}</color>");
        }
  }
}
