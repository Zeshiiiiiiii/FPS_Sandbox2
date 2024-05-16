using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public GameObject healthPacket;

    public int howManyEnemys;
    public int howManyHealthPacket;

    private int numberOfEnemys;
    private int numberOfHealthPacket;

    public Image healthBar;

    public PlayerMovement movementScript;

    private float health;

    //

    void Start()
    {
        InvokeRepeating("spawnEnemy", 0f, 2f);
        InvokeRepeating("spawnHealthPacket", 0f, 1f);
    }

    // Update is called once per frame
    void Update(){
        numberOfHealthPacket = GameObject.FindGameObjectsWithTag("healthPack").Count();
        numberOfEnemys = GameObject.FindGameObjectsWithTag("enemy").Count();
        //health = movementScript.returnHealth();
        
        health = movementScript.returnHealth();

        if(health <= 0){
            UnityEditor.EditorApplication.isPlaying = false;
        }

        health = Mathf.Clamp(health, 0, 100);

        healthBar.fillAmount = health/100;

        
    }

    void spawnEnemy(){
        if (numberOfEnemys <= howManyEnemys - 1)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(155, 175), 5, Random.Range(200, 220));
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
    void spawnHealthPacket(){
        if (numberOfHealthPacket <= howManyHealthPacket - 1){
            Vector3 spawnPosition = new Vector3(Random.Range(155, 175), 5, Random.Range(200, 220));
            Instantiate(healthPacket, spawnPosition, Quaternion.identity);
        }
    }
}
