using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionTimeScale;

    private float normalTimeScale;
    private float normalFixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        normalTimeScale = Time.timeScale;
        normalFixedDeltaTime = Time.fixedDeltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            startSlowMotion();
        }
        if (Input.GetKeyUp(KeyCode.Q)){
            stopSlowMotion();
        }
    }
    private void startSlowMotion(){
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * slowMotionTimeScale;
    }
    private void stopSlowMotion(){
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime;
    }
}
