using System;
using System.Collections;
using System.Collections.Generic;
using ARTutorial.ShootGame;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private SpouterController spouterController;
    private float lifeTime = 1f;
    private bool isInitialed = false;

    public void Init(SpouterController spouter, float LifeTime = 0.5f)
    {
        spouterController = spouter;
        lifeTime = LifeTime;
        isInitialed = true;
    }

    public void OnHited()
    {
        spouterController.OnHited(this);
        isInitialed = false;
    }

    private void Update()
    {
        if (!isInitialed)
        {
            return;
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            isInitialed = false;
            if (spouterController != null)
            {
                spouterController.ResponseTarget(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}