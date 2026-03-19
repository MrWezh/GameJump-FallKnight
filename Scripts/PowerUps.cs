using FallKnight.Scripts.PlayerScript;
using Godot;
using System;

public partial class PowerUps : Area2D
{
	public enum PowerUpTypeEnum : int 
	{
		HealthPotion = 0,
		Armor = 1,
		FeatherFall = 2
	}

	[Export] public PowerUpTypeEnum PowerUpType;
	public Player player;

	public override void _Ready()
	{
		
	}

	public void ApplyPowerUp(Player player)
	{
		switch (PowerUpType)
		{
			case PowerUpTypeEnum.HealthPotion: // HealthPotion
				player.UsePotion();
				break;
			case PowerUpTypeEnum.Armor: // Armor
				player.UseArmor();
				break;
			case PowerUpTypeEnum.FeatherFall: // FeatherFall
				player.UseFeatherFall();
				break;
			default:
				GD.Print("Tipo de Power-Up desconocido");
				break;
		}
	}

	public void OnPlayerPickedUp(Node2D body)
	{
		GD.Print("Power-Up: " + PowerUpType);
		if (body is Player player)
		{
			ApplyPowerUp((Player)body);
			GD.Print("Power-Up recogido: " + PowerUpType);
			QueueFree();
		}
	}

}
