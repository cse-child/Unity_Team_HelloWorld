using UnityEngine;
using System.Collections;

public class ChildDoor : Door
{
	protected override void Awake()
	{
		isChild = true;
	}
}

public class Door : MonoBehaviour
{
	public enum OpenType
	{
		OpenLeft,
		OpenRight,
		OpenFront,
		OpenBack,
		SlidePosX,
		SlideNegX,
		SlidePosZ,
		SlideNegZ,
		slidePosY,
		slideNegY
	};
	
	public enum TypeOfDetection
	{
		GameObject,
		ScriptType,
		Any
	};
	
	public enum AnimationType
	{
		FileAnimation,
		ScriptAnimation
	};
	
	public AnimationType animationType		= AnimationType.ScriptAnimation;
	
	public bool drawGizmos					= true;
	public bool locked						= false;
	public bool openOnly					= false;
	private bool alreadyOpen				= false;
	private bool lockOpen					= false;
	
	[HideInInspector]
	public bool useModelOrigin				= false;
	[HideInInspector]
	public OpenType openType				= OpenType.OpenLeft;
	[HideInInspector]
	public TypeOfDetection restrictedTo		= TypeOfDetection.GameObject;
	
	public enum DoorState
	{
		None,
		StartOpen,
		Opening,
		Opened,
		StartClose,
		Closing,
		Closed
	};
	
	[HideInInspector]
	public DoorState doorState = DoorState.None;
	
	public DoorState Opened { get { return doorState; } }
	
	[HideInInspector]
	public GameObject checkAgainstObject	= null;
	
	[HideInInspector]
	public string classType					= string.Empty;
	
	[HideInInspector]
	public float range						= 0;
	
	[HideInInspector]
	public bool isChild						= false;
	private GameObject trigger				= null;
	
	[HideInInspector]
	public float openedY_Rotation			= 90;
	[HideInInspector]
	public float closedY_Rotation			= 0;
	[HideInInspector]
	public float openedSlideDistance		= 0;
	[HideInInspector]
	public float closedSlideDistance		= 0;
	
	[HideInInspector]
	public float openSpeed					= 1;
	
	[HideInInspector]
	public Vector3 rangeOffset				= Vector3.zero;
	
	private Vector3 offsetPosition			= Vector3.zero;
	private float keepTrack					= 0;
	
	[HideInInspector]
	public AudioClip openSound				= null;
	[HideInInspector]
	public AudioClip fullyOpenedSound		= null;
	[HideInInspector]
	public AudioClip closeSound				= null;
	[HideInInspector]
	public AudioClip fullyClosedSound		= null;
	
	[HideInInspector]
	public string introAnimation			= string.Empty;
	[HideInInspector]
	public string finishedOpenAnimation		= string.Empty;
	[HideInInspector]
	public string outroAnimation			= string.Empty;
	[HideInInspector]
	public string finishedCloseAnimation	= string.Empty;
	
	[HideInInspector]
	public string idleOpenAnimation			= string.Empty;
	
	[HideInInspector]
	public string fileAnimationOpen			= string.Empty;
	[HideInInspector]
	public string fileAnimationClose		= string.Empty;
	private bool startedOpenAnimation		= false;
	private bool startedCloseAnimation		= false;
	
	[HideInInspector]
	public Transform mainChild				= null;
	
	protected virtual void Awake()
	{
		if (transform.GetComponent<MeshFilter>() != null && transform.GetComponent<MeshFilter>().mesh != null)
		{
			Mesh m = transform.GetComponent<MeshFilter>().mesh;
			if (m != null && !useModelOrigin)
			{
				float modifyer = 0;
				
				if (openType == OpenType.OpenFront || openType == OpenType.OpenBack)
					modifyer = (transform.GetComponent<MeshFilter>().mesh.bounds.size.z * transform.localScale.z) * 0.5f;
				else
					modifyer = (transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x) * 0.5f;
				
				switch (openType)
				{
					case OpenType.OpenLeft:
						offsetPosition = -transform.right * modifyer;
						break;
					case OpenType.OpenRight:
						offsetPosition = transform.right * modifyer;
						break;
					case OpenType.OpenFront:
						offsetPosition = transform.forward * modifyer;
						break;
					case OpenType.OpenBack:
						offsetPosition = -transform.forward * modifyer;
						break;
					default:
						offsetPosition = Vector3.zero;
						break;
				}
			}
		}
		
		trigger = new GameObject("DoorRange");
		trigger.transform.position = transform.position + rangeOffset;
		//SphereCollider sc = trigger.AddComponent("SphereCollider") as SphereCollider;
                SphereCollider sc = trigger.AddComponent<SphereCollider>() as SphereCollider;
		trigger.GetComponent<Collider>().isTrigger = true;
		Door childDoor = trigger.AddComponent<ChildDoor>();
		childDoor.isChild = true;
		sc.radius = range;
		childDoor.range = range;
		trigger.GetComponent<ChildDoor>().restrictedTo = restrictedTo;
		
		if (transform.parent != null)
			trigger.transform.parent = transform.parent;
		
		childDoor.mainChild = transform;

		mainChild = trigger.transform;
		
		if ((openType == OpenType.OpenBack || openType == OpenType.OpenLeft ||
			openType == OpenType.OpenFront || openType == OpenType.OpenRight))
		{
				if (openedY_Rotation < 0)
					openSpeed *= -1;
		}
		else
		{
			// Slow down to match rotation
			openSpeed *= 0.0113f;
		}
	}
	
