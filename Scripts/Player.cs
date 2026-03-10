using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float Speed = 500.0f;
	private float JumpVelocity = -100.0f;
	private const float weight = 50f;


	 private bool _charging = false;

    public void SetCharging(bool a)
    {
        _charging = a;
    }

	public bool GetCharging()
	{
		return _charging;
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
	}

	public void SetAnimation(string animationName)
	{
		if (_animatedSprite == null)
			_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play(animationName);
	}


}
