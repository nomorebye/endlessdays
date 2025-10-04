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
    public Slider progressSlider;        // ���൵ ��

    [Header("Dialogue UI")]
    public TMP_Text speakerText;
    public TMP_Text lineText;

    [TextArea] public string speaker = "����ī";
    [TextArea]
    public string[] lines = {
        "���� ���̾�?",
        "���� ������ �������?",
        "������ ���� ����.",
        "���ݸ� �� ���� ������?",
        "��������, ��ٸ� �� �־�.",
        "��, �������!"
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
        if (idx == 0) { state.AddAffection(1); } // �� ���� ���� ȣ���� +1 ����
        ShowLine();
    }

    // ===== ��ư�� =====
    public void OnLove() { /* ȣ���� �� ȭ�� ���� */ }

    public void OnEvent(int k)
    {
        state.AdvanceStage(1);   // �̺�Ʈ ���� �������� +1
        RefreshTopBar();
        Debug.Log($"Event {k}");
    }

    public void OnStory()
    {
        state.AdvanceStage(1);   // ���丮 ���� �� +1 (���ϸ� +2 �� ����)
        RefreshTopBar();
        Debug.Log("Story");
    }

    public void OnGallery() { Debug.Log("Gallery"); }
    public void OnSave() { Debug.Log("Save/Load"); }
    public void OnSetting() { Debug.Log("Setting/Option"); }
}
