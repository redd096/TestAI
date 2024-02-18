using Inworld;
using System.Collections.Generic;
using UnityEngine;

public class InworldTriggersManager : MonoBehaviour
{
    public static InworldTriggersManager Instance;

    [SerializeField] InworldCharacter inworldCharacter;
    [SerializeField] EndGame endGame;

    [Header("The goals we want to end the game - and the trigger to end the game")]
    [SerializeField] List<string> necessaryGoals = new List<string>();
    [SerializeField] string triggerToEndGame;

    List<string> completedGoals = new List<string>();   //check completed every goal
    bool sendEndGameTrigger;                            //wait ai stops to talk, then send end trigger
    bool startEndGameAnimation;                         //then wait again ai stops to talk, and stop the game

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Send a trigger to character
    /// </summary>
    /// <param name="triggerName"></param>
    public void SendTrigger(string triggerName)
    {
        inworldCharacter.SendTrigger(triggerName);
    }

    /// <summary>
    /// Called when ai starts to talk
    /// </summary>
    public void OnBeginSpeaking()
    {
    }

    /// <summary>
    /// Called when ai stops to talk
    /// </summary>
    public void OnEndSpeaking()
    {
        //if completed every goal, send end game trigger
        if (sendEndGameTrigger)
        {
            sendEndGameTrigger = false;
            SendTrigger(triggerToEndGame);
        }

        //if received trigger to end game, end it
        if (startEndGameAnimation)
        {
            startEndGameAnimation = false;
            endGame.OnEndGame();
        }
    }

    /// <summary>
    /// This is called both when player or ai talk. PlayerName is the one in UserSettings
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="text"></param>
    public void OnCharacterSpeaks(string characterName, string text)
    {
    }

    /// <summary>
    /// On emotion change, receive the new emotion and the strength in this emotion
    /// </summary>
    /// <param name="strength"></param>
    /// <param name="emotion"></param>
    public void OnEmotionChanged(string strength, string emotion)
    {
    }

    /// <summary>
    /// Called when activate a goal
    /// </summary>
    /// <param name="goalName"></param>
    public void OnGoalCompleted(string goalName)
    {
        //check if this is one of the goal we want
        if (necessaryGoals.Contains(goalName))
        {
            //add to the list and check
            if (completedGoals.Contains(goalName) == false)
            {
                completedGoals.Add(goalName);

                //check if there is still some goals to complete
                foreach (string goal in necessaryGoals)
                {
                    if (completedGoals.Contains(goal) == false)
                        return;
                }

                //else, send end trigger
                sendEndGameTrigger = true;
            }
        }


        //if this is the goal to end game, start end game animation
        if (goalName == triggerToEndGame)
        {
            startEndGameAnimation = true;
        }
    }
}
