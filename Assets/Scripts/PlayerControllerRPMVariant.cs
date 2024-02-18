using Inworld;
using Inworld.Sample.RPM;

public class PlayerControllerRPMVariant : PlayerControllerRPM
{
    bool m_isTalking;

    public void ToggleTalk(bool isTalking)
    {
        if (isTalking)
        {
            if (m_isTalking == false)
            {
                m_isTalking = true;
                InternalToggleTalk();
                InworldController.Instance.StartAudio();
            }
        }
        else
        {
            if (m_isTalking)
            {
                m_isTalking = false;
                InternalToggleTalk();
                InworldController.Instance.PushAudio();
            }
        }
    }

    protected override void HandlePTT()
    {
        //base.HandlePTT();
    }

    private void InternalToggleTalk()
    {
        m_PushToTalk = m_isTalking == false;
        m_BlockAudioHandling = m_isTalking == false;
        InworldController.CharacterHandler.ManualAudioHandling = m_PushToTalk;
        InworldController.Audio.AutoPush = !m_PushToTalk;
    }
}
