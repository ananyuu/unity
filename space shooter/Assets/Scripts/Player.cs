using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float speedMultiplier = 2;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;

    [SerializeField] private float fireRate = 0.1f;
    private float canFire = -1f;

    [SerializeField] private int life = 5;
    
    private SpawnManager spawnManager;

    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;

    [SerializeField] private GameObject shieldVisualizer;

    [SerializeField] private int score;

    private UIManager uiManager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Object").GetComponent<SpawnManager>();

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (spawnManager == null)
        {
            Debug.LogError("Spawn Enemy is Null");
        }

        if(uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser(/*laserPrefab*/);
        }
        //else if (Input.GetKeyDown(KeyCode.C) && Time.time > canFire)
        //{
        //    FireLaser(tripleShotPrefab);
        //}
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(Time.deltaTime * speed * direction);


        //Mathf.Clamp-> 최소, 최대사이의 값 ex) Mathf.Clamp(transform.position.x,0,1)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), transform.position.y, 0);

        //if(transform.position.x > 11.3f)
        //{
        //    transform.position = new Vector3(-11.3f, transform.position.y, 0);
        //}
        //else if (transform.position.x < -11.3f)
        //{
        //    transform.position = new Vector3(11.3f, transform.position.y, 0);
        //}

    }

    void FireLaser(/*GameObject prefab*/)
    {
        canFire = Time.time + fireRate;

        if (isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }


    }

    public void DamageSystem()
    {
        if(isShieldActive == true)
        {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        life--;
        uiManager.UpdateLifeScore(life);
        if (life < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        
        

    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostDownActive());
    }

    IEnumerator SpeedBoostDownActive()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldDownActive());
    }

    IEnumerator ShieldDownActive()
    {
        yield return new WaitForSeconds(5f);
        isShieldActive = false;
        shieldVisualizer.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }
}
