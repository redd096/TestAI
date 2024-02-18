using Suburb;
using UnityEngine;

public class FeedbackPressToInteract : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ControlInworldAudio controlInworldAudio;
    RaycastHit hit;

    private void Update()
    {
        CheckSimpleOpenClose();
        CheckCanTalkWithCharacters();
    }

    void CheckSimpleOpenClose()
    {
        //if hit something that can open/close
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 2.3f))
        {
            if (hit.collider.gameObject.GetComponent<SimpleOpenClose>())
            {
                //show Open with E
                return;
            }
        }

        //else remove
    }

    void CheckCanTalkWithCharacters()
    {
        //if can interact
        if (controlInworldAudio && controlInworldAudio.CanInteract)
        {
            //show Talk with E
            //or Stop Talk with E
        }

        //else remove
    }
}
