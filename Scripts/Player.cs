using Godot;
using System;
using FallKnight.Scripts.StateMachines;


namespace FallKnight.Scripts.PlayerScript{

public partial class Player : CharacterBody2D
{
	private const float Speed = 200.0f;
	private float JumpVelocity = -100.0f;
	private const float weight = 50f;

	private const float MinmJumpVelocity = -50.0f;
    private const float MaxJumpVelocity = -800.0f;
	private const float ChargeRate = 1000.0f;

	private CollisionShape2D _collision;

	 private bool _charging = false;

    public void SetCharging(bool a)
    {
        _charging = a;
    }

	public bool GetCharging()
	{
		return _charging;
	}
	public float GetMinJumpVelocity()
    {
        return MinmJumpVelocity;
    }

	public float GetMaxJumpVelocity()
    {
        return MaxJumpVelocity;
    }
	public float GetChargeRate()
    {
        return ChargeRate;
    }


	private AnimatedSprite2D _animatedSprite;

	StateMachine _stateMachine;

	public float GetSpeed()
	{
	   return Speed; 
	}

	public float GetJumpVelocity()
	{
		return JumpVelocity;
	}

	public void SetJumpVelocity(float a)
	{
		JumpVelocity = a;
	}

	public float GetWeight()
	{
		return weight;
	}

	public override void _Ready()
	{
		_stateMachine = GetNode<StateMachine>("StateMachine");
		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_collision = GetNode<CollisionShape2D>("Collision");
	}

	public void SetAnimation(string animationName)
	{
		if (_animatedSprite == null)
			_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play(animationName);
	}

    
}
}