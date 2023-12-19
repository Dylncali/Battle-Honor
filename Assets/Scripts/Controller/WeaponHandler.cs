using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Unity.VisualScripting;
public class WeaponHandler : NetworkBehaviour
{   
    [Networked(OnChanged = nameof(OnAtkChanged))]
    public bool isAtking {get; set;}

    public GameObject weaponAim;
    public LayerMask collisionLayers;

    float lastTimeFired = 0f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            if(networkInputData.isAtkingPressed)
                Atk();
                
        }
    }

    void Atk()
    {
        if(Time.time - lastTimeFired <0.15)
        return;

        StartCoroutine(AtkEffect());

        lastTimeFired = Time.time;
        Vector3 rayOrigin = weaponAim.transform.position;
        Vector3 rayDirection = weaponAim.transform.up;
        Runner.LagCompensation.Raycast(rayOrigin, rayDirection, 10, Object.InputAuthority, out var hitInfo, collisionLayers, HitOptions.IncludePhysX);
        float hitDistance = 10f;
        bool isHitOtherPlayer = false;
        Debug.DrawRay(rayOrigin, rayDirection*hitDistance,Color.red);

        if(hitInfo.Distance>0)
        {
            hitDistance = hitInfo.Distance;
        }

        if(hitInfo.Hitbox != null)
        {
            if(Object.HasStateAuthority)
                hitInfo.Hitbox.transform.root.GetComponent<HPHandler>().OnTakeDamage();

            Debug.Log($"{Time.time} hit Hitbox {hitInfo.Hitbox.name}");
            isHitOtherPlayer = true;
        }
        // else if (hitInfo.Collider != null)
        // {
        //     Debug.Log($"{Time.time} hit PhysX {hitInfo.Hitbox.name}");
        // }
        else{
            Debug.Log("No object hit");
        }

        //Debug
        if(isHitOtherPlayer)
        {
            Debug.DrawRay(rayOrigin, rayDirection*hitDistance,Color.red);
        }
        else Debug.DrawRay(rayOrigin, rayDirection*hitDistance,Color.green);

        
        
    }

    IEnumerator AtkEffect()
    {

        isAtking = true;
        animator.Play("Attack01_SwordAndShiled");
        yield return new WaitForSeconds(1f);
        isAtking = false;
    }

    static void OnAtkChanged(Changed<WeaponHandler> changed)
    {
        // Debug.Log($"{Time.time} OnAtkChanged value {changed.Behaviour.isAtking}");
        bool isAtkingCurrent = changed.Behaviour.isAtking;

        changed.LoadOld();

        bool isAtkingOld = changed.Behaviour.isAtking;
        if(isAtkingCurrent && !isAtkingOld)
            changed.Behaviour.OnAtkingRemote();
    }

    void OnAtkingRemote()
    {
        if(!Object.HasInputAuthority)
            animator.Play("Attack01_SwordAndShiled");
    }
}
