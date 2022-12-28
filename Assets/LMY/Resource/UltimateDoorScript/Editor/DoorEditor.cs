using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Door))]
public class DoorEditor : Editor
{
	Door door = null;
	
	public void OnEnable()
    {
		door = (Door)target;
	}
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if (door.animationType == Door.AnimationType.ScriptAnimation)
		{
			door.openSpeed = EditorGUILayout.Slider("Open/Close Speed:", door.openSpeed, 0.000001f, 100);
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		if (door.animationType == Door.AnimationType.ScriptAnimation)
		{
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Movement");
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginVertical();
				door.useModelOrigin = EditorGUILayout.Toggle("Use Models Origin:", door.useModelOrigin);
				door.openType = (Door.OpenType)EditorGUILayout.EnumPopup("How to Open:", door.openType);
			EditorGUILayout.EndVertical();
			
			if (door.openType == Door.OpenType.OpenBack || door.openType == Door.OpenType.OpenLeft ||
				door.openType == Door.OpenType.OpenFront || door.openType == Door.OpenType.OpenRight)
			{
				EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Rotation Amounts");
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginVertical();
					door.openedY_Rotation = EditorGUILayout.Slider("Opened Y Rotation:", door.openedY_Rotation, -360, 360, null);
					door.closedY_Rotation = EditorGUILayout.Slider("Closed Y Rotation:", door.closedY_Rotation, -360, 360, null);
				EditorGUILayout.EndVertical();
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Slide Amounts");
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginVertical();
					door.openedSlideDistance = EditorGUILayout.Slider("Opened Slide Distance:", door.openedSlideDistance, 0.000001f, 200, null);
					door.closedSlideDistance = EditorGUILayout.Slider("Closed Slide Distance:", door.closedSlideDistance, 0.000001f, 200, null);
				EditorGUILayout.EndVertical();
			}
		}
		else if (door.animationType == Door.AnimationType.FileAnimation)
		{
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Animations");
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginVertical();
				door.fileAnimationOpen = EditorGUILayout.TextField("Intro Animation Name:", door.fileAnimationOpen);
				door.idleOpenAnimation = EditorGUILayout.TextField("Open Idle Animation:", door.idleOpenAnimation);
				door.fileAnimationClose = EditorGUILayout.TextField("Outro Animation Name:", door.fileAnimationClose);
			EditorGUILayout.EndVertical();
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Interact With");
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginVertical();
			door.restrictedTo = (Door.TypeOfDetection)EditorGUILayout.EnumPopup("What Opens It:", door.restrictedTo);
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		switch (door.restrictedTo)
		{
			case Door.TypeOfDetection.GameObject:
				door.checkAgainstObject = (GameObject)EditorGUILayout.ObjectField("Required GameObject: ", door.checkAgainstObject, typeof(GameObject), true);
				break;
			case Door.TypeOfDetection.ScriptType:
				door.classType = EditorGUILayout.TextField("Name of Script:", door.classType);
				break;
		}
		
		EditorUtility.SetDirty(door);
		
		float maxRange = 100;
		
		door.range = EditorGUILayout.Slider("Range before open:", door.range, 0, maxRange, null);
		door.rangeOffset = EditorGUILayout.Vector3Field("Range Offset Position:", door.rangeOffset);
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Sound Effects");
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginVertical();
			door.openSound = (AudioClip)EditorGUILayout.ObjectField("Opening Sound:", door.openSound, typeof(AudioClip), true);
			door.fullyOpenedSound = (AudioClip)EditorGUILayout.ObjectField("Fully Opened Sound:", door.fullyOpenedSound, typeof(AudioClip), true);
			door.closeSound = (AudioClip)EditorGUILayout.ObjectField("Closing Sound:", door.closeSound, typeof(AudioClip), true);
			door.fullyClosedSound = (AudioClip)EditorGUILayout.ObjectField("Fully Closed Sound:", door.fullyClosedSound, typeof(AudioClip), true);
		EditorGUILayout.EndVertical();
		
		if (door.animationType == Door.AnimationType.ScriptAnimation)
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Animations");
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginVertical();
				door.introAnimation = EditorGUILayout.TextField("Intro Animation Name:", door.introAnimation);
				door.finishedOpenAnimation = EditorGUILayout.TextField("Finished Opening Animation Name:", door.finishedOpenAnimation);
				door.idleOpenAnimation = EditorGUILayout.TextField("Open Idle Animation Name:", door.idleOpenAnimation);
				door.outroAnimation = EditorGUILayout.TextField("Outro Animation Name:", door.outroAnimation);
				door.finishedCloseAnimation = EditorGUILayout.TextField("Finished Closing Animation Name:", door.finishedCloseAnimation);
			EditorGUILayout.EndVertical();
		}
	}
	
	private void OnDrsawGizmos()
	{
		door.DrawGizmos();
	}
}