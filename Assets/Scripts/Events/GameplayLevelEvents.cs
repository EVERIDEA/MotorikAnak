using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLevelEvents : GameEvent {
	public int Id;
	public bool IsActive;

	public GameplayLevelEvents(int id, bool isActive)
	{
		Id = id;
		IsActive = isActive;
	}
}
