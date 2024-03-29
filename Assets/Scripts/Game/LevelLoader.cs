using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Myorudo.Game
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private Animator _transition;
        [SerializeField]
        private float _transitionTime = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        public void LoadSpecificLevel(int buildIndex)
        {
            StartCoroutine(LoadLevel(buildIndex));
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            _transition.SetTrigger("StartTransition");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(levelIndex);
        }



    }
}