using UnityEngine;
using System.Collections.Generic;

//TODO: attach animationController to player prefab

//TODO: add an animate method in the playerController script to flipbook animate the characters
public class AnimationController: MonoBehaviour {
    public Sprite[] idleAnimation;
    public Sprite[] aimAnimation;
    public Sprite[] fireAnimation;
    public Sprite[] hurtAnimation;
    private Dictionary<StateController.PlayerState, Sprite[]> animationAtlas;

    public AnimationController(Sprite[] idle, Sprite[] aim, Sprite[] fire, Sprite[] hurt) {
        idleAnimation = idle;
        aimAnimation = aim;
        fireAnimation = fire;
        hurtAnimation = hurt;

        animationAtlas = new Dictionary<StateController.PlayerState, Sprite[]>();
        animationAtlas.Add(StateController.PlayerState.Idle, idleAnimation);
        animationAtlas.Add(StateController.PlayerState.Aim, aimAnimation);
        animationAtlas.Add(StateController.PlayerState.Fire, fireAnimation);
        animationAtlas.Add(StateController.PlayerState.Hit, hurtAnimation);
    }
    public Dictionary<StateController.PlayerState, Sprite[]> GetAnimationAtlas() {
        return animationAtlas;
    }
}