using UnityEngine;
using System.Collections;

public class ThomasLeapTrainer : MonoBehaviour
{
    private bool isTraining = false;
    public LeapTrainer trainer;

    // Use this for initialization
    void Start () {
      //  if (trainer = null)
            //trainer = new LeapTrainer();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire3"))
            startTraining();

        if (Input.GetButtonDown("Fire2"))
        {
            string s = trainer.toJSON("TestGest");
            Debug.Log(s);
        }
            
        if (trainer != null)
        {

          //  Debug.Log("Trainer not null");
            trainer.OnGestureRecognized += (name, value, allHits) => {
                Debug.Log("Gesture recognized");
            };
         }
        else
        {
           // Debug.Log("Trainer is null");
        }
    }

    void startTraining()
    {
        if(isTraining)
        {
            return;
        }
        isTraining = true;
        Debug.Log("Button Pressed");
        if (trainer == null)
            Debug.Log("Trainer is Null!");
        else
        {
            trainer.Create("TestGest", false);
            Debug.Log("Gesture Created");
            
        }
        isTraining = false;
    }
}