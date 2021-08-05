using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    PhysicAttackBuff,
    FireAttackBuff,
    FrogBuff,
    MetalBuff,
    FireflyBuff,
    GlobalChargeBuff,
    FireChargeBuff,
    FrogChargeBuff,
    MetalChargeBuff,
    FireflyChargeBuff,
}
public enum Currency
{
    SkillPear,
    Scrolls
}

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill", order = 2)]
public class SkillSO : ScriptableObject
{
    [SerializeField] private AttackSettingsSO attackSO;
    public bool isActivated = false;
    public List<SkillSO> requiredSkills;
    public Effect skillEffect;
    public float skillValue;
    public Currency moneyToSpend;
    public int cost;

    public bool Activate()
    {
        foreach (SkillSO skill  in requiredSkills)
        {
            if (!skill.isActivated)
            {
                return false;
            }
        }
        isActivated = true;
        switch (skillEffect)
        {
            case Effect.PhysicAttackBuff:
                attackSO.damageATK1 += (int)skillValue;
                attackSO.damageATK2 += (int)skillValue;
                attackSO.damageATK3 += (int)skillValue;
                break;
            case Effect.FireAttackBuff:
                attackSO.dpsATK3Fire += (int)skillValue;
                break;
            case Effect.FrogBuff:
                attackSO.frogTimeATK3 += skillValue;
                break;
            case Effect.MetalBuff:
                attackSO.damageReductionATK3Metal += skillValue;
                break;
            case Effect.FireflyBuff:
                attackSO.stunTimeATK3Firefly += skillValue;
                break;
            case Effect.GlobalChargeBuff:
                attackSO.GlobalChargeNumber += (int)skillValue;
                break;
            case Effect.FireChargeBuff:
                attackSO.MaxChargeFire += (int)skillValue;
                break;
            case Effect.FrogChargeBuff:
                attackSO.MaxChargeFrog += (int)skillValue;
                break;
            case Effect.FireflyChargeBuff:
                attackSO.MaxChargeFirefly += (int)skillValue;
                break;
            case Effect.MetalChargeBuff:
                attackSO.MaxChargeMetal += (int)skillValue;
                break;
            default:
                break;
        }
        return isActivated;
    }

    public bool Reset()
    {
        if (isActivated)
        {
            switch (skillEffect)
            {
                case Effect.PhysicAttackBuff:
                    attackSO.damageATK1 -= (int)skillValue;
                    attackSO.damageATK2 -= (int)skillValue;
                    attackSO.damageATK3 -= (int)skillValue;
                    break;
                case Effect.FireAttackBuff:
                    attackSO.dpsATK3Fire -= (int)skillValue;
                    break;
                case Effect.FrogBuff:
                    attackSO.frogTimeATK3 -= skillValue;
                    break;
                case Effect.MetalBuff:
                    attackSO.damageReductionATK3Metal -= skillValue;
                    break;
                case Effect.FireflyBuff:
                    attackSO.stunTimeATK3Firefly -= skillValue;
                    break;
                case Effect.GlobalChargeBuff:
                    attackSO.GlobalChargeNumber -= (int)skillValue;
                    break;
                case Effect.FireChargeBuff:
                    attackSO.MaxChargeFire -= (int)skillValue;
                    break;
                case Effect.FrogChargeBuff:
                    attackSO.MaxChargeFrog -= (int)skillValue;
                    break;
                case Effect.FireflyChargeBuff:
                    attackSO.MaxChargeFirefly -= (int)skillValue;
                    break;
                case Effect.MetalChargeBuff:
                    attackSO.MaxChargeMetal -= (int)skillValue;
                    break;
                default:
                    break;
            }
            isActivated = false;
            return true;
        }
        else
        {
            return false;
        }

    }

}
