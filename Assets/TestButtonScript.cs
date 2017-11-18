using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonScript : MonoBehaviour
{
    private float shakingDuration = 0;
    public float shakingThreshold = 0.5f;

    public static float LowPassKernelWidthInSeconds = 1;
// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
//    You should be able to use LowPassFilter() function instead of avgSamples().

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

    bool IsShaking()
    {
        return Mathf.Abs(PhoneDeltaAcc.x) >= .2;
    }

    void FixedUpdate () {

        PhoneAcc = Input.acceleration;
        PhoneDeltaAcc = PhoneAcc - LowPassFilter(PhoneAcc);

        if (IsShaking())
        {
            shakingDuration += Time.fixedDeltaTime;
        }
        else if (!IsShaking())
        {
            shakingDuration = 0;
        }
        if (shakingDuration > shakingThreshold)
        {
            sound.Play(0);
            shakingDuration = 0;
        }
    }

}
