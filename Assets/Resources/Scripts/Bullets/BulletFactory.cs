using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory 
{
    #region Singleton
    private static BulletFactory instance;

    public static BulletFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletFactory();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<BulletTypes, GameObject> bulletPrefabDict;

    string bulletPrefabPath = "Prefabs/Bullet/";

    //string[] bulletNames;
    int bulletLayer;

    private BulletFactory()
    {
        bulletLayer = LayerMask.NameToLayer("Bullet");
    }

    public BulletTypes GetRandomBulletType()
    {
       
        int randNum = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BulletTypes)).Length);
        return (BulletTypes)randNum;

       // return bulletNames[Random.Range(0, bulletNames.Length)];
    }

   

    public void InitializeFactory()
    {

        //Loads all prefabs into the factory

        bulletPrefabDict = new Dictionary<BulletTypes, GameObject>();

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(bulletPrefabPath);
        //Bullet[] allBullets = Resources.LoadAll<Bullet>(bulletPrefabPath);
        foreach (GameObject prefab in allPrefabs)
        {

            Bullet newBullet = prefab.GetComponent<Bullet>();
            if (newBullet)
            {
                newBullet.GetComponent<SpriteRenderer>().sortingLayerName = "Bullet";

                Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.gravityScale = 0;

                bulletPrefabDict.Add(newBullet.types, prefab); 
            }

            else
            {
                Debug.Log("A prefab inside of Assets/Resources/" + bulletPrefabPath + " did not have a bullet script, only have bullet prefabs in this folder! prefab name: " + prefab.name);

            }

        }

    }

    
 

    public Bullet CreateBullet(BulletTypes type,Vector2 pos)
    {
        Vector2 randV2 = UnityEngine.Random.insideUnitCircle.normalized * 5;

        GameObject toRetObj = null;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(type);
        if (poolable != null)
        {
            toRetObj = poolable.GetGameObject;
        }
        else
            toRetObj = _CreateBullet(type).gameObject;
        Bullet toRet = toRetObj.GetComponent<Bullet>();
        if (!toRet)
        {


            Debug.LogError("Something went wrong in bullet factory, object: " + toRetObj.name + " did not contain a bullet script. Returning Null");
        }
        else
        {
            toRet.transform.position = pos;

            BulletManager.Instance.Addbullet(toRet);
        }

       

        return toRet;
       
    }


    private Bullet _CreateBullet(BulletTypes bulletTypes)

    {
        if (!bulletPrefabDict.ContainsKey(bulletTypes))
        {

            Debug.LogError("Bullet of name: " + bulletTypes + " not found in bulletDict");
            return null;
        }

        GameObject newBulletObj = GameObject.Instantiate(bulletPrefabDict[bulletTypes]);

        Bullet newBullet = newBulletObj.GetComponent<Bullet>();

        return newBullet;



    }

        /*public Bullet CreateNewBulletNormal(BulletTypes types, Vector3 position) ///////////////////////////////////////
        {
            Bullet newBullet = GameObject.Instantiate(bulletPrefabDict[types], position, Quaternion.identity);
            return newBullet;

        }

        public Bullet CreateBullet(Vector3 position,BulletTypes types,Vector2 direction)
        {
            Bullet newBullet = null;
            BulletTypes type = GetRandomBulletType();

            IPoolable bulletStack = ObjectPool.Instance.RetrieveFromPool(type);

            if (bulletStack != null)
            {

                newBullet = bulletStack.GetGameObject.GetComponent<Bullet>();
                newBullet.transform.position = position;

            }

            else
                newBullet = CreateNewBulletNormal(type, position);

            return newBullet;


        }*/
 

}
