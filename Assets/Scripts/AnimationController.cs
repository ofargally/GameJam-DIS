using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//TODO: add an animate method in the playerController script to flipbook animate the characters
public class AnimationController: PlayerController {
    private Dictionary<StateController.PlayerState, Sprite[]> animationAtlas;

    public AnimationController() {
        animationAtlas = new Dictionary<StateController.PlayerState, Sprite[]>();
        animationAtlas.Add(StateController.PlayerState.Idle, idleAnimation);
        animationAtlas.Add(StateController.PlayerState.Aim, aimAnimation);
        animationAtlas.Add(StateController.PlayerState.Fire, fireAnimation);
        animationAtlas.Add(StateController.PlayerState.Hit, hurtAnimation);
    }
    public Dictionary<StateController.PlayerState, Sprite[]> GetAnimationAtlas() {
        return animationAtlas;
    }

    private IEnumerator playIdleAnimation() {
        int counter = 0;
        while(counter < idleAnimation.Length) {
        }
    }
    private IEnumerator playAimAnimation() {

    }
    private IEnumerator playFireAnimation() {

    }
    private IEnumerator playHitAnimation() {

    }
}