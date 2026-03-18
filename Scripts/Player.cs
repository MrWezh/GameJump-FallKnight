using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using System.ComponentModel.DataAnnotations.Schema;


namespace FallKnight.Scripts.PlayerScript
{

	public partial class Player : CharacterBody2D
	{

		private int _health = 100;
		private const float Speed = 160.0f;
		private float JumpVelocity = -50.0f;
		private const float weight = 50f;
		private const float MinmJumpVelocity = -50.0f;
		private const float MaxJumpVelocity = -600.0f;
		private const float ChargeRate = 1000.0f;
		private bool _hit = false;

		//Capturar las posiciones al caer para calcular el daño de caida
		private float _initHeight;
		private float _finalHeight;
		private bool _charging = false;

		[Export] private CollisionShape2D _collision;
		[Export] private AnimatedSprite2D _animatedSprite;
		[Export] private ProgressBar _healthBar;

		StateMachine _stateMachine;
		public void SetCharging()
		{
			_charging = !_charging;
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

		public bool GetHit()
		{
			return _hit;
		}

		public void SetHit()
		{
			_hit = !_hit;
		}
		public float getInitHeight()
		{
			return _initHeight;
		}
		public void setInitHeight(float a)
		{
			_initHeight = a;
		}

		public float getFinalHeight()
		{
			return _finalHeight;
		}
		public void setFinalHeight(float a)
		{
			_finalHeight = a;
		}

		[Signal] public delegate void playerDeadEventHandler();
		public override void _Ready()
		{
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

			if (collision != null)
			{
				//Solo ejecuta la animación del hit una vez haya saltado y que haya collisionado con algo
				Vector2 normal = collision.GetNormal();
				if (normal == new Vector2(0, -1))
				{
					vel = Velocity;
				}
				else
				{
					SetAnimation("hit");
					vel = vel.Bounce(normal);
				}
			}

			Velocity = vel;
			MoveAndSlide();
		}

	public void fallDamage()
		{
		float bloc = 32;
		float fallenHeight =_finalHeight - _initHeight;
		GD.Print("Altura inicia: ", _initHeight, ", altura final: ", _finalHeight);
		float MaxHeightWhioutDAmenge = 16*bloc;
		//GD.Print(fallenHeight); 

			if (fallenHeight > MaxHeightWhioutDAmenge)
			{
				_health -= (int)(fallenHeight-MaxHeightWhioutDAmenge);
				if(_health<0) _health = 0;
			}
			_healthBar.Value = _health;
			
			if (_health == 0)
			{
				SetAnimation("die");
				EmitSignal(SignalName.playerDead);
			}
		_initHeight = 0f;
		_finalHeight = 0f;


		}

      public void onHealthDown(float newValue)
        {
            SetAnimation("die");
        }
	}
}

