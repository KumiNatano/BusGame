using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Rotation during roll
public class PlayerManagment : MonoBehaviour
{
    [SerializeField] string potionKey;
    [SerializeField] string dodgeKey;
    public PlayerControlSystem pcsystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dodgeKey))
        {
            dodge(0.1f, 15,0.1f, 1,30);
        }
    }

    public void dodge(float iTime, float dodgeSpeed, float dodgeLength, float cooldown, int staminaCost)
    {
        pcsystem.changeSpeedTimeLimit(dodgeSpeed, dodgeLength,cooldown-dodgeLength);
    }

}
