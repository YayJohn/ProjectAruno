using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public ShotScript shotScript;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shotScript.Shooting();
        }
    }
}
