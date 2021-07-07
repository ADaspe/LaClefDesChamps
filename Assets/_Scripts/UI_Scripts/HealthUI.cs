using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Sprite healthSprite = null;
    [SerializeField] private Sprite healthSpriteEmpty = null;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private Vector2 sizeDelta = Vector2.zero;
    private List<HealthImage> healthImageList;

    private HealthSystem healthSystem;

    private void Awake()
    {
        healthImageList = new List<HealthImage>();
    }

    void Start()
    {
        HealthSystem healthSystem = FindObjectOfType<PlayerController>().playerHealth;
        SetHealthSystem(healthSystem);

    }

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        List<HealthSystem.HealthUnit> healthList = healthSystem.GetHealthList();
        Vector2 healthAnchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < healthList.Count; i++)
        {
            HealthSystem.HealthUnit healthUnit = healthList[i];
            CreateHealthImage(healthAnchoredPosition).SetHealth(healthUnit.GetHealth());
            healthAnchoredPosition += new Vector2(horizontalOffset, 0);
        }

        this.healthSystem.OnDamaged += HealthSystem_OnDamaged;
        this.healthSystem.OnHealed += HealthSystem_OnHealed;
        /*CreateHealthImage(new Vector2(0, 0));
        CreateHealthImage(new Vector2(25, 0));
        CreateHealthImage(new Vector2(50, 0));*/
    }

    #region Subscribers Methods
    private void HealthSystem_OnHealed()
    {
        RefreshHealthImages();
        
    }

    private void HealthSystem_OnDamaged()
    {
        RefreshHealthImages();
        /*
        List<HealthSystem.HealthUnit> healthList = healthSystem.GetHealthList();
        for(int i = 0; i < healthImageList.Count; i++)
        {
            HealthImage healthImage = healthImageList[i];
            HealthSystem.HealthUnit healthUnit = healthList[i];
            healthImage.SetHealth(healthUnit.GetHealth());
            
        }*/
    }

    private void OnDisable()
    {
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
        healthSystem.OnHealed -= HealthSystem_OnHealed;
    }
    #endregion

    #region Debug Methods
    public void DebugDamage(int damageAmout)
    {
        healthSystem.Damage(damageAmout);
    }

    public void DebugHeal(int healAmount)
    {
        healthSystem.Heal(healAmount);
    }
    #endregion

    private HealthImage CreateHealthImage(Vector2 anchoredPosition)
    {
        GameObject healthObject = new GameObject("HealthUnit", typeof(Image));

        healthObject.transform.SetParent(transform);
        healthObject.transform.localPosition = Vector3.zero;

        healthObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        healthObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);

        Image healthImageUI = healthObject.GetComponent<Image>();
        healthImageUI.sprite = healthSprite;

        HealthImage healthImage = new HealthImage(this, healthImageUI);
        healthImageList.Add(healthImage);

        return healthImage;
    }

    private void RefreshHealthImages()
    {
        List<HealthSystem.HealthUnit> healthList = healthSystem.GetHealthList();
        for (int i = 0; i < healthImageList.Count; i++)
        {
            HealthImage healthImage = healthImageList[i];
            HealthSystem.HealthUnit healthUnit = healthList[i];
            healthImage.SetHealth(healthUnit.GetHealth());

        }
    }

    public class HealthImage
    {

        private Image healthImage;
        private HealthUI healthUI;

        public HealthImage(HealthUI healthUI, Image healthImageUI)
        {
            this.healthImage = healthImageUI;
            this.healthUI = healthUI;
        }

        public void SetHealth(bool isFull)
        {
            if(!isFull)
            {
                healthImage.sprite = healthUI.healthSpriteEmpty;
            }
            else
            {
                healthImage.sprite = healthUI.healthSprite;
            }
        }
    }


}