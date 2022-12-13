using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitBehaviour : MonoBehaviour
{
    private int digit_value;

    public enum DigitPosition {First, Second, Third, Fourth};
    private DigitPosition digit_position;
    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "First":
                digit_position = DigitPosition.First;
                break;
            case "Second":
                digit_position = DigitPosition.Second;
                break;
            case "Third":
                digit_position = DigitPosition.Third;
                break;
            case "Fourth":
                digit_position = DigitPosition.Fourth;
                break;
        }
        digit_value = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public DigitPosition getDigitPosition()
    {
        return digit_position;
    }

    public int getDigitValue()
    {
        return digit_value;
    }
}
