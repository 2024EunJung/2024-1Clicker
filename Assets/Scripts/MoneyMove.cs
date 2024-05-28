using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMove : MonoBehaviour
{
    public Vector2 point;
    Text txt;

    public long feverMoney;

    void Start()
    {
        txt = transform.GetComponentInChildren<Text>();
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        feverMoney = gm.moneyIncreaseAmount * 2;
        if (gm.isFever == false)
            txt.text = "+" + gm.moneyIncreaseAmount.ToString("###,###");
        else if (gm.isFever == true)
            txt.text = "+" + feverMoney.ToString("###,###");

        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.isFever == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * 10f);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.01f);
        }
        else if (gm.isFever == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * 10f);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, 0, sr.color.a - 0.01f);
        }

        txt = transform.GetComponentInChildren<Text>();
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.01f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 0.2f);
    }
}
