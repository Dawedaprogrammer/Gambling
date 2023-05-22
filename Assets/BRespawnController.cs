using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRespawnController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ymove = new Vector2(0, -0.002f);

        transform.Translate(ymove);

        if(transform.position.y < -26)
        {
            Instantiate(gameObject, new Vector3(0, 23, 1), Quaternion.identity);                        
            Destroy(this.gameObject);
        }
    }
}
