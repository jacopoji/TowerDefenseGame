using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{

    public Image fadeImage;
    public AnimationCurve curve;

    IEnumerator FadeInEffect()
    {
        float t=1f;
        while (t > 0)
        {
            fadeImage.color = new Vector4(0f, 0f, 0f, curve.Evaluate(t));
            t -= Time.deltaTime;
            yield return 0;
        }
    }

    IEnumerator FadeOutEffect(int scene)
    {
        float t = 0f;
        while (t < 1f)
        {
            fadeImage.color = new Vector4(0f, 0f, 0f, curve.Evaluate(t));
            t += Time.deltaTime;
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeInEffect");
    }

    public void PlayFadeOut(int scene)
    {
        StartCoroutine(FadeOutEffect(scene));
    }
    
}
