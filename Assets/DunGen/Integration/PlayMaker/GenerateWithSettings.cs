using UnityEngine;
using HutongGames.PlayMaker;
using DunGen.Graph;

using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace DunGen.PlayMaker.Actions
{
	[ActionCategory("DunGen")]
	[Tooltip("Generates a new dungeon layout with specific settings")]
	public class GenerateWithSettings : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject who owns the RuntimeDungeon component you'd like to generate from")]
		public FsmOwnerDefault TargetGameObject;

		[RequiredField]
		[Tooltip("The DungeonFlow to use")]
		public DungeonFlow DungeonFlow;

		[RequiredField]
		[Tooltip("Should the genration seed be randomized each time, producing a different result for the dungeon?")]
		public FsmBool RandomizeSeed;

		[Tooltip("The seed used to generate the dungeon layout. If none, a random seed will be used each time")]
		public FsmInt Seed;

		[RequiredField]
		[Tooltip("The maximum number of times a dungeon can fail to generate - only active in the editor to prevent infinite loops. In a packaged game, the generator will keep trying indefinitely")]
		public FsmInt MaxFailedAttempts;

		[RequiredField]
		[Tooltip("Used to modify the main path length specified in the DungeonFlow asset. (1 = Normal Size, 2 = Double Size, 0.5 = Half Size, etc)")]
		public FsmFloat LengthMultiplier;

		[RequiredField]
		[Tooltip("Should sprites be ignored when automatically calculating bounding boxes around tiles? Should be unchecked for 2D dungeons")]
		public FsmBool IgnoreSpriteBounds;

		[RequiredField]
		[ObjectType(typeof(AxisDirection))]
		[Tooltip("The up direction for this dungeon. In most cases, this will be +Y for 3D dungeons and -Z for 2D dungeons")]
		public FsmEnum UpDirection;

		[RequiredField]
		[Tooltip("If checked, useful debug information will be rendered ontop of the dungeon in play mode")]
		public FsmBool DebugRender;

		[RequiredField]
		[Tooltip("If checked, placed trigger colliders around each room which can be used in conjunction with the DungenCharacter component to recieve events when changing rooms")]
		public FsmBool PlaceTileTriggers;

		[RequiredField]
		[Tooltip("The layer to place the tile root objects on if \"Place Tile Triggers\" is checked")]
		public FsmInt TriggerLayer;


		public override void Reset()
		{
			DungeonFlow = null;
			RandomizeSeed = true;
			Seed = null;
			MaxFailedAttempts = 20;
			LengthMultiplier = 1.0f;
			IgnoreSpriteBounds = true;
			UpDirection = AxisDirection.PosY;
			DebugRender = false;
			PlaceTileTriggers = true;
			TriggerLayer = 2; // "Ignore Raycast" layer
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(TargetGameObject);
			RuntimeDungeon runtimeDungeon = go.GetComponent<RuntimeDungeon>();

			if (runtimeDungeon == null)
			{
				runtimeDungeon = go.AddComponent<RuntimeDungeon>();
				runtimeDungeon.GenerateOnStart = false;
			}

			var generator = runtimeDungeon.Generator;
			generator.DungeonFlow = DungeonFlow;
			generator.ShouldRandomizeSeed = RandomizeSeed.Value || Seed.IsNone;

			if (!generator.ShouldRandomizeSeed)
				generator.Seed = Seed.Value;

			generator.MaxAttemptCount = MaxFailedAttempts.Value;
			generator.LengthMultiplier = LengthMultiplier.Value;
			generator.IgnoreSpriteBounds = IgnoreSpriteBounds.Value;
			generator.UpDirection = (AxisDirection)UpDirection.Value;
			generator.DebugRender = DebugRender.Value;
			generator.PlaceTileTriggers = PlaceTileTriggers.Value;
			generator.TileTriggerLayer = TriggerLayer.Value;

			generator.Generate();

			Finish();
		}
	}
}