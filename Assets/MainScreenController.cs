using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainScreenController : MonoBehaviour
{
    [Header("State")]
    public GameState state;

    [Header("TopBar UI")]
    public TMP_Text progressPercentText;   // "30%"
    public TMP_Text progressCountText;     // "25/72"
    public Slider progressSlider;        // 진행도 바

    [Header("Dialogue UI")]
    public TMP_Text speakerText;
    public TMP_Text lineText;

    [TextArea] public string speaker = "마나카";
    [TextArea]
    public string[] lines = {
        "무슨 일이야?",
        "오늘 스케줄 기억하지?",
        "다음엔 내가 고를래.",
        "조금만 더 같이 걸을래?",
        "…괜찮아, 기다릴 수 있어.",
        "자, 출발하자!"
    };
    int idx = 0;

    void Start()
    {
        RefreshTopBar();
        ShowLine();
    }

    void RefreshTopBar()
    {
        if (progressPercentText) progressPercentText.text = $"{state.ProgressPct}%";
        if (progressCountText) progressCountText.text = $"{state.currentStage}/{state.totalStages}";
        if (progressSlider) progressSlider.value = state.Progress01; // 0~1
    }

    void ShowLine()
    {
        if (speakerText) speakerText.text = speaker;
        if (lineText) lineText.text = lines[idx];
    }

    public void OnNext()
    {
        idx = (idx + 1) % lines.Length;
        if (idx == 0) { state.AddAffection(1); } // 한 바퀴 돌면 호감도 +1 정도
        ShowLine();
    }

    // ===== 버튼들 =====
    public void OnLove() { /* 호감도 상세 화면 예정 */ }

    public void OnEvent(int k)
    {
        state.AdvanceStage(1);   // 이벤트 들어가면 스테이지 +1
        RefreshTopBar();
        Debug.Log($"Event {k}");
    }

    public void OnStory()
    {
        state.AdvanceStage(1);   // 스토리 진행 시 +1 (원하면 +2 등 조절)
        RefreshTopBar();
        Debug.Log("Story");
    }

    public void OnGallery() { Debug.Log("Gallery"); }
    public void OnSave() { Debug.Log("Save/Load"); }
    public void OnSetting() { Debug.Log("Setting/Option"); }
}
