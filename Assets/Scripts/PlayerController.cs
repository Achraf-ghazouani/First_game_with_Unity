using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Third Person Controller References
    [SerializeField] private Animator playerAnim;

    // Equip-Unequip parameters
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped;

    // Blocking Parameters
    public bool isBlocking;

    // Kick Parameters
    public bool isKicking;

    // Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;

    // Player Health
    public int maxHealth = 100;
    private int currentHealth;

    // Score
    public int score = 0;
    public Text texthealth;



    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        Cursor.visible = false;
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        Attack();
        Equip();
        Block();
        Kick();
    }

    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void Kick()
    {
        if (Input.GetKey(KeyCode.LeftControl) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }
        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
        {
            if (!isEquipped)
                return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            // Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            // Call Attack Triggers
            playerAnim.SetTrigger("Attack" + currentAttack);

            // Reset Timer
            timeSinceAttack = 0;
        }
    }







    public void TakeDamage(int damage)
    {
        // Decrease the player's health by the damage amount
        currentHealth -= damage;

        // Update UI
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Reload the current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }



    private void UpdateHealthUI()
    {
        // Update the UI text with the current health
        texthealth.text = "Health: " + currentHealth.ToString();
    }


    // This will be used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    }

    //whene the player is hit the enemy the enemy destroy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            if (isAttacking)
            {
                Destroy(other.gameObject);
            }

        }
      
    }


}
