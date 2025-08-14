using UnityEngine;
using UnityEngine.SceneManagement;

public class StartImagePush : MonoBehaviour
{
	private void OnMouseDown()
	{
		SceneManager.LoadScene("InGame");
	}
}