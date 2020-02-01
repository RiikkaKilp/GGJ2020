using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nailgun : MonoBehaviour
{
    Interaction interaction;

    private void Awake()
    {
        interaction = GetComponent<Interaction>();
    }

    private void Update()
    {
        if (interaction.currentInteractable != null)
        {
            if (Input.GetMouseButton(1) && interaction.currentInteractable.itemPicked)
            {
                Debug.Log("here!");
                interaction.currentInteractable.FreezeItem();
                interaction.holdingObject = false;
                interaction.currentInteractable.itemPicked = false;
            }
        }
    }
}
