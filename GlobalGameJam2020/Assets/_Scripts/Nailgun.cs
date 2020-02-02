using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nailgun : MonoBehaviour
{
    Interaction interaction;
    Animator animator;
    Animation nail;

    public ParticleSystem nailEffect;

    private void Awake()
    {
        interaction = GetComponent<Interaction>();
        animator = Camera.main.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (interaction.currentInteractable != null)
        {
            if (Input.GetMouseButton(1) && interaction.currentInteractable.itemPicked)
            {
                animator.SetTrigger("ShootNail");
                Instantiate(nailEffect, interaction.currentInteractable.gameObject.transform.position, interaction.currentInteractable.gameObject.transform.rotation);
                interaction.currentInteractable.FreezeItem();
                interaction.holdingObject = false;
                interaction.currentInteractable.itemPicked = false;
            }
        }
    }
}
