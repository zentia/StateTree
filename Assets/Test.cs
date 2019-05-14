using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public RawImage mRawImage;
    public void LoadTexture()
    {
        mRawImage.texture = Resources.Load<Texture2D>("Cinema Suite Monochrome");
    }
}
