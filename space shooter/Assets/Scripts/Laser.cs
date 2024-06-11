using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 10f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime* laserSpeed);
        if(transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
