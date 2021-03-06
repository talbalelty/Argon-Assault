using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameObject crashExplosionFX;

    GameObject parentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartCrashSequence());
    }

    IEnumerator StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        GameObject fx = Instantiate(crashExplosionFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;

        // depends on the ship Prefab, the ship and it's colliders will disapear leaving the explosion behind
        MeshRenderer shipRenderer;
        if (TryGetComponent<MeshRenderer>(out shipRenderer))
        {
            shipRenderer.enabled = false;
        }
        else
        {
            MeshRenderer[] shipRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in shipRenderers)
            {
                renderer.enabled = false;
            }
        }
        Collider shipCollider;
        if (TryGetComponent<Collider>(out shipCollider))
        {
            shipCollider.enabled = false;
        }
        else
        {
            Collider[] shipColliders = GetComponentsInChildren<Collider>();
            foreach (Collider collider in shipColliders)
            {
                collider.enabled = false;
            }
        }

        yield return new WaitForSeconds(loadDelay);
        ReloadLevel();
    }

    private void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
