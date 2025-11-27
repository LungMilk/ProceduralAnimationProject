using UnityEngine;
using System.Collections.Generic;
public class FootManager : MonoBehaviour
{
    public CharacterState charState;
    public List<SteppingLogic> feet;
    public List<SteppingLogic> activeFeet;

    public float globalStepDistance;
    public float globalStepHeight;
    public float globalStepDuration;
    public float globalSpeed;
    //having it be a list could mean that I can have lots of feet??
    //how would I differentiate, maybe they have a bool or something to communicate their orientation?
    //should I filter then?

    //I would like to display the active foot
    public string activeFoot;
    private void Start()
    {
        SetupFeetValues();
    }
    private void SetupFeetValues()
    {
        foreach (SteppingLogic foot in feet)
        {
            foot.stepDistance = globalStepDistance;
            foot.stepHeight = globalStepHeight;
            foot.stepDuration = globalStepDuration;
            foot.speed = globalSpeed;
        }
    }

    private void Update()
    {
        //each frame update the direction for the feet and their speed.
        foreach (SteppingLogic foot in feet)
        {
            foot.stepDirection = charState.direction;
        }

        //now the foot manager needs to tell the feet when to step, as well as know when.
        //Check the feets state and if that foots state canStep and no other are stepping

        foreach (SteppingLogic foot in feet)
        {
            //if this foot can step let it step
            if (!foot.stepping)
            {
                foot.toldToStep = true;
                break;
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
