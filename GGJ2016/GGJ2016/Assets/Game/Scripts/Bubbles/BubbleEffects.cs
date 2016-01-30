using UnityEngine;

using System;
using System.Collections;

using Common;
using Common.Utils;

namespace Game {

	public class BubbleEffects : MonoBehaviour {

		[SerializeField]
		private GameObject bubblePopTemplate;

		private void Awake() {
			Assertion.AssertNotNull(this.bubblePopTemplate);
		}

		private void Start() {
			Factory.Register<BubbleEffects, BubbleEffects>(this);
		}

		private void OnDestroy() {
			Factory.Clean<BubbleEffects>();
		}

		public void Pop(Vector3 position) {
			this.StartCoroutine(this.ShowPopEffect(position));
		}

		private IEnumerator ShowPopEffect(Vector3 position) {
			var prefab = Instantiate(this.bubblePopTemplate, position, Quaternion.identity) as GameObject;
			yield return null;
		}

	}

}