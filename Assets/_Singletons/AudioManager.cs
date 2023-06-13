using UnityEngine;
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get => instance; set => instance = value; }
    public AssetSounds[] Sounds { get => sounds; set => sounds = value; }
    [SerializeField] AssetSounds[] sounds;
    private float musicVolume;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void PlaySound(AudioSource source,string clipName)
    {
        foreach(AssetSounds sound in sounds)
        {
            if(sound.clipName == clipName)
            {
                source.clip = sound.clip;
                source.Play();
            }
        }
    }
    public void StopSound(AudioSource source)
    {
       if(source.isPlaying)
       {
            Debug.Log("Stop");
            source.Stop();
       } 
    }
}
