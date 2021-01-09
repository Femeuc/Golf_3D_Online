using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Femeuc.Golf3DOnline
{
    public class ThorwForce : MonoBehaviour
    {
        private Text forwardForceValue, upwardForceValue;
        // Start is called before the first frame update
        void Start()
        {
            forwardForceValue = GameObject.Find("Forward Force Value").GetComponent<Text>();
            upwardForceValue = GameObject.Find("Upward Force Value").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            forwardForceValue.text = (ThrowBall.forwardForceIntensity.value * 10).ToString("0");
            upwardForceValue.text = (ThrowBall.upwardForceIntensity.value * 10).ToString("0");
        }
    }
}