using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
public class HPHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHPChanged))]
    byte HP {get; set;}

    [Networked(OnChanged = nameof(OnStateChanged))]
    public bool isDead {get; set;}
    bool isInitialized = false;
    Animator animator;
    
    public Color defaultMeshColor;

    const byte startingHP = 100;
    Image currentHealthDisplay;

    private void Awake() {
        animator = GetComponent<Animator>();
        GameObject playerUI = GameObject.FindGameObjectWithTag("PlayerUIHealthBar");
        currentHealthDisplay = playerUI.GetComponent<Image>();
    }
    void Start()
    {
        if(currentHealthDisplay == null)
            Debug.Log("Could not find health bar");
        HP = startingHP;
        isDead = false;
        isInitialized = true;
        
    }

    //Only Called by Server
    public void OnTakeDamage()
    {
        if(isDead)
        return;

    // Deals damage to player when hit by weapon raycast
        HP -=10;
        // HP = (byte)Mathf.Clamp(HP, 0, startingHP);
    // Updates the Player UI so they know they have been hit

        Debug.Log($"You too Damage!! Your health is {HP}");

    //Checks if the Object that has been hit has Input authority, If it does not then trigger "Hit animation"
        if(!Object.HasInputAuthority)
            animator.Play("GetHit01_SwordAndShield");
            currentHealthDisplay.fillAmount = HP/startingHP;
        
        if(HP<=0)
        {
            isDead = true;
            Debug.Log("You is Dead");
        }
        

    }

    static void OnHPChanged(Changed<HPHandler> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.HP}");
        byte newHP = changed.Behaviour.HP;

        //load the old value

        changed.LoadOld();
        byte oldHP = changed.Behaviour.HP;

        if(newHP < oldHP)
            changed.Behaviour.OnHPReduced();
    }

    private void OnHPReduced()
    {
        if(!isInitialized)
            return;
        
        if(Object.HasInputAuthority)
            animator.Play("GetHit01_SwordAndShield");
            currentHealthDisplay.fillAmount = HP/startingHP;
    }

    static void OnStateChanged(Changed<HPHandler> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.isDead}");
    }



    



}
