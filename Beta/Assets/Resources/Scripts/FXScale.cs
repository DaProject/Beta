using UnityEngine;
using System.Collections;

public class FXScale : MonoBehaviour
{

    public float iniScale;
    public float duration;
    public float finalScale;
    public float currentTime;

    public int startFrames;

    public Transform transform;

    // Use this for initialization
    void Start()
    {

        transform = this.transform;

        currentTime = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (startFrames <= 0)
        {


            if (currentTime <= duration)
            {

				transform.localScale = new Vector3(Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration), 
					Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration),
					Easing.ExpoEaseOut(currentTime, iniScale, (finalScale - iniScale), duration));
                
                

                currentTime++;
            }
        }

        else startFrames--;
    }

}