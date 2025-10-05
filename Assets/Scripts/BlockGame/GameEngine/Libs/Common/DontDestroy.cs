using UnityEngine;

namespace BlockGame.GameEngine.Libs.Common
{
	public class DontDestroy : MonoBehaviour
	{
		private void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}
	}
}
