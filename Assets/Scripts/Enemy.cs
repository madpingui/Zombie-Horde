using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    private float health;
    public float expOnDeath;
    private Text healthUI;

    private NavMeshAgent agent;
    private GameObject target;

    public Slider healthSlider;
    public Image healthColor;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        health = Random.Range((int)Time.time / 8, (int)Time.time / 2);
        if (health < 1)
        {
            health = 1;
        }
        healthUI = GetComponentInChildren<Text>();
        healthUI.text = health.ToString("");
        healthSlider.maxValue = health;
        healthSlider.value = health;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void Die()
    {
        target.GetComponent<Player>().AddExperience(expOnDeath);
        GameManager.Instance.AddScore();
        Destroy(transform.parent.gameObject);
    }

    public void Update()
    {
        healthSlider.transform.parent.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        healthSlider.transform.LookAt(Camera.main.transform.position);

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    public void healthLeft()
    {
        health--;
        healthSlider.value = health;
        healthUI.text = health.ToString("");
        if (health <= 0)
        {
            Die();
        }
    }
}
