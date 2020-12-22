using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bullet;
    BulletFactory bf;
    //public Bullet b =  new Bullet();
   // private Transform playerPos;
    private float timeBetweenShoot = 1f ;

    Dictionary<BulletTypes, GameObject> alreadyLoadedBullets = new Dictionary<BulletTypes, GameObject>();



    void Start()
    {
        BulletFactory.Instance.InitializeFactory();
        BulletManager.Instance.UpdateBulletManager();
       


    }

    // Update is called once per frame
    void Update()
    {
        BulletManager.Instance.UpdateBulletManager();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }
        
    }



    void shoot()
    {
        Vector2 randV2 = Random.insideUnitCircle.normalized * 5;
        

        Bullet newBullet = BulletFactory.Instance.CreateBullet(BulletFactory.Instance.GetRandomBulletType(), transform.position);

      

        newBullet.rb.velocity = randV2   ;
        
    }


    
}





















/*
if (timeBetweenShoot <= 0)
{

    GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

    newBullet.GetComponent<Rigidbody2D>().velocity = randV2.normalized * bulletSpeed * Time.deltaTime;


    timeBetweenShoot = 0.2f;

}

timeBetweenShoot -= Time.deltaTime;*/