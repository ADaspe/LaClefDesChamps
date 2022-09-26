using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerVFX : MonoBehaviour
{
    // BOOK EYE MAT
    public Renderer eyeMaterial;
    public Renderer playerMat;
    private bool _trailOn;
    // CameraShake
    public CinemachineImpulseSource impulse;

    [SerializeField] private Fire_big fire_Big;
    [SerializeField] private Metal_big metal_Big;
    [SerializeField] private Light_big light_Big;
    [SerializeField] private PlayerBasicCombat playerBasicCombat;
    private SpecialAttackFilter SpecialAttackFilter;
    private PlayerState playerState;
    private CharacterAnimations CharacterAnimations;
    private AbsorbManager AbsorbManager;

    [SerializeField] private Transform[] _VFXpoints;   

    // VFX
    [SerializeField] GameObject splashFX;
    [SerializeField] public GameObject[] AttackTrail;
    [SerializeField] public GameObject swishMetal;
    [HideInInspector] public ParticleSystem fireVFX;
    [HideInInspector] public ParticleSystem metalVFX;
    [SerializeField] GameObject metalRock;
    public ParticleSystem lightVFX;
    [SerializeField] public ParticleSystem grabVFX;
    [HideInInspector] public ParticleSystem healVFX;
    [HideInInspector] public ParticleSystem absorbVFX;

    public PowerType powerType;
    private bool _onWater;

    public enum PowerType
    {
        empty = 0,
        fire = 1,
        metal = 2,
        light = 3,
        grab = 4,
        absorb = 5,
        Atk = 6,
        heal
    }

    void Start()
    {
        _trailOn = false;
        SpecialAttackFilter = GetComponentInParent<SpecialAttackFilter>();
        playerState = GetComponentInParent<PlayerState>();
        CharacterAnimations = GetComponent<CharacterAnimations>();
        AbsorbManager = GetComponent<AbsorbManager>();
    }
    public void cameraShake(float shackeforce) => impulse.GenerateImpulse(new Vector3(shackeforce, shackeforce, shackeforce));

    //public void cameraShake(float shackeforce) => impulse.GenerateImpulse(shackeforce);
    //public void swapEyeColor(Color color) => eyeMaterial.SetColor("_EyeColor", color);  // dosen't work

    // realm of illegal things call a chaque fin d'anim pour sortir du state
    public void EndAttack()
    {
        playerState.isAttacking = false;
        foreach (GameObject trail in AttackTrail) { trail.SetActive(false); }
        swishMetal.SetActive(false);
    }
    public void Heal()
    {
        healVFX.Play(true);
        SetMaterialPorperty("_heal", 7);
        StartCoroutine(ResetFVX("_heal", 1, .7f));
    }
    public void Splash()
    {
        if(_onWater)
        {
            Instantiate(splashFX.gameObject, transform.position, splashFX.transform.rotation);
        }
    }
    public void SetMaterialPorperty(string propertyname ,float _float) => playerMat.material.SetFloat(propertyname, _float);
    public void playVFX(ParticleSystem newVFX, Transform castPoint)
    {
        newVFX.transform.position = castPoint.transform.position;
        newVFX.transform.rotation = castPoint.transform.rotation;   // on peut dire ca plus simplement srmt

        newVFX.Play(true);
        newVFX.transform.SetParent(null);

        powerType = PowerType.empty;    // back to mmama
    }
    public void AttackSwish()
    {
        _trailOn = !_trailOn;
        if   (!playerState.swapAtk) { AttackTrail[0].SetActive(_trailOn); }
        else                       { AttackTrail[1].SetActive(_trailOn); }
    }
    public void SwishMetal() => swishMetal.SetActive(true);
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("water"))
        {
            _onWater = true;
        }
    }   // is walking on water
    private void OnTriggerExit(Collider other)
    {
            _onWater = false;
    }
    public void StartVFX()
    {
        switch (powerType)
        {
            case PowerType.empty:
                break;

            case PowerType.fire:
                playVFX(fireVFX, _VFXpoints[0]);
                fire_Big.EnableCollider();      // c'est de la merde fix ca pas propre grosse ref sale de chaque script jsp comment fix ca mdr
                break;

            case PowerType.metal:
                playVFX(metalVFX, _VFXpoints[1]);  // transform is not perfecty placed 
                metal_Big.EnableCollider();
                metalRock.SetActive(true);
                break;

            case PowerType.light:
                playVFX(lightVFX, _VFXpoints[2]);
                light_Big.EnableCollider();

                break;
            case PowerType.grab:
                playVFX(grabVFX, _VFXpoints[3]);
                //enable and disable collider
                break;

            case PowerType.Atk:
                playerBasicCombat.EnableCollider();     // :(((((
                break;

            case PowerType.absorb:
                //playVFX(absorbVFX, _VFXpoints[4]);
                absorbVFX.Play(true);
                AbsorbManager.instance.EnableCollider();    // this is very ugly :'(
                break;

            case PowerType.heal:
                playVFX(healVFX, _VFXpoints[5]);
                break;
            default:
                break;
        }
    }
    IEnumerator ResetFVX(string propertyname, float _float, float waitbeforereset)
    {
        yield return new WaitForSeconds(waitbeforereset);
        SetMaterialPorperty(propertyname, _float);
    }
}