using UnityEngine;

public class Player_2 : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void Idle(){
	if (!(TurnManager.isPlayer1Turn)){
		stateController.TransitionToState(StateController.PlayerState.Aim);
    		}
	}

}
