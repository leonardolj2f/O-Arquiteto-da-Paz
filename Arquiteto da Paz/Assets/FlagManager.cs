using System.Collections;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject flagPrefab; // Prefab da bandeira
    public Vector3[] positions; // Posições onde as bandeiras vão aparecer
    public float fadeDuration = 1.0f; // Duração do fade in/out
    public float displayTime = 2.0f; // Tempo que a bandeira ficará visível

    void Start()
    {
        foreach (Vector3 pos in positions)
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
        spriteRenderer.color = new Color(1, 1, 1, 1); // Garante que fique totalmente visível

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
