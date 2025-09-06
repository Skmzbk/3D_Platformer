using UnityEngine;
using HutongGames.PlayMaker;
using DunGen.Graph;

using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace DunGen.PlayMaker.Actions
{
	[ActionCategory("DunGen")]
	[Tooltip("Generates a dungeon layout from an existing RuntimeDungeon component")]
	public class Generate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RuntimeDungeon))]
		[Tooltip("The GameObject who owns the RuntimeDungeon component you'd like to generate from")]
		public FsmOwnerDefault TargetGameObject;


		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(TargetGameObject);
			RuntimeDungeon runtimeDungeon = go.GetComponent<RuntimeDungeon>();

			if (runtimeDungeon != null)
				runtimeDungeon.Generator.Generate();
			else
				Debug.LogError("No RuntimeDungeon component is attached to this GameObject.");

			Finish();
		}
	}
}