using Suburb;
using TMPro;
using UnityEngine;

public class FeedbackPressToInteract : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ControlInworldAudio controlInworldAudio;
    [SerializeField] PlayerClueInteraction playerClueInteraction;
    [SerializeField] GameObject textPrefab;
    [Space]
    [SerializeField] string openString = "Press E to open";
    [SerializeField] string talkString = "Press E to talk";
    [SerializeField] string stopTalkString = "Press E to stop talking";
    [SerializeField] string clueString = "Press E to interact";

    GameObject openText;
    GameObject talkText;
    GameObject clueText;
    Vector3 position;

    private void Awake()
    {
        //instantiate prefabs
        openText = Instantiate(textPrefab);
        openText.name = "Open - " + openText.name;
        talkText = Instantiate(textPrefab);
        talkText.name = "Talk - " + talkText.name;
        clueText = Instantiate(textPrefab);
        clueText.name = "Interact Clue - " + clueText.name;
    }

    private void Update()
    {
        CheckSimpleOpenClose();
        CheckCanTalkWithCharacters();
        CheckCanTakeCLue();
    }

    void CheckSimpleOpenClose()
    {
        //if hit something that can open/close
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out RaycastHit hit, 2.3f))
        {
            if (hit.collider.gameObject.GetComponent<SimpleOpenClose>())
            {
                //show Open with E
                ShowText(hit, openString, openText);
                return;
            }
        }

        //else remove
        HideText(openText);
    }

    void CheckCanTalkWithCharacters()
    {
        //if can interact
        if (controlInworldAudio && controlInworldAudio.CanInteract)
        {
            //show Talk with E or Stop Talk with E
            ShowText(controlInworldAudio.Hit, controlInworldAudio.IsTalking ? stopTalkString : talkString, talkText);
            return;
        }

        //else remove
        HideText(talkText);
    }

    void CheckCanTakeCLue()
    {
        //if can interact
        if (playerClueInteraction && playerClueInteraction.CanInteract)
        {
            //show Interact with E
            ShowText(playerClueInteraction.Hit, clueString, clueText);
            return;
        }

        //else remove
        HideText(clueText);
    }

    void ShowText(RaycastHit hit, string text, GameObject textInstance)
    {
        textInstance.GetComponentInChildren<TMP_Text>().text = text;
        position = hit.point + (cam.transform.position - hit.point) * 0.1f;
        textInstance.transform.position = position;
        textInstance.transform.rotation = Quaternion.LookRotation(position - cam.transform.position, Vector3.up);
        textInstance.SetActive(true);
    }

    void HideText(GameObject textInstance)
    {
        textInstance.SetActive(false);
    }
}
