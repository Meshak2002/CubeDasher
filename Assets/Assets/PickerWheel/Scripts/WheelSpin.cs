using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpin : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;
    public RectTransform spinningWheel;

    [SerializeField]
    private Text wintext;
    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }


    public void spin()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(Spin());
        }
    }
    private IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;
        for (int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.2f;
            if (i > Mathf.RoundToInt(randomValue * 0.85f))
                timeInterval = 0.4f;
            yield return new WaitForSeconds(timeInterval);
        }
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0) transform.Rotate(0, 0, 22.5f);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (finalAngle)
        {
            case 0:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                //  wintext.text = "You Win 0";
                break;
            case 45:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                //  wintext.text = "You Win 45";
                break;
            case 135:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                // wintext.text = "You Win 135";
                break;
            case 180:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                // wintext.text = "You Win 180";
                break;
            case 225:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                // wintext.text = "You Win 225";
                break;
            case 270:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                // wintext.text = "You Win 270";
                break;
            case 315:
                wintext.text = spinningWheel.transform.rotation.eulerAngles.z.ToString("0");
                //  wintext.text = "You Win 315";
                break;
        }
        coroutineAllowed = true;

    }
}