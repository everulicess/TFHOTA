using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField, Range(1, 10)]
    private float speed = 2f;    

    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(horizontalMovement, 0, 0);
    }
}
