using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPointDamage : Ability
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float radius;
    protected override void CastAbility()
    {
        AbilityCreator.AreaDamage(AbilityOwner, mousePosition, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), radius);
        VFXManager.Instance.SpellHit(mousePosition, 1f, Color.HSVToRGB(320f/360f, 0.75f, 1f));
    }
}
