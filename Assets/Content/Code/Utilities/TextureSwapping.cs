using UnityEngine;

using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class TextureSwapping : MonoBehaviour
{
    [SerializeField]
    private List<Texture> _textures = new List<Texture>();
    [SerializeField]
    private int _materialIndex = 0;

    [SerializeField, Range(0, 30)]
    private float _minSwapTime = 3f;

    [SerializeField, Range(0, 30)]
    private float _maxSwapTime = 5f;

    [SerializeField]
    private string _sourceTextureName = "_MainTexture";
    [SerializeField]
    private string _textureName = "_EmissionMap";

    [SerializeField]
    private float _timer = 0f;

    private Material _originalUnmodifiedMaterial = null;
    private Material _modifiedMaterialInstance = null;

    private Renderer _meshRenderer;

    private void Start()
    {
        Initialize();
    }

    protected void Update()
    {
        SetRandomTexture();
    }

    protected void OnDestroy ()
    {
        RestoreOriginalMaterial();
    }
    
    private void OnValidate()
    {
        if (_maxSwapTime < _minSwapTime)
        {
            _minSwapTime = _maxSwapTime;
        }

        if (_materialIndex < 0)
        {
            _materialIndex = 0;
        }

        if (_meshRenderer != null)
        {
            if (_materialIndex > _meshRenderer.sharedMaterials.Length - 1)
            {
                _materialIndex = _meshRenderer.sharedMaterials.Length - 1;
            }
        }
    }

    private void Initialize ()
    {
        _meshRenderer = GetComponent<Renderer>();
        _timer = RandomizeInterval();

        SetupSwappedMaterial();
    }

    private void SetRandomTexture ()
    {
        Texture texture = ((SpriteRenderer)_meshRenderer).sprite.texture;

        if (_modifiedMaterialInstance == null)
        {
            SetupSwappedMaterial();
        }

        _modifiedMaterialInstance.SetTexture(_textureName, texture);
    }

    private void RestoreOriginalMaterial()
    {
        if (_modifiedMaterialInstance != null)
        {
            Destroy(_modifiedMaterialInstance);
            SetObjectMaterial(_meshRenderer, _materialIndex, _originalUnmodifiedMaterial);
        }
    }

    private void SetupSwappedMaterial()
    {
        // destroy old material if it exists
        if (_modifiedMaterialInstance != null)
        {
            Destroy(_modifiedMaterialInstance);
        }
        
        // create material instance used to modify textures without creating new material instances
        _originalUnmodifiedMaterial = _meshRenderer.sharedMaterials[_materialIndex];
        _modifiedMaterialInstance = CreateMaterialInstance();

        // set new material collection with modified material
        SetObjectMaterial(_meshRenderer, _materialIndex, _modifiedMaterialInstance);
    }

    private void SetObjectMaterial (Renderer renderer, int materialIndex, Material materialToSet)
    {
        Material[] objectMaterials = _meshRenderer.sharedMaterials;

        objectMaterials[_materialIndex] = _modifiedMaterialInstance;

        _meshRenderer.sharedMaterials = objectMaterials;
    }

    private Material CreateMaterialInstance ()
    {
        Material newMaterial = new Material(_originalUnmodifiedMaterial);

        newMaterial.CopyPropertiesFromMaterial(_originalUnmodifiedMaterial);

        return newMaterial;
    }

    private float RandomizeInterval()
    {
        return Random.Range(_minSwapTime, _maxSwapTime);
    }

    private Texture GetRandomTexture()
    {
        int index = Random.Range(0, _textures.Count);
        return _textures[index];
    }
}