	private void Update()
	{
		if (!isChild)
		{
			if (transform.GetComponent<MeshFilter>() != null && transform.GetComponent<MeshFilter>().mesh != null && !useModelOrigin)
			{
				float modifyer = 0;
				
				if (openType == OpenType.OpenFront || openType == OpenType.OpenBack)
					modifyer = (transform.GetComponent<MeshFilter>().mesh.bounds.size.z * transform.localScale.z) * 0.5f;
				else
					modifyer = (transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x) * 0.5f;
				
				switch (openType)
				{
					case OpenType.OpenLeft:
						offsetPosition = -transform.right * modifyer;
						break;
					case OpenType.OpenRight:
						offsetPosition = transform.right * modifyer;
						break;
					case OpenType.OpenFront:
						offsetPosition = transform.forward * modifyer;
						break;
					case OpenType.OpenBack:
						offsetPosition = -transform.forward * modifyer;
						break;
					default:
						offsetPosition = Vector3.zero;
						break;
				}
			}
			
			if ((doorState == DoorState.StartOpen || doorState == DoorState.Opening || doorState == DoorState.Opened) ||
				lockOpen && (!openOnly || !alreadyOpen))
			{
				Open();
				
				if (openOnly)
					lockOpen = true;
			}
			else if ((doorState == DoorState.StartClose || doorState == DoorState.Closing || doorState == DoorState.Closed) &&
				!openOnly)
			{
				Close();
			}
		}
	}
	
	private enum SoundClipState
	{
		None,
		Opening,
		Closing,
		Opened,
		Closed
	};
	
	private SoundClipState soundClipState = SoundClipState.None;
	
	private void PlaySoundClip(SoundClipState scs)
	{
		soundClipState = scs;
		
		switch (soundClipState)
		{
			case SoundClipState.Opening:
				if (openSound != null)
					AudioSource.PlayClipAtPoint(openSound, transform.position, 1);
				break;
			case SoundClipState.Opened:
				if (fullyOpenedSound != null)
					AudioSource.PlayClipAtPoint(fullyOpenedSound, transform.position);
				break;
			case SoundClipState.Closing:
				if (closeSound != null)
					AudioSource.PlayClipAtPoint(closeSound, transform.position);
				break;
			case SoundClipState.Closed:
				if (fullyClosedSound != null)
					AudioSource.PlayClipAtPoint(fullyClosedSound, transform.position);
				break;
		}
	}
	
