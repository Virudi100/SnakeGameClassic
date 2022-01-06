using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnFood : MonoBehaviour
{
    private bool aSpawn = false;
    [SerializeField] private GameObject foodPrefab;
    private GameObject newFood;


    private void Update()
    {
        objectSpawn();
    }

    void objectSpawn()
    {
        //Génère une position aléatoire et créer un nouvelle objet
        Vector3 randomPosition = GetRandomposition();
        if (aSpawn == false)
        {
            newFood = Instantiate<GameObject>(foodPrefab, randomPosition, Quaternion.identity);
            aSpawn = true;
        }
    }
    
    public  Vector3 GetRandomposition()
    {
        //Stock le max et min de la surface 
        float minX = -13f;
        float maxX = 13f;
        float minY = -7f;
        float maxY = 6f;
        
        //Retourne une valeur à utilisé pour généré l'objet
        Vector3 newVec = new Vector3(UnityEngine.Random.Range (minX, maxX), Random.Range(minY,maxY),0);
        
        return newVec;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            aSpawn = false;
            this.GetComponent<Player>().currentScore += 1;
            Debug.Log("score +1");
            Destroy(other.gameObject);
        }
    }
}
