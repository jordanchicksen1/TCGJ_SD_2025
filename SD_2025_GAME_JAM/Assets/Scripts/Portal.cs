using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject ExitPosition;

    [Header("Flash Panels")]
    [SerializeField] CanvasGroup flashPanelPlayer01;
    [SerializeField] CanvasGroup flashPanelPlayer02;

    private void OnTriggerEnter(Collider other)
    {
        
   
        string tag = other.gameObject.tag;

        if (tag == "Player01")
        {
            StartCoroutine(TeleportWithFlash(other.gameObject, flashPanelPlayer01));
            print("Player01 teleported");
        }
        else if (tag == "Player02")
        {
            StartCoroutine(TeleportWithFlash(other.gameObject, flashPanelPlayer02));
            print("Player02 teleported");
        }
    }

    IEnumerator TeleportWithFlash(GameObject player, CanvasGroup flashPanel)
    {
        yield return StartCoroutine(Flash(flashPanel, 1f, 0.2f));

        player.transform.position = ExitPosition.transform.position;

        yield return StartCoroutine(Flash(flashPanel, 0f, 0.2f));
    }

    IEnumerator Flash(CanvasGroup panel, float targetAlpha, float duration)
    {
        float startAlpha = panel.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        panel.alpha = targetAlpha;
    }
}
