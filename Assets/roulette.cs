using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wheel : MonoBehaviour
{
    private int randomvalue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;

    [SerializeField]
    private Text winText;

    // Use this for initialization
    private void Start()
    {
        coroutineAllowed = true;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0) && coroutineAllowed)
            StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomvalue = Random.Range(20, 30);
        timeInterval = 0.1f;

        for (int i = 0; i < randomvalue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomvalue * 0.5f))
                timeInterval = 0.2f;
            if (i > Mathf.RoundToInt(randomvalue * 0.85f))
                timeInterval = 0.4f;
            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            transform.Rotate(0, 0, 22.5f);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (finalAngle)
        {
            case 0:
                winText.text = "You Luck 5%";
                break;
            case 1:
                winText.text = "You Luck 10%";
                break;
            case 2:
                winText.text = "You Luck 12%";
                break;
            case 3:
                winText.text = "You Luck 30%";
                break;
            case 4:
                winText.text = "You Luck 43%";
                break;
            case 5:
                winText.text = "You Luck 66%";
                break;
            case 6:
                winText.text = "You Luck 70%";
                break;
            case 7:
                winText.text = "You Luck 99%";
                break;
        }

        coroutineAllowed = true;
    }
}
