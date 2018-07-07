using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] private Image _image = null;
    [SerializeField] private Color _a = new Color();
    [SerializeField] private Color _b = new Color();

    private void Awake()
    {
        _image.color = _a;
    }

    public void LerpColor(float fill)
    {
        _image.color = Color.Lerp(_a, _b, 1 - fill);
    }
}
