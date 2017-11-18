using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonScript : MonoBehaviour
{
    private float shakingDuration = 0;
    public int shakingMotions = 1;
    public int shakingThreshold = 3;

    public static float LowPassKernelWidthInSeconds = 0.4f;
// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).

    public static float AccelerometerUpdateInterval = 1 / 60;
    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;

    private Vector3 lowPassValue = Vector3.zero; // should be initialized with 1st sample

    private Vector3  PhoneAcc;
    private Vector3 PhoneDeltaAcc;

    private AudioSource sound;

	void Start ()
	{
	    sound = GetComponent<AudioSource>();
	}

    Vector3 LowPassFilter(Vector3 newSample) {
        lowPassValue = Vector3.Lerp(lowPassValue, newSample, LowPassFilterFactor);
        return lowPassValue;
    }

    bool Shaked()
    {
        return Mathf.Abs(PhoneDeltaAcc.y) >= .2;
    }

    void FixedUpdate () {

        PhoneAcc = Input.acceleration;
        PhoneDeltaAcc = PhoneAcc - LowPassFilter(PhoneAcc);

        if (Shaked())
        {
            Debug.Log("Shake !");
            shakingMotions += 1;
        }
        if (shakingMotions >= shakingThreshold)
        {
            sound.Play(0);
            shakingMotions = 0;
        }
    }

}
