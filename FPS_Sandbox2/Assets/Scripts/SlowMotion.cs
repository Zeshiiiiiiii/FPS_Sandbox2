using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionTimeScale;

    private float normalTimeScale;
    private float normalFixedDeltaTime;
    public float slomoCooldown = 15;
    private float nextSlomoTime;
    public float forcedSlomo = 10;
    private float forcedSlomoStop;

    public PlayerMovement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        normalTimeScale = Time.timeScale;
        normalFixedDeltaTime = Time.fixedDeltaTime;
        
    }

    // Update is called once per frame
    void Update(){

        if(Time.time > nextSlomoTime){
            if(Input.GetKeyDown(KeyCode.Q)){
                nextSlomoTime = Time.time + slomoCooldown;
                forcedSlomoStop = Time.time + 2; 
            }
        }
        if (Time.time < forcedSlomoStop){
                startSlowMotion();
        }

        if(Time.time > forcedSlomoStop){
            stopSlowMotion();
        }
        Debug.Log("Time: " + Time.time + " \n forced slomo stop: " + forcedSlomoStop);
    }
    private void startSlowMotion(){
        Debug.Log("slomo started");
        movementScript.Movement();
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * slowMotionTimeScale;
    }
    private void stopSlowMotion(){
        Debug.Log("slomo stopped");
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime;
    }
}
