using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject[] checkPoints;

    private void Awake()
    {
        checkPoints[0].SetActive(true);
    }

    public void Activate(int checkPointNum)
    {
        try
        {
            checkPoints[checkPointNum].SetActive(true);
        }
        catch
        {
            ;
        }
        
    }
}
