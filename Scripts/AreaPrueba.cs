using Godot;
using System;

public partial class AreaPrueba : Area2D
{
	public void OnCuerpoEntrar(Node2D body)
	{
		GD.Print("Cuerpo entró: " + body.Name);
	}
}
