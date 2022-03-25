using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TerrainTextureDetector))]
public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] mossClips;
    [SerializeField] private AudioClip[] mulchClips;
    [SerializeField] private AudioClip[] rockClips;
    [SerializeField] private AudioClip[] woodClips;
    [SerializeField] private AudioClip[] waterClips;


    [SerializeField] private Transform playLocation;
    [SerializeField] private float volume;
    
    [SerializeField, Range(1, 10), Tooltip("Max clips considered per each sound pool")] 
    private int maxClipsPerSoundPool = 5;

    enum ClipType
    {
        Grass,
        Moss,
        Mulch,
        Rock,
        Wood,
        Water
    }
    private TerrainTextureDetector textureDetector;


    private void Awake()
    {
        textureDetector = GetComponent<TerrainTextureDetector>();
        
    }

    void PlayFootStep()
    {
        bool meshSoundPlayed = TryPlayMeshTagClip();
        if (meshSoundPlayed) return;
        
        int textureLayerIndex = textureDetector.GetPrevalentTextureIndex();
        PlayRandomClipOfType((ClipType)textureLayerIndex);
    }
    
    /// <summary>
    /// Attempt to play a clip based on mesh tag. Return true upon success, false otherwise.
    /// </summary>
    bool TryPlayMeshTagClip()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 50f);

        if (!hit.collider) return false;
        
        string objectTag = hit.collider.gameObject.tag;
        
        switch (objectTag)
        {
            case "Wood":
                PlayRandomClipOfType(ClipType.Wood);
                return true;
            case "Water":
                PlayRandomClipOfType(ClipType.Water);
                return true;
        }

        return false;
    }

    void PlayRandomClipOfType(ClipType clipType)
    {
        AudioClip[] selectedSoundPool = new AudioClip[maxClipsPerSoundPool];

        switch (clipType)
        {
            case ClipType.Grass:
                selectedSoundPool = grassClips;
                break;
            case ClipType.Moss:
                selectedSoundPool = mossClips;
                break;
            case ClipType.Mulch:
                selectedSoundPool = mulchClips;
                break;
            case ClipType.Rock:
                selectedSoundPool = rockClips;
                break;
            case ClipType.Wood:
                selectedSoundPool = woodClips;
                break;
            case ClipType.Water:
                selectedSoundPool = waterClips;
                break;
        }

        if (selectedSoundPool.Length == 0)
            return;
        
        int randomIndex = Random.Range(0, selectedSoundPool.Length);
        AudioSource.PlayClipAtPoint(selectedSoundPool[randomIndex], playLocation.position, volume);

    }
}
