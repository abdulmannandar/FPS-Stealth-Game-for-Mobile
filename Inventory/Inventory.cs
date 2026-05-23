
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryTable;
    public GameObject Knife;
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Shotgun;
    public GameObject Sniper;
    public GameObject Map;
    public GameObject Binocular;

    public GameObject KnifeU;
    public GameObject PistolU;
    public GameObject RifleU;
    public GameObject ShotgunU;
    public GameObject SniperU;
    public GameObject MapU;
    public GameObject BinocularU;

    public GameObject PistolR;
    public GameObject RifleR;
    public GameObject ShotgunR;
    public GameObject SniperR;

    public TextMeshProUGUI pistolammo;
    public TextMeshProUGUI rifleammo;
    public TextMeshProUGUI shotgunammo;
    public TextMeshProUGUI sniperammo;

    public AudioClip pistol;
    public AudioClip sniper; 
    public AudioClip rifle; 
    public AudioClip bagopen;
    public AudioClip knife;
    public AudioClip map;
    public AudioClip bagclose;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void OpenInventory()
    {
        InventoryTable.SetActive(true);
        audioSource.clip = bagopen;
        audioSource.Play();
    }
    public void CloseInventory()
    {
        InventoryTable.SetActive(false);
        audioSource.clip = bagclose;
        audioSource.Play();
    }
    public void SelectKnife()
    {
        Knife.SetActive(true);
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Sniper.SetActive(false);
        Shotgun.SetActive(false);
        Map.SetActive(false);
        Binocular.SetActive(false);

        KnifeU.SetActive(true);
        PistolU.SetActive(false);
        RifleU.SetActive(false);
        SniperU.SetActive(false);
        ShotgunU.SetActive(false);
        MapU.SetActive(false);
        BinocularU.SetActive(false); 
        
        PistolR.SetActive(false);
        RifleR.SetActive(false);
        SniperR.SetActive(false);
        ShotgunR.SetActive(false);

        pistolammo.enabled = false;
        rifleammo.enabled = false;
        shotgunammo.enabled = false;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);

        audioSource.clip = knife;
        audioSource.Play();
    }
    public void SelectPistol()
    {
        Knife.SetActive(false);
        Pistol.SetActive(true);
        Rifle.SetActive(false);
        Sniper.SetActive(false);
        Shotgun.SetActive(false);
        Map.SetActive(false);
        Binocular.SetActive(false);

        KnifeU.SetActive(false);
        PistolU.SetActive(true);
        RifleU.SetActive(false);
        SniperU.SetActive(false);
        ShotgunU.SetActive(false);
        MapU.SetActive(false);
        BinocularU.SetActive(false);

        PistolR.SetActive(true);
        RifleR.SetActive(false);
        SniperR.SetActive(false);
        ShotgunR.SetActive(false);

        pistolammo.enabled = true;
        rifleammo.enabled = false;
        shotgunammo.enabled = false;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);

        audioSource.clip = pistol;
        audioSource.Play();
    }
    public void SelectRifle()
    {
        Knife.SetActive(false);
        Pistol.SetActive(false);
        Rifle.SetActive(true);
        Sniper.SetActive(false);
        Shotgun.SetActive(false);
        Map.SetActive(false);
        Binocular.SetActive(false);

        KnifeU.SetActive(false);
        PistolU.SetActive(false);
        RifleU.SetActive(true);
        SniperU.SetActive(false);
        ShotgunU.SetActive(false);
        MapU.SetActive(false);
        BinocularU.SetActive(false);

        PistolR.SetActive(false);
        RifleR.SetActive(true);
        SniperR.SetActive(false);
        ShotgunR.SetActive(false);

        pistolammo.enabled = false;
        rifleammo.enabled = true;
        shotgunammo.enabled = false;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);

        audioSource.clip = rifle;
        audioSource.Play();
    }
    public void SelectSniper()
    {
        Knife.SetActive(false);
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Sniper.SetActive(true);
        Shotgun.SetActive(false);
        Map.SetActive(false);
        Binocular.SetActive(false);

        KnifeU.SetActive(false);
        PistolU.SetActive(false);
        RifleU.SetActive(false);
        SniperU.SetActive(true);
        ShotgunU.SetActive(false);
        MapU.SetActive(false);
        BinocularU.SetActive(false);

        PistolR.SetActive(false);
        RifleR.SetActive(false);
        SniperR.SetActive(true);
        ShotgunR.SetActive(false);

        pistolammo.enabled = false;
        rifleammo.enabled = false;
        shotgunammo.enabled = false;
        sniperammo.enabled = true;

        InventoryTable.SetActive(false);

        audioSource.clip = sniper;
        audioSource.Play();
    }
    public void SelectShotGun()
    {
        Knife.SetActive(false);
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Sniper.SetActive(false);
        Shotgun.SetActive(true);
        Map.SetActive(false);
        Binocular.SetActive(false);

        KnifeU.SetActive(false);
        PistolU.SetActive(false);
        RifleU.SetActive(false);
        SniperU.SetActive(false);
        ShotgunU.SetActive(true);
        MapU.SetActive(false);
        BinocularU.SetActive(false);

        PistolR.SetActive(false);
        RifleR.SetActive(false);
        SniperR.SetActive(false);
        ShotgunR.SetActive(true);

        pistolammo.enabled = false;
        rifleammo.enabled = false;
        shotgunammo.enabled = true;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);

        audioSource.clip = rifle;
        audioSource.Play();
    }
    public void SelectMap()
    {
        Knife.SetActive(false);
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Sniper.SetActive(false);
        Shotgun.SetActive(false);
        Map.SetActive(true);
        Binocular.SetActive(false);

        KnifeU.SetActive(false);
        PistolU.SetActive(false);
        RifleU.SetActive(false);
        SniperU.SetActive(false);
        ShotgunU.SetActive(false);
        MapU.SetActive(true);
        BinocularU.SetActive(false);

        PistolR.SetActive(false);
        RifleR.SetActive(false);
        SniperR.SetActive(false);
        ShotgunR.SetActive(false);

        pistolammo.enabled = false;
        rifleammo.enabled = false;
        shotgunammo.enabled = false;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);

        audioSource.clip = map;
        audioSource.Play();
    }
    public void SelectBinoculars()
    {
        Knife.SetActive(false);
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Sniper.SetActive(false);
        Shotgun.SetActive(false);
        Map.SetActive(false);
        Binocular.SetActive(true);

        KnifeU.SetActive(false);
        PistolU.SetActive(false);
        RifleU.SetActive(false);
        SniperU.SetActive(false);
        ShotgunU.SetActive(false);
        MapU.SetActive(false);
        BinocularU.SetActive(true);

        PistolR.SetActive(false);
        RifleR.SetActive(false);
        SniperR.SetActive(false);
        ShotgunR.SetActive(false);

        pistolammo.enabled = false;
        rifleammo.enabled = false;
        shotgunammo.enabled = false;
        sniperammo.enabled = false;

        InventoryTable.SetActive(false);
    }
}
