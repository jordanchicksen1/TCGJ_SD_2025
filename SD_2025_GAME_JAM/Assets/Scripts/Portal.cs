using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject ExitPosition;
    [SerializeField] CanvasGroup flashPanel;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(TeleportWithFlash(other.gameObject));
        }
    }

    IEnumerator TeleportWithFlash(GameObject player)
    {
        // Flash out
        yield return StartCoroutine(Flash(1f, 0.2f));

        // Teleport player
        player.transform.position = ExitPosition.transform.position;

        // Flash in
        yield return StartCoroutine(Flash(0f, 0.2f));
    }

    IEnumerator Flash(float targetAlpha, float duration)
    {
        float startAlpha = flashPanel.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            flashPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        flashPanel.alpha = targetAlpha;
    }
}
