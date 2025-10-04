using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashStart : MonoBehaviour
{
    [Header("UI")]
    public Button btnGuest;
    public Button btnGoogle;
    public Button btnKakao;
    public TMP_Text statusText;         // 선택(없어도 됨)
    public Image fade;                  // Splash에서 만든 검은 Image

    [Header("Flow")]
    public string nextSceneName = "MainScreen"; // 이미 있는 씬 이름으로!

    bool busy = false;

    void Awake()
    {
        if (btnGuest) btnGuest.onClick.AddListener(OnGuest);
        if (btnGoogle) btnGoogle.onClick.AddListener(() => OnComingSoon("Google"));
        if (btnKakao) btnKakao.onClick.AddListener(() => OnComingSoon("Kakao"));
    }

    void OnComingSoon(string which)
    {
        if (statusText) statusText.text = $"{which} 로그인은 곧 지원됩니다.";
    }

    void OnGuest()
    {
        if (busy) return;
        StartCoroutine(GoNext());
    }

    IEnumerator GoNext()
    {
        busy = true;
        if (statusText) statusText.text = "시작 중…";
        // 페이드 아웃 (검은幕 올리기)
        if (fade != null)
        {
            var c = fade.color;
            for (float t = 0; t < 0.8f; t += Time.deltaTime)
            {
                c.a = Mathf.Lerp(0f, 1f, t / 0.8f);
                fade.color = c;
                yield return null;
            }
            c.a = 1f; fade.color = c;
        }
        var op = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        while (!op.isDone) yield return null;
    }
}
