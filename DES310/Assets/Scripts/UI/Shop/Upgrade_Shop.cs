using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Shop : MonoBehaviour
{
    [SerializeField] float damageUpgradeMagnitude, fireSpeedUpgradeMagnitude, overheatCapacityUpgradeMagnitude, rangeUpgradeMagnitude;
    [SerializeField] int damageUpgradeCost, fireSpeedUpgradeCost, overheatCapacityUpgradeCost, rangeUpgradeCost;
    [SerializeField] Item_SO currencyItem;
    [SerializeField] Text oldWeaponName, newWeaponName, oldWeaponDescription, newWeaponDescription, damageUpgradeText, fireSpeedUpgradeText, overheatUpgradeText, rangeUpgradeText, costText;
    [SerializeField] Image oldImage, newImage, currencyImage;
    [SerializeField] Dropdown dropdown;
    [SerializeField] List<Equippable_Item_SO> weaponScriptableObjects = new();
    [SerializeField] List<Projectile_Type> associatedProjectileTypes = new();
    [SerializeField] Settings_SO settings;
    [SerializeField] Inventory_SO inventory;
    [SerializeField] Agent_Weapon agentWeapon;
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
        Projectile_Type selectedProjectileType = associatedProjectileTypes[selectedWeapon];
        Weapon_Properties selectedWeaponProperties = weaponScriptableObjects[selectedWeapon].weaponProperties;
        Weapon_Upgrades selectedWeaponUpgrades = selectedWeaponProperties.weaponUpgrades;
        oldWeaponDescription.text = "Damage: " + (selectedProjectileType.damage * selectedWeaponUpgrades.damageModifier) + "\nFire Speed: " + Mathf.Round((1 / (selectedWeaponProperties.fireDelay / selectedWeaponUpgrades.fireSpeedModifier)) * 100f) / 100f + " bullets/second\nTime To Overheat: " + Mathf.Round((selectedWeaponProperties.overheatCapacity * selectedWeaponUpgrades.overheatCapacityModifier) * 100f) / 100f + " seconds\nRange: " + (selectedProjectileType.despawnTimer * selectedWeaponUpgrades.fireRangeModifier * selectedProjectileType.speed) + " meters";
        newWeaponDescription.text = "Damage: " + (selectedProjectileType.damage * (selectedWeaponUpgrades.damageModifier + damageUpgradeMagnitude * damageUpgrades)) + "\nFire Speed: " + Mathf.Round((1 / (selectedWeaponProperties.fireDelay / (selectedWeaponUpgrades.fireSpeedModifier + fireSpeedUpgradeMagnitude * fireSpeedUpgrades))) * 100f) / 100f + " bullets/second\nTime To Overheat: " + Mathf.Round((selectedWeaponProperties.overheatCapacity * (selectedWeaponUpgrades.overheatCapacityModifier + overheatCapacityUpgradeMagnitude * overheatUpgrades)) * 100f) / 100f + " seconds\nRange: " + (selectedProjectileType.despawnTimer * (selectedWeaponUpgrades.fireRangeModifier + fireSpeedUpgradeMagnitude * rangeUpgrades) * selectedProjectileType.speed) + " meters";
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
            if (inventory.RemoveItem(currencyItem, cost) && cost > 0)
            {
                Sound_Manager.instance.PlaySound(Sound_Manager.SFX.PurchaseSuccessful, transform, 1f);
                ApplyUpgrades();
                agentWeapon.SetWeapon(agentWeapon.weapon, agentWeapon.weapon.DefaultParametersList, true);
            }
            else
            {
                Sound_Manager.instance.PlaySound(Sound_Manager.SFX.PurchaseUnsuccessful, transform, 1f);
            }
        }
        else
        {
            Sound_Manager.instance.PlaySound(Sound_Manager.SFX.PurchaseUnsuccessful, transform, 1f);
        }
    }

    public void ApplyUpgrades()
    {
        Weapon_Upgrades weaponUpgrades = weaponScriptableObjects[dropdown.value].weaponProperties.weaponUpgrades;
        weaponUpgrades.damageModifier += damageUpgrades * damageUpgradeMagnitude;
        weaponUpgrades.fireSpeedModifier += fireSpeedUpgrades * fireSpeedUpgradeMagnitude;
        weaponUpgrades.overheatCapacityModifier += overheatUpgrades * overheatCapacityUpgradeMagnitude;
        weaponUpgrades.fireRangeModifier += rangeUpgrades * rangeUpgradeMagnitude;
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
