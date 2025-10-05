using System;
using UnityEngine;

namespace BlockGame.New.Core.UI
{
	public class BaseDialog : BaseUI
	{
		public Animator anim;

		public AnimationClip hidingAnimation;

		public Action onDialogCompleteClosed;

		private AnimatorStateInfo info;

		private bool isShowing;

		protected virtual void Awake()
		{
			if (anim == null)
			{
				anim = GetComponent<Animator>();
			}
			ProcessTexts();
		}

		protected virtual void Start()
		{
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !GlobalVariables.ShowingTutorial)
			{
				Close();
			}
		}

		public virtual void Show()
		{
			UnityEngine.Debug.Log("show dialog");
			UpdateLanguage();
			if (GameLogic.Instance != null)
			{
				GameLogic.Instance.State = GameLogic.GameState.Pause;
			}
			base.gameObject.SetActive(value: true);
			base.transform.SetAsLastSibling();
			if (!(anim != null))
			{
			}
		}

		public virtual void Close()
		{
			UnityEngine.Debug.Log("Close");
			if (GameLogic.Instance != null && !GlobalVariables.RestartGame)
			{
				GameLogic.Instance.State = GameLogic.GameState.Run;
			}
			if (base.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("dialog active");
				if (anim != null && hidingAnimation != null)
				{
					anim.SetTrigger("hide");
					Timer.Schedule(this, hidingAnimation.length, DoHide);
				}
			}
		}

		private void DoHide()
		{
			base.gameObject.SetActive(value: false);
		}

		private void DoClose()
		{
			UnityEngine.Object.Destroy(base.gameObject);
			if (onDialogCompleteClosed != null)
			{
				onDialogCompleteClosed();
			}
		}

		public void Hide()
		{
			base.gameObject.SetActive(value: false);
			if (!GlobalVariables.RestartGame)
			{
				GameLogic.Instance.State = GameLogic.GameState.Run;
			}
			isShowing = false;
		}

		public bool IsShowing()
		{
			return isShowing;
		}

		public virtual void OnDialogCompleteClosed()
		{
			onDialogCompleteClosed = (Action)Delegate.Remove(onDialogCompleteClosed, new Action(OnDialogCompleteClosed));
		}

		public void PlayButton()
		{
		}

		private void OnEnable()
		{
		}
	}
}
