public class AutoSprite : UnityEngine.MonoBehaviour
{
    public float animSpeed = 1.0f;
    public bool DestroyAtEnd = false;
    public bool DiactiveAtEnd = false;
    public bool clampAnim = false;
    public SpriteSheet sheet;
    public UnityEngine.AudioSource audioSource;
    public void Awake()
    {
        if(sheet==null)
        {
            sheet = GetComponent<SpriteSheet>();
        }
        if(enabled)
        {
            sheet.init();
            sheet.AddAnim("play", sheet._sprites.Length, animSpeed, clampAnim);
            sheet.Play("play");
            sheet.AddAnimationEvent("play", -1, () => PlayEnded());            
        }        
    }

    void PlayEnded()
    {
        if (DestroyAtEnd)
        {
            Destroy(gameObject);
        }

        if (DiactiveAtEnd)
        {
            gameObject.SetActive(false);
        }                
    }
}
