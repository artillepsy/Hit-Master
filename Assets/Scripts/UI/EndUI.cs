using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndUI : MonoBehaviour
    {
        [SerializeField] private Animation endCanvasAnim;

        private void Awake()
        {
            endCanvasAnim.gameObject.SetActive(false);
            
            PlayerMovement.OnFinishReach.AddListener(() =>
            {
                endCanvasAnim.gameObject.SetActive(true);
                endCanvasAnim.Play();
                Invoke(nameof(RestartLevel), endCanvasAnim.clip.length);
            });
        }

        private void RestartLevel()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}