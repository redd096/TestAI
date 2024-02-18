using UnityEngine;

public class SendTriggerOnHit : MonoBehaviour
{
    [SerializeField] string triggerName;
    [SerializeField] bool useOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        //if hit CharacterController (player), send trigger
        if (other is CharacterController)
        {
            InworldTriggersManager.Instance.SendTrigger(triggerName);

            //if use once, turn off this object
            if (useOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
