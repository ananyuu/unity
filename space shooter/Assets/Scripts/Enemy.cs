using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 1;

    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            //Player player선언후 star문에 getcomponent<Player>시 충돌해도 개체 안 없어짐 other.transform에 주의!
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.DamageSystem();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            if(_player  != null)
            {
                _player.AddScore(10);
            }
            Destroy(other.gameObject);
        }
    }
}