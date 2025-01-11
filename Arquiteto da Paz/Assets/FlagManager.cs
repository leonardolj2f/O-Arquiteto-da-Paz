using System.Collections;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject flagPrefab; // Prefab da bandeira
    public Vector2[] positions; // Posi��es onde as bandeiras v�o aparecer
    public float fadeDuration = 1.0f; // Dura��o do fade in/out
    public float displayTime = 2.0f; // Tempo que a bandeira ficar� vis�vel

    void Start()
    {
        foreach (Vector2 pos in positions)
        {
            GameObject flag = Instantiate(flagPrefab, pos, Quaternion.identity);
            StartCoroutine(FadeFlag(flag));
        }
    }

    IEnumerator FadeFlag(GameObject flag)
    {
        SpriteRenderer spriteRenderer = flag.GetComponent<SpriteRenderer>();

        // Fade In
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1); // Garante que fique totalmente vis�vel

        // Espera antes do fade out
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(1, 1, 1, 0); // Garante que fique totalmente transparente
    }
}
