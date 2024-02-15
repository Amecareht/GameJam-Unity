using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelLoader : MonoBehaviour
{
    public string NextLevel;

    public static Vector3 spawnPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifiez si l'objet entrant est celui que vous souhaitez d�tecter (par exemple, un joueur)
        if (other.CompareTag("Player"))
        {
            spawnPlayer = other.transform.position;

            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // V�rifie si le nom du niveau suivant est valide
        if (!string.IsNullOrEmpty(NextLevel))
        {
            // Charge le niveau suivant
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            Debug.LogWarning("Le nom du niveau suivant n'est pas d�fini dans l'�diteur Unity !");
        }
    }
    
}