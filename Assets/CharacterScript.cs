using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterScript : MonoBehaviour
{
    Vector3 newPosition;
    public float speed = 3;
    void Start()
    {
        newPosition = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                //transform.position = newPosition;
                
            }
        }
        if (newPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
    }
}
