using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public bool waiting = true;
    public float speed = 0.5f;

    void Update()
    {
        Wait();
    }

    private void Wait()
    {
        if (waiting)
        {
            transform.position = new Vector3(transform.position.x + 1 * speed * Time.deltaTime, transform.position.y, transform.position.z);
            if (Mathf.Abs(transform.position.x) > 0.75f)
            {
                speed = -speed;
            }
        }
        else transform.position = new Vector3(0, transform.position.y, transform.position.z);
        
    }
}
