using FallKnight.Scripts.PlayerScript;
using Godot;
using System;
using System.ComponentModel;

public partial class PowerUps : Area2D
{
	[Export] Sprite2D _sprite;

	private const float _animationSize = 15.0f;
	private const float _animationVelocity = 4.0f;
	private float _spriteInitPosition;
	private float _time;

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
		BodyEntered += OnPlayerPickedUp;
		switch (PowerUpType)
		{
			case PowerUpTypeEnum.HealthPotion:
			_sprite.Texture = GD.Load<Texture2D>("uid://vxauue3ahrxw");
			break;
			case PowerUpTypeEnum.Armor:
			_sprite.Texture = GD.Load<Texture2D>("uid://ctp351n54tacv");
			break;
			case PowerUpTypeEnum.FeatherFall:
			_sprite.Texture = GD.Load<Texture2D>("uid://cnbecog8no8b");
			break;
		}

		_spriteInitPosition = _sprite.Position.Y;
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

    public override void _Process(double delta)
    {
		_time += (float) delta;
        float displacement = (float) Math.Sin(_time*_animationVelocity) * _animationSize;
		_sprite.Position = new Vector2(_sprite.Position.X, _spriteInitPosition + displacement);
    }

}
