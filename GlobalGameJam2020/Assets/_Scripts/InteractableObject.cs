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
    public bool itemFreezed = false;

    private bool isAttachable = false;
    private GameObject currentCollision;
    private Color defaultColor;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        objectPlacement = GameObject.Find("ObjectPlacement").transform;
        player = GameObject.FindGameObjectWithTag("MainCamera");
        defaultColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        if (itemPicked)
        {
            //rigidBody.MovePosition(objectPlacement.position);

            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
    public void ObjectHighlight(bool isHighlighted)
    {
        if (itemFreezed)
            return;

        if(isHighlighted)
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = defaultColor;
    }

    public void PickItem()
    {
        if (itemFreezed)
            return;

        rigidBody.isKinematic = false;
        itemPicked = true;
        GetComponent<Renderer>().material.color = defaultColor;

        transform.SetParent(objectPlacement);
        rigidBody.useGravity = false;
    }

    public void DropItem()
    {
        if (itemFreezed)
            return;

        itemPicked = false;
        GetComponent<Renderer>().material.color = defaultColor;

        transform.SetParent(null);

        rigidBody.useGravity = true;
        rigidBody.detectCollisions = true;
    }

    public void FreezeItem()
    {
        if (!isAttachable)
        {
            DropItem();
            return;
        }

        itemPicked = false;
        itemFreezed = true;
        rigidBody.isKinematic = true;
        GetComponent<Renderer>().material.color = Color.gray;

        transform.SetParent(null);

        rigidBody.useGravity = false;
        rigidBody.detectCollisions = true;
    }

    public void RotateItem(Vector3 rotationAxis)
    {
        transform.Rotate(rotationAxis * RotationSpeed, Space.World);
    }

    public void OnCollisionStay(Collision itemCollision)
    {
        if (itemCollision.gameObject.transform.IsChildOf(this.transform))
            return;
        currentCollision = itemCollision.gameObject;
        isAttachable = true;
    }

    public void OnCollisionExit(Collision itemCollision)
    {
        currentCollision = itemCollision.gameObject;
        isAttachable = false;
    }
}
