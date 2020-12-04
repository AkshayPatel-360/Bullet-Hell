using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bullet;

    public float bulletSpeed = 2;
    private float timeBetweenShoot = 0.2f ;
    



    void Start()
    {
        bullet = Resources.Load<GameObject>("Prefabs/EggBullet");
        
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }



    void shoot()
    {
        if (Input.GetKey(KeyCode.Space))

        {
           // Vector2 randV2 = new Vector2 (Random.Range(-360, 360), Random.Range(-360, 360));

            Vector2 randV2 = Random.insideUnitCircle.normalized * 5;
            if (timeBetweenShoot <=0)
            {

                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

                newBullet.GetComponent<Rigidbody2D>().velocity =randV2.normalized*bulletSpeed * Time.deltaTime;


                timeBetweenShoot = 0.2f;

            }
           
            timeBetweenShoot -= Time.deltaTime;

        }
    }


    
}
