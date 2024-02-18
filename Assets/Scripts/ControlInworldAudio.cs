using UnityEngine;

public class ControlInworldAudio : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] PlayerControllerRPMVariant playerControllerRPMVariant;
    [SerializeField] Transform aiTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] float minDistanceToTalk = 3;
    [SerializeField] KeyCode talkKey;

    RaycastHit hit;
    bool canInteract;
    bool canTalk;
    bool isTalking;

    RaycastHit Hit => hit;
    public bool CanInteract => canInteract;

    private void Awake()
    {
        playerControllerRPMVariant.ToggleTalk(isTalking);
    }

    private void Update()
    {
        CheckCanTalk();
        CheckCanInteractToTalk();

        //if press when can interact, toggle talking
        if (Input.GetKeyDown(talkKey))
        {
            if (canInteract && canTalk)
            {
                isTalking = !isTalking;
                playerControllerRPMVariant.ToggleTalk(isTalking);
            }
        }
    }

    void CheckCanInteractToTalk()
    {
        if (canTalk)
        {
            //if hit a character, can interact with it
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, minDistanceToTalk))
            {
                if (hit.transform == aiTransform)
                {
                    canInteract = true;
                    return;
                }
            }
        }

        //if not, can't interact
        canInteract = false;
    }

    void CheckCanTalk()
    {
        //check the distance if can talk
        canTalk = Vector3.Distance(playerTransform.position, aiTransform.position) < minDistanceToTalk;

        //if can't talk, stop talking if was talking
        if (canTalk == false && isTalking)
        {
            isTalking = false;
            playerControllerRPMVariant.ToggleTalk(isTalking);
        }
    }
}
