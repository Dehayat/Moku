using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject[] ticks;


    public void SetHealth(int health)
    {
        for (int i = 0; i < ticks.Length; i++)
        {
            if (i < health)
            {
                ticks[i].SetActive(true);
            }
            else
            {
                ticks[i].SetActive(false);
            }
        }
    }
}
