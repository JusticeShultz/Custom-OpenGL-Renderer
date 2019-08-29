﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public GameObject DestroyedState;

    private void OnCollisionEnter(Collision collision)
    {
        //This will destroy the target and spawn the debris object.
        if (collision.gameObject.name.Contains("Arrow"))
        {
            Instantiate(DestroyedState, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
