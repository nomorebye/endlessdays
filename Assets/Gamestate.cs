using UnityEngine;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject
{
    [Header("Story Progress")]
    [Min(0)] public int currentStage = 0;   // 현재 진행 스테이지(예: 25)
    [Min(1)] public int totalStages = 72;  // 전체 스테이지 수(예: 72)

    [Header("Affection / Coins")]
    [Range(0, 100)] public int affection = 0;
    public int coins = 0;

    // 0~100% 진행률 계산용
    public float Progress01 => Mathf.Clamp01(totalStages <= 0 ? 0f : (float)currentStage / totalStages);
    public int ProgressPct => Mathf.RoundToInt(Progress01 * 100f);

    public void AdvanceStage(int step = 1)
    {
        currentStage = Mathf.Clamp(currentStage + step, 0, totalStages);
    }

    public void AddAffection(int v)
    {
        affection = Mathf.Clamp(affection + v, 0, 100);
    }

    public void AddCoins(int v) => coins = Mathf.Max(0, coins + v);
}