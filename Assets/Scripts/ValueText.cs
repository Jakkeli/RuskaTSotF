using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueText : MonoBehaviour {

    Text myText;

    public Slider mySlider;

	void Start () {
        myText = GetComponent<Text>();
        ValueChange();

    }

    public void ValueChange() {
        float value = mySlider.value;
        
        myText.text = "" + System.Math.Round(value, 2);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            if (mySlider.value < 0.9f) {
                mySlider.value += 0.05f;
                ValueChange();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            if (mySlider.value > 0.1f) {
                mySlider.value -= 0.05f;
                ValueChange();
            }
        }
    }
}
