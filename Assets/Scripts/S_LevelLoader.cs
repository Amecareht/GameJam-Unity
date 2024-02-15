using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelLoader : MonoBehaviour
{
    public string NextLevel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifiez si l'objet entrant est celui que vous souhaitez détecter (par exemple, un joueur)
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // Vérifie si le nom du niveau suivant est valide
        if (!string.IsNullOrEmpty(NextLevel))
        {
            // Charge le niveau suivant
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            Debug.LogWarning("Le nom du niveau suivant n'est pas défini dans l'éditeur Unity !");
        }
    }
}