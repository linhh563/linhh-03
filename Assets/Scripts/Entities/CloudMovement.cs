using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float startPoint;
    [SerializeField] private float endPoint;

    private void Update() {
        Move();
    }

    private  void Move()
    {
        if (transform.position.x >= endPoint)
        {
            transform.position = new Vector3(startPoint, Random.Range(1.2f, 2.9f));
            return;
        }

        float posX = transform.position.x + Time.deltaTime * speed;
        transform.position = new Vector3(posX, transform.position.y);
    }
}
