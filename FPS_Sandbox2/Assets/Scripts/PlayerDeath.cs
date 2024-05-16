using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Transform player;

    public void Update (){
        if(player.transform.position.y <= -80) {
            FindObjectOfType<gameManager>().EndGame();
        }
        
    }
}