	private void Open()
	{
		if (!locked)
		{
			if (animationType == AnimationType.ScriptAnimation)
			{
				switch (doorState)
				{
					case DoorState.StartOpen:
						if (keepTrack == closedY_Rotation)
						{
							if (!string.IsNullOrEmpty(introAnimation.Trim()) && !startedOpenAnimation)
							{
								if (!GetComponent<Animation>().isPlaying)
								{
									GetComponent<Animation>().Blend(introAnimation);
									startedOpenAnimation = true;
								}
							}
							else
							{
								bool nextState = false;
							
								if (string.IsNullOrEmpty(introAnimation.Trim()))
									nextState = true;
								else
								{
									if (!GetComponent<Animation>().IsPlaying(introAnimation))
										nextState = true;
								}
							
								if (nextState)
								{
									startedOpenAnimation = false;
									doorState = DoorState.Opening;
								}
							}
						}
						else
							doorState = DoorState.Opening;
						break;
					case DoorState.Opening:
						if (openType == OpenType.OpenBack || openType == OpenType.OpenLeft ||
							openType == OpenType.OpenFront || openType == OpenType.OpenRight)
						{
							if (keepTrack != openedY_Rotation)
							{
								if (soundClipState != SoundClipState.Opening)
									PlaySoundClip(SoundClipState.Opening);
								
								transform.RotateAround(transform.position + offsetPosition, transform.up, openSpeed);
								
								keepTrack += openSpeed;
								
								if ((openSpeed > 0 && keepTrack >= openedY_Rotation) || (openSpeed < 0 && keepTrack <= openedY_Rotation))
								{
									float backStep = keepTrack - openedY_Rotation;
									transform.RotateAround(transform.position + offsetPosition, transform.up, backStep);
									
									keepTrack = openedY_Rotation;
									
									if (soundClipState != SoundClipState.Opened)
										PlaySoundClip(SoundClipState.Opened);
									
									if (openOnly)
										alreadyOpen = true;
									
									if (!string.IsNullOrEmpty(finishedOpenAnimation.Trim()))
										GetComponent<Animation>().Blend(finishedOpenAnimation);
								
									doorState = DoorState.Opened;
								}
							}
						}
						else
						{
							if (keepTrack != openedSlideDistance)
							{
								if (soundClipState != SoundClipState.Opening)
									PlaySoundClip(SoundClipState.Opening);
								
								switch (openType)
								{
									case OpenType.SlidePosX:
										Shift(new Vector3(openSpeed, 0, 0), true);
										break;
									case OpenType.SlideNegX:
										Shift(new Vector3(-openSpeed, 0, 0), true);
										break;
									case OpenType.SlidePosZ:
										Shift(new Vector3(0, openSpeed, 0), true);
										break;
									case OpenType.SlideNegZ:
										Shift(new Vector3(0, -openSpeed, 0), true);
										break;
									case OpenType.slidePosY:
										Shift(new Vector3(0, 0, openSpeed), true);
										break;
									case OpenType.slideNegY:
										Shift(new Vector3(0, 0, -openSpeed), true);
										break;
								}
							}
						}
						break;
					case DoorState.Opened:
						if (!string.IsNullOrEmpty(idleOpenAnimation.Trim()) && !GetComponent<Animation>().IsPlaying(finishedOpenAnimation))
							GetComponent<Animation>().CrossFade(idleOpenAnimation);
						break;
				}
			}
			else if (animationType == AnimationType.FileAnimation)
			{
				if (startedCloseAnimation)
				{
					Close();
				}
				else if (doorState == DoorState.StartOpen)
				{
					if (!startedOpenAnimation)
					{
						if (!string.IsNullOrEmpty(fileAnimationOpen))
						{
							if (!GetComponent<Animation>().IsPlaying(fileAnimationOpen))
								GetComponent<Animation>().Blend(fileAnimationOpen);
							
							startedOpenAnimation = true;
						}
						else
							Debug.LogWarning("There is no animation chosen for open animation.  Please add an open animation, or switch the animation type.");
					}
					else
					{
						if (!GetComponent<Animation>().IsPlaying(fileAnimationOpen))
						{
							doorState = DoorState.Opened;
							startedOpenAnimation = false;
						}
					}
				}
				else if (doorState == DoorState.StartClose)
				{
					if (!GetComponent<Animation>().IsPlaying(fileAnimationOpen))
						startedOpenAnimation = false;
				}
				else
				{
					if (!string.IsNullOrEmpty(idleOpenAnimation))
					{
						if (!GetComponent<Animation>().IsPlaying(idleOpenAnimation))
							GetComponent<Animation>().CrossFade(idleOpenAnimation);
					}
					else
						doorState = DoorState.None;
				}
			}
		}
	}
	
	private void Shift(Vector3 dir, bool fromOpen)
	{
		transform.Translate(dir);
					
		if (mainChild != null)
			mainChild.Translate(-dir);
		
		float final = 0;
		
		if (fromOpen)
		{
			keepTrack += openSpeed;
			final = openedSlideDistance;
		}
		else
		{
			keepTrack -= openSpeed;
			final = closedSlideDistance;
		}
		
		if ((fromOpen && keepTrack >= final) || (!fromOpen && keepTrack <= final))
		{
			float backStep = keepTrack - final;
			
			if (dir.x != 0)
				dir.x = backStep;
			else if (dir.y != 0)
				dir.y = backStep;
			else if (dir.z != 0)
				dir.z = backStep;
			
			transform.Translate(dir);
			
			keepTrack = final;
			
			if (fromOpen)
			{
				if (soundClipState != SoundClipState.Opened)
					PlaySoundClip(SoundClipState.Opened);
				
				if (openOnly)
					alreadyOpen = true;
				
				if (!string.IsNullOrEmpty(finishedOpenAnimation.Trim()))
					GetComponent<Animation>().Blend(finishedOpenAnimation);
				
				doorState = DoorState.Opened;
			}
			else
			{
				if (soundClipState != SoundClipState.Closed)
					PlaySoundClip(SoundClipState.Closed);
				
				if (!string.IsNullOrEmpty(finishedCloseAnimation.Trim()))
					GetComponent<Animation>().Blend(finishedCloseAnimation);
				
				doorState = DoorState.Closed;
			}
		}
	}
	
