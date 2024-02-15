using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpawnJoueur : MonoBehaviour
{
    private void Start()
    {
        // Assurez-vous que le joueur existe dans la scène
        GameObject joueur = GameObject.FindGameObjectWithTag("Player");
        if (joueur != null)
        {
            // Définir la position du joueur sur le point d'apparition
            joueur.transform.position = S_LevelLoader.spawnPlayer;
        }
        else
        {
            Debug.Log("pas de joueur");
        }
            
    }
}
