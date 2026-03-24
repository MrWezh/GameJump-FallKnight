using FallKnight.Scripts.PlayerScript;
using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

public partial class Princess : Sprite2D
{

	//public Player player;
	[Signal] public delegate void playerWinEventHandler();
	public void OnPlayerWin(Node2D body)
	{
		GD.Print("Player wins");
		if (body is Player player)
		{
			EmitSignal(SignalName.playerWin);
		}
	}
}
