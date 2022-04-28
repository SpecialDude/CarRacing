using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    CarMovement car;
    [SerializeField]
    private int checkPointNum;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            car = other.gameObject.GetComponent<CarMovement>();

            print(car);

            if (car.LastCheckpoint + 1 == checkPointNum)
            {
                car.LastCheckpoint = checkPointNum;
                this.gameObject.SetActive(false);
                GetComponentInParent<Activator>().Activate(checkPointNum + 1);

                //GameObject.Find("Activator").GetComponent<Activator>().Activate(checkPointNum + 1);
            }
        }
    }
}