	private void Close()
	{
		if (animationType == AnimationType.ScriptAnimation)
		{
			switch (doorState)
			{
				case DoorState.StartClose:
					if (keepTrack == openedY_Rotation)
					{
						if (!string.IsNullOrEmpty(outroAnimation.Trim()) && !startedCloseAnimation && (!GetComponent<Animation>().isPlaying || GetComponent<Animation>().IsPlaying(idleOpenAnimation)))
						{
							GetComponent<Animation>().Blend(outroAnimation);
							
							startedCloseAnimation = true;
						}
						else
						{
							bool nextState = false;
							
							if (string.IsNullOrEmpty(outroAnimation.Trim()))
								nextState = true;
							else
							{
								if (!GetComponent<Animation>().IsPlaying(outroAnimation))
									nextState = true;
							}
							
							if (nextState)
							{
								startedCloseAnimation = false;
								doorState = DoorState.Closing;
							}
						}
					}
					else
						doorState = DoorState.Closing;
					break;
				case DoorState.Closing:
					if (openType == OpenType.OpenBack || openType == OpenType.OpenLeft ||
						openType == OpenType.OpenFront || openType == OpenType.OpenRight)
					{
						if (keepTrack != closedY_Rotation)
						{
							if (soundClipState != SoundClipState.Closing)
								PlaySoundClip(SoundClipState.Closing);
							
							transform.RotateAround(transform.position + offsetPosition, transform.up, -openSpeed);
							
							keepTrack -= openSpeed;
							
							if ((openSpeed > 0 && keepTrack <= closedY_Rotation) || (openSpeed < 0 && keepTrack >= closedY_Rotation))
							{
								float backStep = closedY_Rotation - keepTrack;
								
								transform.RotateAround(transform.position + offsetPosition, transform.up, backStep);
								
								keepTrack = closedY_Rotation;
								
								if (soundClipState != SoundClipState.Closed)
									PlaySoundClip(SoundClipState.Closed);
								
								if (!string.IsNullOrEmpty(finishedCloseAnimation.Trim()))
									GetComponent<Animation>().Blend(finishedCloseAnimation);
								
								doorState = DoorState.Closed;
							}
						}
						else
						{
							if (!string.IsNullOrEmpty(finishedCloseAnimation.Trim()) && !GetComponent<Animation>().IsPlaying(finishedCloseAnimation))
							{
								if (!GetComponent<Animation>().isPlaying)
								{
									GetComponent<Animation>().Blend(finishedCloseAnimation);
									doorState = DoorState.Closed;
								}
							}
							else
							{
								doorState = DoorState.Closed;
							}
						}
					}
					else
					{
						if (keepTrack != closedSlideDistance)
						{
							if (soundClipState != SoundClipState.Closing)
								PlaySoundClip(SoundClipState.Closing);
							
							switch (openType)
							{
								case OpenType.SlidePosX:
									Shift(new Vector3(-openSpeed, 0, 0), false);
									break;
								case OpenType.SlideNegX:
									Shift(new Vector3(openSpeed, 0, 0), false);
									break;
								case OpenType.SlidePosZ:
									Shift(new Vector3(0, -openSpeed, 0), false);
									break;
								case OpenType.SlideNegZ:
									Shift(new Vector3(0, openSpeed, 0), false);
									break;
								case OpenType.slidePosY:
									Shift(new Vector3(0, 0, -openSpeed), false);
									break;
								case OpenType.slideNegY:
									Shift(new Vector3(0, 0, openSpeed), false);
									break;
							}
						}
					}
					break;
				case DoorState.Closed:
					doorState = DoorState.None;
					break;
			}
		}
		else if (animationType == AnimationType.FileAnimation)
		{
			if (startedOpenAnimation)
			{
				Open();
			}
			else if (!startedCloseAnimation)
			{
				if (!string.IsNullOrEmpty(fileAnimationClose))
				{
					if (!GetComponent<Animation>().IsPlaying(fileAnimationClose))
						GetComponent<Animation>().Blend(fileAnimationClose);
				
					startedCloseAnimation = true;
				}
				else
					Debug.LogWarning("There is no animation chosen for close animation.  Please add a close animation, or toggle on open only, or switch the animation type.");
			}
			else
			{
				if (!GetComponent<Animation>().IsPlaying(fileAnimationClose))
				{
					if (doorState == DoorState.StartClose)
						doorState = DoorState.None;
					
					startedCloseAnimation = false;
				}
			}
		}
	}
	
