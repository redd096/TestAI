using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerClueInteraction : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float minDistanceToInteract = 3;
    [SerializeField] KeyCode interactKey;
    [SerializeField] CanvasGroup clueCanvas;
    [SerializeField] TMP_Text clueText;
    [Space]
    [SerializeField] float durationFadeIn = 0.5f;
    [SerializeField] float durationText = 10;
    [SerializeField] float durationFadeOut = 1f;

    RaycastHit hit;
    bool canInteract;
    Coroutine showClueCoroutine;

    public RaycastHit Hit => hit;
    public bool CanInteract => canInteract;

    private void Start()
    {
        clueCanvas.alpha = 0f;
    }

    private void Update()
    {
        CheckCanInteract();

        //if press when can interact, show clue with a coroutine
        if (Input.GetKeyDown(interactKey))
        {
            if (canInteract)
            {
                if (showClueCoroutine != null)
                    StopCoroutine(showClueCoroutine);
                showClueCoroutine = StartCoroutine(ShowClueCoroutine());
            }
        }
    }

    void CheckCanInteract()
    {

        //if hit a clue, can interact with it
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, minDistanceToInteract))
        {
            if (hit.transform.GetComponentInParent<ClueInteractable>())
            {
                canInteract = true;
                return;
            }
        }

        //else, can't interact
        canInteract = false;
    }

    IEnumerator ShowClueCoroutine()
    {
        ClueInteractable clue = hit.transform.GetComponentInParent<ClueInteractable>();
        if (clue == null || string.IsNullOrEmpty(clue.ClueString))
        {
            Debug.LogError("Missing ClueInteractable or string");
            yield break;
        }

        //set text
        clueText.text = clue.ClueString;

        //fade in text
        float delta = 0;
        while (delta < 1)
        {
            delta += Time.deltaTime / durationFadeIn;
            clueCanvas.alpha = delta;
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(durationText);
        //and fade out
        delta = 0;
        while (delta < 1)
        {
            delta += Time.deltaTime / durationFadeOut;
            clueCanvas.alpha = 1 - delta;
            yield return null;
        }
    }
}
