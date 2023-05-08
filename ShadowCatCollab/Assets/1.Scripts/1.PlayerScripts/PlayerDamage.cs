using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int maxHealth = 3;
    public float currentHealth;

    public HealthBar healthBar;

    private bool canDamage = true;

    public GameObject DeadPanel;
    public GameObject DetectiveFoto;
    public GameObject GatoFoto;

    public bool FotosSwitch = true;

    public bool PlayerDead = false;

    public GameObject BttnReset, BttnMn;

    //Deactivate enemy script
    

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && FotosSwitch)
        {
            DetectiveFoto.SetActive(false);
            GatoFoto.SetActive(true);
            FotosSwitch = false;

        }
        else if(Input.GetKeyDown(KeyCode.Q) && !FotosSwitch)
        {
            DetectiveFoto.SetActive(true);
            GatoFoto.SetActive(false);
            FotosSwitch = true;
        }

    }
    public void DealDamage()
    {
        //if (canDamage)
        //{

        currentHealth = currentHealth - 1f;
            healthBar.SetHealth(currentHealth);

            if (currentHealth == 0)
            {
                PlayerDead = true;
                DeadPanel.SetActive(true);
                Invoke(nameof(StopTime), 5f);
                Invoke(nameof(BttnMenu), 4.8f);

            }

            //canDamage = false;
            //StartCoroutine(WaitForDamage());
        //}
    }

    void BttnMenu()
    {
        BttnMn.SetActive(true);
        BttnReset.SetActive(true);
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds (2f);
        canDamage = true;
    }

    void StopTime()
    {
        Time.timeScale = 0f;
    }

}
