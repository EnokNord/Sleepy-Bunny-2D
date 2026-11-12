using UnityEngine;
using UnityEngine.SceneManagement;
using static Global.GlobalVariablesLibrary;


    [RequireComponent(typeof(BoxCollider2D))]
    public class NewLevelTrigger : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;
        private void OnValidate()
        {
            //Only sets the value if not null
            _boxCollider2D ??= GetComponent<BoxCollider2D>();
            if (!_boxCollider2D.isTrigger) _boxCollider2D.isTrigger = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != PlayerLayer) return;
            SavefileManager savefileManager = FindAnyObjectByType<SavefileManager>();
        if (savefileManager != null)
        {
            savefileManager.CurrentSave.levelCheckpoints[SceneManager.GetActiveScene().buildIndex] = 0;
            savefileManager.SaveGame();
        }
            LevelFunctionsLibrary.LevelFunctions.LoadNextLevel();
        }
    }