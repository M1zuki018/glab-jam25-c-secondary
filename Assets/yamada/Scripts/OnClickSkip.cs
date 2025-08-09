using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ʂ��N���b�N������Scene��Load
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
