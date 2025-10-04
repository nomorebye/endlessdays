using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScreenController : MonoBehaviour
{
    [Header("UI")]
    public Slider progress;          // 진행바
    public TMP_Text tip;             // "로딩 중…" 또는 팁 문구

    [Header("Flow")]
    public string nextSceneName = "MainScreen";  // 다음에 갈 씬 이름
    public float minLoadTime = 1.2f;             // 최소 대기(UX용)

    // 간단 팁 문구들 (원하면 Inspector에서 수정 가능)
    public string[] tips = new string[] {
        "로딩 중…",
        "팁: 선택은 관계에 영향을 줍니다.",
        "팁: 진행도는 메인에서 확인할 수 있어요."
    };

    IEnumerator Start()
    {
        // 1) 팁 문구 세팅
        if (tip != null && tips != null && tips.Length > 0)
            tip.text = tips[Random.Range(0, tips.Length)];

        // 2) 씬 비동기 로드 시작 (바로 전환은 막기)
        var op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;

        float t = 0f;
        if (progress != null) progress.value = 0f;

        // 3) 로딩 진행 표시
        while (!op.isDone)
        {
            // Unity 규칙: op.progress는 0~0.9까지가 로딩, 0.9에서 대기
            float target = Mathf.Clamp01(op.progress / 0.9f);

            if (progress != null)
                progress.value = Mathf.MoveTowards(progress.value, target, Time.deltaTime * 0.6f);

            t += Time.deltaTime;

            // 실제 로딩(0.9 도달) + 최소 대기 시간 충족 시 종료
            if (op.progress >= 0.9f && t >= minLoadTime)
                break;

            yield return null;
        }

        // 4) 마지막 남은 진행바 1.0까지 부드럽게 채우기
        if (progress != null)
        {
            while (progress.value < 1f)
            {
                progress.value = Mathf.MoveTowards(progress.value, 1f, Time.deltaTime * 1.2f);
                yield return null;
            }
        }

        // 5) 씬 전환 허용
        op.allowSceneActivation = true;
    }
}
