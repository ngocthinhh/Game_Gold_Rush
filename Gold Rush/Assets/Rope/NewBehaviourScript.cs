using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int angle;

    private int speed = 5;

    private int rotateSpeed = 2;

    private bool launch = false;
    private bool back = false;

    private int step = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(step)
        {
            case 1:
                launch = false;
                back = false;
                angle += rotateSpeed;

                if (angle > 80 || angle < -80)
                {
                    rotateSpeed *= -1;
                }

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case 2:
                Launch(Time.deltaTime);
                if (transform.position.y < -4.5)
                {
                    step = 3;
                }
                //launch = true;
                //back = false;
                break;
            case 3:
                Back(Time.deltaTime);
                if (transform.position.y > 0.5)
                {
                    step = 1;
                }
                //launch = false;
                //back = true;
                break;
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            step = 2;
        }

        if (Input.GetKey(KeyCode.W))
        {
            step = 3;
        }

        if (launch == true)
        {
            Launch(Time.deltaTime);
            if (transform.position.y < -4.5)
            {
                step = 3;
            }
        }

        if (back == true)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y > 0.5)
            {
                step = 1;
            }
        }

    }

    private void Launch(float timedelta)
    {
        transform.Translate(Vector3.down * speed * timedelta);
    }

    private void Back(float timedelta)
    {
        transform.Translate(Vector3.up * speed * timedelta);
    }
}
