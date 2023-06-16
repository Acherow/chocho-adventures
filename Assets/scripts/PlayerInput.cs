using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 dir;

    Transform stick;

    public PlayerMove pl;

    private void Start()
    {
        stick = transform.GetChild(0);
    }

    void Update()
    {
        DetectTouch();
        stick.position = (Vector2)transform.position + (dir);
        pl.dir = dir;
    }

    void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    transform.position = t.position;
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    dir = t.position - (Vector2)transform.position;
                    dir = Vector2.ClampMagnitude(dir, 50);
                    if (Vector2.Distance(transform.position, t.position) > 150)
                        transform.position = Vector2.Lerp(transform.position, t.position, Time.deltaTime * 10);
                }

                if (t.phase == TouchPhase.Ended)
                {
                    dir = Vector2.zero;
                    transform.position = new Vector2(150, -100);
                }
            }
        }
    }
}
