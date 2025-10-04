using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScreenController : MonoBehaviour
{
    [Header("UI")]
    public Slider progress;          // �����
    public TMP_Text tip;             // "�ε� �ߡ�" �Ǵ� �� ����

    [Header("Flow")]
    public string nextSceneName = "MainScreen";  // ������ �� �� �̸�
    public float minLoadTime = 1.2f;             // �ּ� ���(UX��)

    // ���� �� ������ (���ϸ� Inspector���� ���� ����)
    public string[] tips = new string[] {
        "�ε� �ߡ�",
        "��: ������ ���迡 ������ �ݴϴ�.",
        "��: ���൵�� ���ο��� Ȯ���� �� �־��."
    };

    IEnumerator Start()
    {
        // 1) �� ���� ����
        if (tip != null && tips != null && tips.Length > 0)
            tip.text = tips[Random.Range(0, tips.Length)];

        // 2) �� �񵿱� �ε� ���� (�ٷ� ��ȯ�� ����)
        var op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;

        float t = 0f;
        if (progress != null) progress.value = 0f;

        // 3) �ε� ���� ǥ��
        while (!op.isDone)
        {
            // Unity ��Ģ: op.progress�� 0~0.9������ �ε�, 0.9���� ���
            float target = Mathf.Clamp01(op.progress / 0.9f);

            if (progress != null)
                progress.value = Mathf.MoveTowards(progress.value, target, Time.deltaTime * 0.6f);

            t += Time.deltaTime;

            // ���� �ε�(0.9 ����) + �ּ� ��� �ð� ���� �� ����
            if (op.progress >= 0.9f && t >= minLoadTime)
                break;

            yield return null;
        }

        // 4) ������ ���� ����� 1.0���� �ε巴�� ä���
        if (progress != null)
        {
            while (progress.value < 1f)
            {
                progress.value = Mathf.MoveTowards(progress.value, 1f, Time.deltaTime * 1.2f);
                yield return null;
            }
        }

        // 5) �� ��ȯ ���
        op.allowSceneActivation = true;
    }
}
