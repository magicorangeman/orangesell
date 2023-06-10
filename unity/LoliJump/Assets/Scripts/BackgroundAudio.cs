using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string createdTag;
    
    private void Awake()
    {
        GameObject tag = GameObject.FindWithTag(this.createdTag);
        if (tag != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.createdTag;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
