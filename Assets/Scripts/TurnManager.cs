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
	SwapPlayer();
    }
    private void SwapPlayer() 
    {
        if(isPlayer1Turn){
            Player_1.Instance.EndTurn();
            Player_2.Instance.StartTurn();
        }else{
            Player_1.Instance.StartTurn();
            Player_2.Instance.EndTurn();
        }
        isPlayer1Turn = !isPlayer1Turn;
    }
}
