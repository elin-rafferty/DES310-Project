using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] float damageUpgradeMagnitude, fireSpeedUpgradeMagnitude, overheatCapacityUpgradeMagnitude, rangeUpgradeMagnitude;
    [SerializeField] int damageUpgradeCost, fireSpeedUpgradeCost, overheatCapacityUpgradeCost, rangeUpgradeCost;
    [SerializeField] ItemSO currencyItem;
    [SerializeField] Text oldWeaponName, newWeaponName, oldWeaponDescription, newWeaponDescription, damageUpgradeText, fireSpeedUpgradeText, overheatUpgradeText, rangeUpgradeText, costText;
    [SerializeField] Image oldImage, newImage, currencyImage;
    [SerializeField] Dropdown dropdown;
    [SerializeField] List<EquippableItemSO> weaponScriptableObjects = new();
    [SerializeField] List<ProjectileType> associatedProjectileTypes = new();
    [SerializeField] SettingsSO settings;
    [SerializeField] InventorySO inventory;
    [SerializeField] AgentWeapon agentWeapon;
    List<Dropdown.OptionData> weaponNames = new();
    int damageUpgrades = 0;
    int fireSpeedUpgrades = 0;
    int overheatUpgrades = 0;
    int rangeUpgrades = 0;
    int cost = 0;

    private void Start()
    {
        foreach (var weapon in weaponScriptableObjects)
        {
            weaponNames.Add(new Dropdown.OptionData(weapon.Name, weapon.ItemImage));
        }
        dropdown.AddOptions(weaponNames);
    }

    private void Update()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        int selectedWeapon = dropdown.value;
        oldWeaponName.text = weaponNames[selectedWeapon].text;
        newWeaponName.text = weaponNames[selectedWeapon].text;
        oldImage.sprite = weaponScriptableObjects[selectedWeapon].ItemImage;
        newImage.sprite = weaponScriptableObjects[selectedWeapon].ItemImage;
        currencyImage.sprite = currencyItem.ItemImage;
        cost = damageUpgrades * damageUpgradeCost + fireSpeedUpgrades * fireSpeedUpgradeCost + overheatUpgrades * overheatCapacityUpgradeCost + rangeUpgrades * rangeUpgradeCost;
        costText.text = cost.ToString();
        damageUpgradeText.text = damageUpgrades.ToString();
        fireSpeedUpgradeText.text = fireSpeedUpgrades.ToString();
        overheatUpgradeText.text = overheatUpgrades.ToString();
        rangeUpgradeText.text = rangeUpgrades.ToString();
        ProjectileType selectedProjectileType = associatedProjectileTypes[selectedWeapon];
        WeaponProperties selectedWeaponProperties = weaponScriptableObjects[selectedWeapon].weaponProperties;
        WeaponUpgrades selectedWeaponUpgrades = selectedWeaponProperties.weaponUpgrades;
        oldWeaponDescription.text = "Damage: " + (selectedProjectileType.damage * selectedWeaponUpgrades.damageModifier) + "\nFire Speed: " + Mathf.Round((1 / (selectedWeaponProperties.fireDelay / selectedWeaponUpgrades.fireSpeedModifier)) * 100f) / 100f + " bullets/second\nTime To Overheat: " + Mathf.Round((selectedWeaponProperties.overheatCapacity * selectedWeaponUpgrades.overheatCapacityModifier) * 100f) / 100f + " seconds\nRange: " + (selectedProjectileType.despawnTimer * selectedWeaponUpgrades.fireRangeModifier * selectedProjectileType.speed) + " meters";
        newWeaponDescription.text = "Damage: " + (selectedProjectileType.damage * (selectedWeaponUpgrades.damageModifier + 0.1f * damageUpgrades)) + "\nFire Speed: " + Mathf.Round((1 / (selectedWeaponProperties.fireDelay / (selectedWeaponUpgrades.fireSpeedModifier + 0.1f * fireSpeedUpgrades))) * 100f) / 100f + " bullets/second\nTime To Overheat: " + Mathf.Round((selectedWeaponProperties.overheatCapacity * (selectedWeaponUpgrades.overheatCapacityModifier + 0.1f * overheatUpgrades)) * 100f) / 100f + " seconds\nRange: " + (selectedProjectileType.despawnTimer * (selectedWeaponUpgrades.fireRangeModifier + 0.1 * rangeUpgrades) * selectedProjectileType.speed) + " meters";
    }

    public void AddDamageUpgrade()
    {
        damageUpgrades++;
    }

    public void RemoveDamageUpgrade()
    {
        damageUpgrades--;
        if (damageUpgrades < 0) damageUpgrades = 0;
    }

    public void AddFireSpeedUpgrade()
    {
        fireSpeedUpgrades++;
    }

    public void RemoveFireSpeedUpgrade()
    {
        fireSpeedUpgrades--;
        if (fireSpeedUpgrades < 0) fireSpeedUpgrades = 0;
    }

    public void AddOverheatCapacityUpgrade()
    {
        overheatUpgrades++;
    }

    public void RemoveOverheatCapacityUpgrade()
    {
        overheatUpgrades--;
        if (overheatUpgrades < 0) overheatUpgrades = 0;
    }

    public void AddRangeUpgrade()
    {
        rangeUpgrades++;
    }

    public void RemoveRangeUpgrade()
    {
        rangeUpgrades--;
        if (rangeUpgrades < 0) rangeUpgrades = 0;
    }

    public void PurchaseUpgrades()
    {
       if (inventory.HasItem(currencyItem, cost))
        {
            if (inventory.RemoveItem(currencyItem, cost))
            {
                ApplyUpgrades();
                agentWeapon.SetWeapon(agentWeapon.weapon, agentWeapon.weapon.DefaultParametersList);
            }
        }
    }

    public void ApplyUpgrades()
    {
        WeaponUpgrades weaponUpgrades = weaponScriptableObjects[dropdown.value].weaponProperties.weaponUpgrades;
        weaponUpgrades.damageModifier += damageUpgrades * 0.1f;
        weaponUpgrades.fireSpeedModifier += fireSpeedUpgrades * 0.1f;
        weaponUpgrades.overheatCapacityModifier += overheatUpgrades * 0.1f;
        weaponUpgrades.fireRangeModifier += rangeUpgrades * 0.1f;
        ResetUnpurchasedUpgrades();
    }

    public void ResetUnpurchasedUpgrades()
    {
        damageUpgrades = 0;
        fireSpeedUpgrades = 0;
        overheatUpgrades = 0;
        rangeUpgrades = 0;
    }
}
