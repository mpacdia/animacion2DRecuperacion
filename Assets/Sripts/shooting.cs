using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Animator animatorShoot;
    public bool shootingBool = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootingBool = true;
        }

        animatorShoot.SetBool("shooting", shootingBool);
    }
    
    public void stopShooting()
    {
        shootingBool = false;
    }
}
