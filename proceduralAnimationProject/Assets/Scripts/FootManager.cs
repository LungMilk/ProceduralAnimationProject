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
        foreach (SteppingLogic foot in feet)
        {
            if (foot.RequestStep())
            {
                foot.stepDirection = charState.direction;
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
