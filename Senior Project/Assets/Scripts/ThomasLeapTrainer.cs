using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThomasLeapTrainer : MonoBehaviour {
    LeapRecorder recorder;
    public LeapTrainer trainer;

    public string toLoadGesture = "VRRecording01.json";
    public KeyCode keyToLoad = KeyCode.Y;
    // Use this for initialization
    public int gNumber = 0;
    void Start () {
        if (trainer != null)
        {
            Debug.Log("Something is not wrong!");
            //trainer = new LeapTrainer();

            recorder = new LeapRecorder();

            //loading gesture from file
           // recorder.Load(currentGesture);
           
            //loading frames into trainer
           // trainer.loadFromFrames("STOP_pose", recorder.GetFrames(), true);


            loadGesture("DesktopRecording01.json", "fist", true);
            trainer.OnGestureRecognized += (name, value, allHits) => {
                Debug.Log("Gesture " + name + " recognized");
            };

            trainer.OnGestureUnknown += (Dictionary<string, float> allHits) => {
                Debug.Log("Gesture not recognized");
            };

            // trainer.OnGestureRecognized += TriggerActivated;
            // trainer.OnGestureUnknown    += Trainer_OnGestureUnknown;

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToLoad))
        {
            loadGesture(toLoadGesture,""+gNumber,false);
        }

    }

    private void Trainer_OnGestureUnknown(Dictionary<string, float> allHits)
    {
        Debug.Log("No gesture recognized...");

        // However you can still work with the hits in case I detect some recording are underperforming...
        // For instance: if you have a recording that the best match is a value of 0.5 but the threshold is 0.7 you'll 
        //   never be notified, but you'll still receive the value here.
    }


    private void TriggerActivated(string name, float value, Dictionary<string, float> allHits)
    {
        Debug.Log("Gesture " + name + " recognized");
    }

    public void loadGesture(string path, string gestureName, bool isPose)
    {

        
        recorder.Load(path);

        //loading frames into trainer
      
        trainer.loadFromFrames(gestureName, recorder.GetFrames(), false);
        Debug.Log("loadGesture called, ");
        Debug.Log("Number of frames is: ");
        Debug.Log(recorder.GetFrames().Count);
    }

   
}
