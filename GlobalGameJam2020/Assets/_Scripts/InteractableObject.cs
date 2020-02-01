using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    public float RotationSpeed = 1f;

    private Rigidbody rigidBody;
    private Transform objectPlacement;
    private GameObject player;
    public bool itemPicked;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        objectPlacement = GameObject.Find("ObjectPlacement").transform;
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (itemPicked)
        {
            //Right
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.down * RotationSpeed, Space.World);
            }

            //Left
            else if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up * RotationSpeed, Space.World);
            }

            //Up
            else if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(player.transform.right * RotationSpeed, Space.World);
            }

            //Down
            else if (Input.GetKey(KeyCode.F))
            {
                transform.Rotate(-player.transform.right * RotationSpeed, Space.World);
            }

            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
    public void ObjectHighlight(bool isHighlighted)
    {
        if(isHighlighted)
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = Color.red;
    }

    public void PickItem()
    {
        rigidBody.isKinematic = false;
        itemPicked = true;
        GetComponent<Renderer>().material.color = Color.blue;

        transform.SetParent(objectPlacement);
        transform.position = objectPlacement.position;
        rigidBody.useGravity = false;
    }

    public void DropItem()
    {
        itemPicked = false;
        GetComponent<Renderer>().material.color = Color.red;

        transform.SetParent(null);

        rigidBody.useGravity = true;
        rigidBody.detectCollisions = true;
    }

    public void FreezeItem()
    {
        itemPicked = false;
        rigidBody.isKinematic = true;
        GetComponent<Renderer>().material.color = Color.black;

        transform.SetParent(null);

        rigidBody.useGravity = false;
        rigidBody.detectCollisions = true;
    }

    public void OnCollisionEnter(Collision itemCollision)
    {
        
    }
}
