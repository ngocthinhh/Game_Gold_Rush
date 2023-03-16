using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    //

    // ANIMATOR MACHINE
    [SerializeField]
    private MachineController machineController;
    //

    //
    //public string find = "finding";
    //GameObject gold;
    //

    private void Awake()
    {
        initPositionHand = transform.position;
        //gold = GameObject.Find("Gold");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, new Vector3(-2.5f,2.5f,0));
        GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y, 1));

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
                if (transform.position.y < -4.72 || transform.position.x < -10.4 || transform.position.x > 10.4)
                {
                    step = 3;
                }
                slowSpeed = 0;
                Launch(Time.deltaTime);
                break;
            case 3:
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

        //if (find == "finded")
        //{ 
        //    gold.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 1);
        //}

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
}
