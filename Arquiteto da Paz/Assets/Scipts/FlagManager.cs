using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject redFlagPrefab; // Prefab da bandeira
    public GameObject greenFlagPrefab; // Prefab da bandeira
    public Vector3[] positions; // Posi��es onde as bandeiras v�o aparecer
    public float fadeDuration = 1.0f; // Dura��o do fade in/out
    public float displayTime = 2.0f; // Tempo que a bandeira ficar� vis�vel

    GameInitializer gameInitializer;

    void Start()
    {
        gameInitializer = GetComponent<GameInitializer>();
    }

    public void PlayAnims(List<int> greens, List<int> reds){

        foreach (int p in reds)
        {
            GameObject flag = Instantiate(redFlagPrefab, positions[p], Quaternion.identity);
            StartCoroutine(FadeFlag(flag));
        }
        foreach (int p in greens)
        {
            GameObject flag = Instantiate(greenFlagPrefab, positions[p], Quaternion.identity);
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
        gameInitializer.p1.UpdateColor();
        gameInitializer.p2.UpdateColor();
        gameInitializer.p3.UpdateColor();
        gameInitializer.p4.UpdateColor();
        gameInitializer.p5.UpdateColor();
        gameInitializer.p6.UpdateColor();
        gameInitializer.p7.UpdateColor();
        gameInitializer.p8.UpdateColor();

        spriteRenderer.color = new Color(1, 1, 1, 0); // Garante que fique totalmente transparente
    }
}
