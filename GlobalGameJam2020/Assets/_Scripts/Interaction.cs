using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public InteractableObject currentInteractable;
    private Camera cam;
    public bool holdingObject = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!holdingObject)
        {
            CheckForInteractables();
        }

        if (Input.GetMouseButtonDown(0) && !holdingObject)
        {
            if (currentInteractable == null)
                return;
            currentInteractable.PickItem();
            holdingObject = true;
        }

        else if (Input.GetMouseButtonDown(0) && holdingObject)
        {
            if (currentInteractable == null)
                return;
            currentInteractable.DropItem();
            holdingObject = false;
        }
    }

    private void CheckForInteractables()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var interactable = hit.collider.GetComponent<InteractableObject>();

            if (currentInteractable == interactable)
                return;

            else if (interactable == null)
            {
                currentInteractable?.ObjectHighlight(false);
                currentInteractable = null;
                return;
            }

            currentInteractable = interactable;
            currentInteractable.ObjectHighlight(true);
        }
    }
}
