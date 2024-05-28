using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class AutoWork : MonoBehaviour
{
    public static long autoMoneyIncreaseAmount = 10;
    public static long autoIncreasePrice = 1000;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Work());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Work()
    {
        while (true)
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.money += autoMoneyIncreaseAmount;
            yield return new WaitForSeconds(1);
        }
    }

    public void Reset()
    {
        autoMoneyIncreaseAmount = 10;
        autoIncreasePrice = 1000;
    }
}
