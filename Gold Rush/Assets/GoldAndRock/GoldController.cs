using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    GameObject Hand;

    [SerializeField]
    private HandController handController;

    public int slowSpeed = 0;

    string find = "finding";

    int valueOfMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        Hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameObject.tag)
        {
            case "BigGold":
                slowSpeed = 3;
                valueOfMoney = 100;
                break;
            case "SmallGold":
                slowSpeed = 1;
                valueOfMoney = 50;
                break;
            case "BigRock":
                slowSpeed = 3;
                valueOfMoney = 60;
                break;
            case "SmallRock":
                slowSpeed = 1;
                valueOfMoney = 30;
                break;
        }

        if (find == "finded")
        {
            transform.position = new Vector3(Hand.transform.position.x, Hand.transform.position.y, 0f);
            if (handController.step == 1)
            {
                handController.slowSpeed = 0;
                Destroy(this.gameObject);
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            find = "finded";
            handController = collision.gameObject.GetComponent<HandController>();
            handController.money += valueOfMoney;
        }
    }
}
