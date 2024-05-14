using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    private Transform tr;
    private Vector2 firstTouch;
    public float limitMinY;
    public float limitMaxY;
    public float dragSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject() == false)
        {
            Move();
        }
    }
    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(firstTouch, currentTouch) < 0.4f) 
            {
                if (firstTouch.y < currentTouch.y)
                {
                    if (tr.position.y > limitMinY)
                    {
                        tr.Translate(Vector2.down * dragSpeed);
                    }
                }
                else if (firstTouch.y > currentTouch.y)
                {
                    if (tr.position.y < limitMaxY)
                    {
                        tr.Translate(Vector2.up * dragSpeed);
                    }
                }
            }
            
        }
    }
}
