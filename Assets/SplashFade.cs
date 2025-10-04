using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashFade : MonoBehaviour
{
    public Image fade;
    public float duration = 1.0f;

    IEnumerator Start()
    {
        if (fade == null) fade = GetComponent<Image>();
        var c = fade.color; c.a = 1f; fade.color = c;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1f, 0f, t / duration);
            fade.color = c;
            yield return null;
        }
        // ���κ�
        c.a = 0f;
        fade.color = c;
        fade.raycastTarget = false;      // �� �߰� (Image�� Ŭ�� ���� �ʰ�)
        fade.gameObject.SetActive(false); // �� ����: �ƿ� ����

    }
}
