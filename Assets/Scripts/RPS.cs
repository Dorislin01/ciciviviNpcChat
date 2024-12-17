using System.Collections;
using UnityEngine;

public class ciciviviNpcChat : MonoBehaviour
{
    private GoogleGeminiAPI geminiAPI;
    private string[] options = { "���Y", "��", "�ŤM" };
    private int ai1Wins = 0, ai2Wins = 0;
    private int totalRounds = 5;

    void Start()
    {
        geminiAPI = gameObject.AddComponent<GoogleGeminiAPI>();
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        for (int round = 1; round <= totalRounds; round++)
        {
            Debug.Log($"\n�� {round} ���}�l�G");

            string ai1Choice = GetRandomChoice();
            string ai2Choice = GetRandomChoice();

            Debug.Log($"AI 1 ��ܤF�G{ai1Choice}");
            Debug.Log($"AI 2 ��ܤF�G{ai2Choice}");

            int result = DetermineWinner(ai1Choice, ai2Choice);
            if (result == 1) ai1Wins++;
            else if (result == 2) ai2Wins++;

            string roundResult = result == 0 ? "����" : result == 1 ? "AI 1 Ĺ�F" : "AI 2 Ĺ�F";
            Debug.Log($"���G�G{roundResult}");

            // AI ���
            string prompt = $"�b�� {round} �����AAI 1 ��ܤF {ai1Choice}�AAI 2 ��ܤF {ai2Choice}�C���G�O�G{roundResult}�C�м��� AI ��������ܡC";
            yield return StartCoroutine(geminiAPI.GenerateContent(prompt, response =>
            {
                Debug.Log($"AI ��������ѡG\n{response}");
            }));
        }

        // �`�����
        string finalPrompt = $"AI 1 Ĺ�F {ai1Wins} ���AAI 2 Ĺ�F {ai2Wins} ���C�м��� AI ��������ܡA�Q�ץL�̪���{�C";
        yield return StartCoroutine(geminiAPI.GenerateContent(finalPrompt, response =>
        {
            Debug.Log($"�C�������I\n{response}");
        }));
    }

    string GetRandomChoice()
    {
        return options[Random.Range(0, options.Length)];
    }

    int DetermineWinner(string choice1, string choice2)
    {
        if (choice1 == choice2) return 0;
        if ((choice1 == "���Y" && choice2 == "�ŤM") ||
            (choice1 == "��" && choice2 == "���Y") ||
            (choice1 == "�ŤM" && choice2 == "��")) return 1;
        return 2;
    }
}