	private void OnDrawGizmos()
	{
		if (!isChild && drawGizmos)
		{
			if (useModelOrigin)
			{
				offsetPosition = Vector3.zero;
				Gizmos.DrawRay(transform.position - new Vector3(0, 5, 0), transform.up * 10);
				DrawGizmos();
			}
			else if (transform.GetComponent<MeshFilter>() != null && transform.GetComponent<MeshFilter>().sharedMesh != null)
			{
				float modifyer = 0;
				
				if (openType == OpenType.OpenFront || openType == OpenType.OpenBack)
					modifyer = (transform.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * transform.localScale.z) * 0.5f;
				else
					modifyer = (transform.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * transform.localScale.x) * 0.5f;
				
				switch (openType)
				{
					case OpenType.OpenLeft:
						offsetPosition = -transform.right * modifyer;
						goto default;
					case OpenType.OpenRight:
						offsetPosition = transform.right * modifyer;
						goto default;
					case OpenType.OpenFront:
						offsetPosition = transform.forward * modifyer;
						goto default;
					case OpenType.OpenBack:
						offsetPosition = -transform.forward * modifyer;
						goto default;
					case OpenType.SlidePosX:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, transform.right * 10);
						break;
					case OpenType.SlideNegX:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, -transform.right * 10);
						break;
					case OpenType.SlidePosZ:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, transform.forward* 10);
						break;
					case OpenType.SlideNegZ:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, -transform.forward * 10);
						break;
					case OpenType.slidePosY:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, transform.up * 10);
						break;
					case OpenType.slideNegY:
						offsetPosition = Vector3.zero;
						Gizmos.DrawRay(transform.position + offsetPosition, -transform.up * 10);
						break;
					default:
						Gizmos.DrawRay(transform.position + offsetPosition + (-transform.up * 5), transform.up * 10);
						break;
				}
				
				DrawGizmos();
			}
			else if (animationType == AnimationType.FileAnimation)
			{
				DrawGizmos();
			}
			else
				Debug.Log("You do not have a mesh filter attached to this object.");
		}
	}
	
	public void DrawGizmos()
	{
		if (range > 0)
		{
			if (mainChild != null)
				Gizmos.DrawWireSphere(mainChild.position, range);
			else
				Gizmos.DrawWireSphere(transform.position + rangeOffset, range);
		}
	}
	
	public void EnteredTrigger()
	{
		doorState = DoorState.StartOpen;
	}
	
	public void ExitedTrigger()
	{
		if (!openOnly)
			doorState = DoorState.StartClose;
	}
	
	private void OnTriggerEnter(Collider c)
	{
		if (isChild)
		{
			Door parentDoor = mainChild.GetComponent<Door>();
			
			switch (restrictedTo)
			{
				case TypeOfDetection.GameObject:
					if (c.gameObject == parentDoor.checkAgainstObject)
						parentDoor.EnteredTrigger();
					break;
				case TypeOfDetection.ScriptType:
					if (c.GetComponent(parentDoor.classType) != null)
						parentDoor.EnteredTrigger();
					break;
				case TypeOfDetection.Any:
					parentDoor.EnteredTrigger();
					break;
			}
		}
	}
	
//	private void OnTriggerStay(Collider c)
//	{
//		Door parentDoor = transform.parent.GetComponent<Door>();
//		
//		switch (restrictedTo)
//		{
//			case TypeOfDetection.GameObject:
//				if (c.gameObject == parentDoor.checkAgainstObject)
//					parentDoor.EnteredTrigger();
//				break;
//			case TypeOfDetection.ScriptType:
//				if (c.GetComponent(parentDoor.classType) != null)
//					parentDoor.EnteredTrigger();
//				break;
//			case TypeOfDetection.Any:
//				parentDoor.EnteredTrigger();
//				break;
//		}
//	}
	
	private void OnTriggerExit(Collider c)
	{
		if (isChild)
		{
			Door parentDoor = mainChild.GetComponent<Door>();
		
			switch (restrictedTo)
			{
				case TypeOfDetection.GameObject:
					if (c.gameObject == parentDoor.checkAgainstObject)
						parentDoor.ExitedTrigger();
					break;
				case TypeOfDetection.ScriptType:
					if (c.GetComponent(parentDoor.classType) != null)
						parentDoor.ExitedTrigger();
					break;
				case TypeOfDetection.Any:
					parentDoor.ExitedTrigger();
					break;
			}
		}
	}
}