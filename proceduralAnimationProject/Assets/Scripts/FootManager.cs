using UnityEngine;
using System.Collections.Generic;
public class FootManager : MonoBehaviour
{
    public CharacterState charState;
    public List<SteppingLogic> feet;
    public List<SteppingLogic> activeFeet;
    //having it be a list could mean that I can have lots of feet??
    //how would I differentiate, maybe they have a bool or something to communicate their orientation?
    //should I filter then?

    //I would like to display the active foot
    public string activeFoot;
    private void Awake()
    {

    }

    private void Update()
    {
        //each frame update the direction for the feet and their speed.
        foreach (SteppingLogic foot in feet)
        {
            foot.stepDirection = charState.direction;
            foot.forwardMovementPrediction = charState.speed;
        }

        //this for loop will interupt the code if a foot is currently stepping.
        foreach (SteppingLogic foot in feet)
        {
            if (foot.stepping)
                return;
        }

        //if no foot is stepping then we call a new step.
        foreach (SteppingLogic foot in feet)
        {
            if (foot.canStep)
            {
                foot.StartStep();
                return;
            }
        }
    }
    public void DetermineFootToMove()
    {
        //start step
        foreach (SteppingLogic foot in feet)
        {
            if (foot.stepping)
            {

                break;
            }
        }
    }
    public void StartFoot(FootSystem foot)
    {
        //start step
    }
}
