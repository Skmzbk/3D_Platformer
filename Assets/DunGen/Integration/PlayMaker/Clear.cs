using UnityEngine;
using HutongGames.PlayMaker;
using DunGen.Graph;

using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace DunGen.PlayMaker.Actions
{
	[ActionCategory("DunGen")]
	[Tooltip("Clears the dungeon layout from an existing RuntimeDungeon component")]
	public class Clear : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RuntimeDungeon))]
		[Tooltip("The GameObject who owns the RuntimeDungeon component you'd like the clear")]
		public FsmOwnerDefault TargetGameObject;


		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(TargetGameObject);
			RuntimeDungeon runtimeDungeon = go.GetComponent<RuntimeDungeon>();

			if (runtimeDungeon != null)
				runtimeDungeon.Generator.Clear(true);
			else
				Debug.LogError("No RuntimeDungeon component is attached to this GameObject.");

			Finish();
		}
	}
}