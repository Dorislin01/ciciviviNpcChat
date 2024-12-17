using System.Collections;
using UnityEngine;

public class ciciviviNpcChat : MonoBehaviour
{
    private GoogleGeminiAPI geminiAPI;
    private string[] options = { "石頭", "布", "剪刀" };
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
            Debug.Log($"\n第 {round} 輪開始：");

            string ai1Choice = GetRandomChoice();
            string ai2Choice = GetRandomChoice();

            Debug.Log($"AI 1 選擇了：{ai1Choice}");
            Debug.Log($"AI 2 選擇了：{ai2Choice}");

            int result = DetermineWinner(ai1Choice, ai2Choice);
            if (result == 1) ai1Wins++;
            else if (result == 2) ai2Wins++;

            string roundResult = result == 0 ? "平局" : result == 1 ? "AI 1 贏了" : "AI 2 贏了";
            Debug.Log($"結果：{roundResult}");

            // AI 對話
            string prompt = $"在第 {round} 輪中，AI 1 選擇了 {ai1Choice}，AI 2 選擇了 {ai2Choice}。結果是：{roundResult}。請模擬 AI 之間的對話。";
            yield return StartCoroutine(geminiAPI.GenerateContent(prompt, response =>
            {
                Debug.Log($"AI 之間的聊天：\n{response}");
            }));
        }

        // 總結對話
        string finalPrompt = $"AI 1 贏了 {ai1Wins} 次，AI 2 贏了 {ai2Wins} 次。請模擬 AI 之間的對話，討論他們的表現。";
        yield return StartCoroutine(geminiAPI.GenerateContent(finalPrompt, response =>
        {
            Debug.Log($"遊戲結束！\n{response}");
        }));
    }

    string GetRandomChoice()
    {
        return options[Random.Range(0, options.Length)];
    }

    int DetermineWinner(string choice1, string choice2)
    {
        if (choice1 == choice2) return 0;
        if ((choice1 == "石頭" && choice2 == "剪刀") ||
            (choice1 == "布" && choice2 == "石頭") ||
            (choice1 == "剪刀" && choice2 == "布")) return 1;
        return 2;
    }
}
