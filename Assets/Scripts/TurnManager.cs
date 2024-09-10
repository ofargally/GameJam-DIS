using System.Collections;
using UnityEngine;

public class TurnManager: MonoBehaviour {
    [SerializeField] private float waitTime;

    public static bool isPlayer1Turn;

    private void Awake() {
        Projectile.EOnProjectileHits += () => StartCoroutine(OnProjectileHit());
        isPlayer1Turn = true;
    }
    private IEnumerator OnProjectileHit() {
        yield return new WaitForSeconds(waitTime);

    }
    private void SwapPlayer() {
        isPlayer1Turn = !isPlayer1Turn;
    }
}