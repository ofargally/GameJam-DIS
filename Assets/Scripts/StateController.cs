using UnityEngine;

//TODO: add StateController object to PlayerController script
public class StateController: MonoBehaviour {
    public enum PlayerState {
        Idle,
        Aim,
        Fire,
        Hit
    }

    private PlayerState state;

    public StateController(PlayerState newState) {
        state = newState;
    }

    public PlayerState GetPlayerState() {
        return state;
    }
    public void TransitionToState(float frameTimer, int frameIndex, PlayerState newState) {
        frameTimer = 0;
        frameIndex = 0;
        state = newState;
    }

    
}