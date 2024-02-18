using System.Collections;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] ControlInworldAudio controlInworldAudio;
    [SerializeField] PlayerControllerRPMVariant playerControllerRPM;
    [Space]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeInDuration = 1;

    Coroutine endGameCoroutine;

    private void Awake()
    {
        canvasGroup.alpha = 0;
    }

    public void OnEndGame()
    {
        //stop ai
        controlInworldAudio.enabled = false;
        playerControllerRPM.ToggleTalk(false);

        //start end game animation
        if (endGameCoroutine == null)
            endGameCoroutine = StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        //fade in
        float delta = 0;
        while (delta < 1)
        {
            delta += Time.deltaTime / fadeInDuration;
            canvasGroup.alpha = delta;
            yield return null;
        }
    }
}
