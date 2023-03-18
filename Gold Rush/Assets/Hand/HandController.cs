using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private GoldController goldController;

    private Vector3 initPositionHand;

    private int angle = 140;

    public int rotateSpeed = 1;

    private int launchAndBackSpeed = 5;

    public int slowSpeed = 0;

    public int step = 1;

    // MONEY
    [HideInInspector]
    public int money = 0;

    [SerializeField]
    private TMP_Text moneyToString;

    public int targetMoney = 0;

    [SerializeField]
    private TMP_Text targetMoneyToString;
    //

    // TIME
    public float minute = 2f;

    public float second = 30f;

    [SerializeField]
    private TMP_Text timeToString;

    bool stopTime = false;
    //

    // BOARD GAMEOVER
    [SerializeField]
    private GameObject boardGameOver;
    //

    // ANIMATOR MACHINE
    [SerializeField]
    private MachineController machineController;
    //


    private void Awake()
    {
        initPositionHand = transform.position;

        // SET TARGET MONEY
        targetMoneyToString.text = "Target Money: " + targetMoney;
        //
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, new Vector3(-2.5f,2.5f,-2));
        GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y, -2));

        switch(step)
        {
            case 1:
                angle += rotateSpeed;

                if (angle > 270 || angle < 90)
                {
                    rotateSpeed *= -1;
                }
                machineController.GetComponent<Animator>().Play("Stand");
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case 2:
                machineController.GetComponent<Animator>().Play("Rush");
                if (transform.position.y < -4.72 || transform.position.x < -10.4 || transform.position.x > 10.4)
                {
                    step = 3;
                }
                slowSpeed = 0;
                Launch(Time.deltaTime);
                break;
            case 3:
                machineController.GetComponent<Animator>().Play("Rush");
                if (transform.position.y > initPositionHand.y)
                {
                    moneyToString.text = "Money: " + money;
                    step = 1;
                }
                if (goldController != null)
                    slowSpeed = goldController.slowSpeed;
                Back(Time.deltaTime);
                break;
        }

        if (Input.GetKey(KeyCode.Space) && step != 3)
        {
            step = 2;
        }

        // COUNT TIME
        if (stopTime == false)
        {
            second -= Time.deltaTime;
            if (second < 0)
            {
                second = 59;
                minute -= 1;
            }
        }
        
        if (minute < 0)
        {
            timeToString.text = "TIME UP !!!";
            stopTime = true;
            boardGameOver.SetActive(true);
            gameObject.GetComponent<HandController>().enabled = false;
        }
        else
        {
            timeToString.text = "Time: " + ((int)minute).ToString("00") + ":" + ((int)second).ToString("00");

        }
        //

    }

    private void Launch(float deltaTime)
    {
        transform.Translate(Vector3.up * (launchAndBackSpeed - slowSpeed) * deltaTime);
    }

    private void Back(float deltaTime)
    {
        transform.Translate(Vector3.down * (launchAndBackSpeed - slowSpeed) * deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        step = 3;
        goldController = collision.gameObject.GetComponent<GoldController>();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
