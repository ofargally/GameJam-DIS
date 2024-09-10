using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//TODO: add an animate method in the playerController script to flipbook animate the characters
public class AnimationController {
    private Dictionary<StateController.PlayerState, Sprite[]> animationAtlas;

    private Sprite[] idleAnimation;
    private Sprite[] aimAnimation;
    private Sprite[] fireAnimation;
    private Sprite[] hitAnimation;

    public AnimationController(Sprite[] idle, Sprite[] aim, Sprite[] fire, Sprite[] hit) {
        idleAnimation = idle;
        aimAnimation = aim;
        fireAnimation = fire;
        hitAnimation = hit;

        animationAtlas = new Dictionary<StateController.PlayerState, Sprite[]>();
        animationAtlas.Add(StateController.PlayerState.Idle, idleAnimation);
        animationAtlas.Add(StateController.PlayerState.Aim, aimAnimation);
        animationAtlas.Add(StateController.PlayerState.Fire, fireAnimation);
        animationAtlas.Add(StateController.PlayerState.Hit, hitAnimation);
    }
    public Dictionary<StateController.PlayerState, Sprite[]> GetAnimationAtlas() {
        return animationAtlas;
    }
}