﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{ 
    [Header("Name")] 
    [SerializeField] private string weaponName = "";
  
    [Header("Settings")] 
    [SerializeField] protected float attackCooltime = 0.5f;

/*     [Header("Weapon")] 
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true; */
/* 
    [Header("Recoil")] 
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 30; */

/*     [Header("Effects")] 
    [SerializeField] private ParticleSystem muzzlePS; */

    // Returns the name of this Weapon
    public string WeaponName => weaponName;

    // Reference of the Character that controls this Weapon
    public Character WeaponOwner { get; set; }
    public CharacterLook WeaponOwnerLook { get; set; }
    protected Vector2 weaponFacing { get { return GetWeaponFacing(); } }

    // Returns if we can shoot now
    public bool CanShoot { get { return internalCooldown <= 0f; } }

    // Internal
    protected float internalCooldown = 0f;
    //protected Animator animator;
    //private readonly int weaponUseParameter = Animator.StringToHash("WeaponUse");

    protected virtual void Awake()
    {     
        //animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (internalCooldown > 0f) { internalCooldown -= Time.deltaTime; }
    }

    // Trigger our Weapon in order to use it
    public virtual void UseWeapon()
    {
        RequestShot();
    }

    public void StopWeapon()
    {
        /* if (useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        } */
    }

    // Makes our weapon start shooting
    protected virtual void RequestShot()
    {
        if (!CanShoot) { return; }
        
/*      
        if (useRecoil)
        {
            Recoil();
        } */
         
        //animator.SetTrigger(weaponUseParameter);
        //WeaponAmmo.ConsumeAmmo(); 
        //muzzlePS.Play();     
    }

    // Reference the owner of this Weapon
    public void SetOwner(Character owner)
    {
        WeaponOwner = owner; 
        WeaponOwnerLook = owner.GetComponent<CharacterLook>();
        //controller = WeaponOwner.GetComponent<PCController>();
    }

    private Vector3 GetWeaponFacing()
    {
        if ( WeaponOwnerLook == null ) { return Vector3.zero; }
        return WeaponOwnerLook.direction;
    }
 
}