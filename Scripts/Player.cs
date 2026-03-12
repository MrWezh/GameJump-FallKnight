using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using System.ComponentModel.DataAnnotations.Schema;


namespace FallKnight.Scripts.PlayerScript{

public partial class Player : CharacterBody2D
{
	private const float Speed = 200.0f;
	private float JumpVelocity = -100.0f;
	private const float weight = 50f;

	private const float MinmJumpVelocity = -50.0f;
    private const float MaxJumpVelocity = -600.0f;
	private const float ChargeRate = 1000.0f;

	private bool jumped = false;

	private bool _hit = false;

	private CollisionShape2D _collision;

	 private bool _charging = false;

	private AnimatedSprite2D _animatedSprite;

	StateMachine _stateMachine;
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

	public bool GetJumped()
        {
            return jumped;
        }

	public void SetJumped(bool a){
		jumped = a;
	}
	public bool GetHit()
        {
            return _hit;
        }

	public void SetHit(bool a){
		_hit = a;
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

        public override void _PhysicsProcess(double delta)
        {
			//Invertir la animación depediendo de la dirección del input

			  float move = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
			              if (move < 0f)
                _animatedSprite.FlipH = true;
            else if (move > 0f)
                _animatedSprite.FlipH = false;

			//El player rebota solamente cuando no este en el suelo.
            KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
				Vector2 vel = Velocity;

			if(collision != null ){
				//Solo ejecuta la animación del hit una vez haya saltado y que haya collisionado con algo
			
				
				Vector2 normal = collision.GetNormal();
				GD.Print(normal);
				if(normal == new Vector2(0,-1)) {
					vel = Velocity;
				}
				else
				{
					SetAnimation("hit");
					
					_hit=true;
					vel = vel.Bounce(normal);
				}
			}

			Velocity = vel;
			MoveAndSlide();
        }
}
}