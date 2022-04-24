using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private int level;
    private float currentLevelExperience = 1;
    private float experienceToLevel;

    private Quaternion targetRotation;
    public float rotationSpeed = 450;

    public GameObject bala;
    public Transform balaSpawner;

    private float rpmGun = 0;
    private float listoRpm = 0.55f;

    private Rigidbody rb;

    private float dashMultiplier = 1;
    private bool dash;

    public Slider healthSlider;
    private float health;
    public Image healthColor;

    void Start()
    {
        health = 10;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        rb = GetComponent<Rigidbody>();
        rpmGun = listoRpm;
    }
    private void Update()
    {
        healthSlider.transform.parent.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        healthSlider.transform.parent.LookAt(Camera.main.transform.position);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovePlayer();
        }

        RotatePlayer();

        rpmGun += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dash == false)
        {
            dash = true;
            Sprint();
        }
    }

    public void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, y);

        rb.velocity = movement * 4f * dashMultiplier;
    }

    public void RotatePlayer()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        if (rpmGun > listoRpm)
        {
            Instantiate(bala, balaSpawner.position, balaSpawner.rotation);
            rpmGun = 0;
        }
    }

    public void AddExperience(float exp)
    {
        currentLevelExperience += exp;
        if(currentLevelExperience >= experienceToLevel)
        {
            currentLevelExperience -= experienceToLevel;
            LevelUp();
        }

        GameManager.Instance.SetPlayerExperience(currentLevelExperience / experienceToLevel, level);
    }

    public void Sprint()
    {
        dashMultiplier = 2;
        StartCoroutine(stopSprint());
    }

    IEnumerator stopSprint()
    {
        yield return new WaitForSeconds(1);
        dashMultiplier = 1;
        dash = false;
    }

    private void LevelUp()
    {
        level++;
        experienceToLevel = level * 50 + Mathf.Pow(level * 2, 2);
        Bullet.minVelocity += 1;
        listoRpm -= 0.05f;

        AddExperience(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            health--;
            healthSlider.value = health;
            if(health <= 0)
            {
                Destroy(transform.parent.gameObject);
                GameManager.Instance.GameOver();
            }
        }
    }
}
