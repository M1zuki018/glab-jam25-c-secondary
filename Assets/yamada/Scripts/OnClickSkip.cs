using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ‰æ–Ê‚ğƒNƒŠƒbƒN‚µ‚½‚çScene‚ğLoad
/// </summary>
public class OnClickSkip : MonoBehaviour
{
    [SerializeField] private SceneLoader _loader; 

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _loader.LoadScene("InGame");
        }
    }
}
