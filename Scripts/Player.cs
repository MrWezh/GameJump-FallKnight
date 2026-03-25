using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using System.ComponentModel.DataAnnotations.Schema;


namespace FallKnight.Scripts.PlayerScript
{

	public partial class Player : CharacterBody2D
	{

		public int _health = 100;
		public int _armor = 0;
		public bool _featherFallActive = false;
		private const float Speed = 160.0f;
		private float JumpVelocity = -50.0f;
		private const float weight = 50f;
		private const float MinJumpVelocity = -50.0f;
		private const float MaxJumpVelocity = -600.0f;
		private const float ChargeRate = 1000.0f;
		private bool _hit = false;
		//Capturar las posiciones al caer para calcular el daño de caida
		private float _initHeight;
		private float _finalHeight;
		private bool _charging = false;
		[Export] private CollisionShape2D _collision;
		[Export] private AnimatedSprite2D _animatedSprite;
		[Export] private AnimatedSprite2D _umbrella;
		[Export] private ProgressBar _healthBar;
		[Export] private ProgressBar _armorBar;
		[Export] private ProgressBar _featherFallBar;
		[Export] private Timer _featherFallTimer;


		public PowerUps _powerUp;

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
			return MinJumpVelocity;
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

		public bool GetFeatherFallActive()
        {
            return _featherFallActive;
        }
		public bool GetArmorBarVisibility()
        {
            return _armorBar.Visible;
        }

		[Signal] public delegate void playerDeadEventHandler();
		public override void _Ready()
		{
			_umbrella.Visible = false;
		}

		public void SetAnimation(string animationName)
		{
			if (_animatedSprite == null)
				_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
			_animatedSprite.Play(animationName);
		}

		public void ActiveUmbrella(String animation)
        {
			_umbrella.Visible = true;
            if(_umbrella == null)
				_umbrella = GetNode<AnimatedSprite2D>("Paraguas");
			_umbrella.Play(animation);
        }

		public void PlayAnimation(string state)
        {
        if(GetArmorBarVisibility())  SetAnimation("armor"+"-"+state);
        else SetAnimation(state);
        if(GetFeatherFallActive()) ActiveUmbrella(state);
        }

        public override void _Process(double delta)
        {
            if (_featherFallActive)
            {
				_featherFallBar.Value = _featherFallTimer.TimeLeft;
            }
           
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

			if (collision != null&&!_featherFallActive)
			{
				//Solo ejecuta la animación del hit una vez haya saltado y que haya collisionado con algo
				Vector2 normal = collision.GetNormal();
				if (normal == new Vector2(0, -1))
				{
					vel = Velocity;
				}
				else
				{
        			PlayAnimation("hit");
					vel = vel.Bounce(normal);
				}
			}

			Velocity = vel;
			MoveAndSlide();
		}

		public void ArmorVisibility()
		{
			if (_armor == 0)
			{
				_armorBar.Visible = false;
			}
			else
			{
				_armorBar.Visible = true;
			}
		}

		public void UsePotion()
		{
            _health += 100;
			if (_health > 100) _health = 100;
			_healthBar.Value = _health;
            
			
		}

		public void UseArmor()
		{
			ArmorVisibility();
			_armor += 100;
			if (_armor > 100) _armor = 100;
			_armorBar.Value = _armor;
		}

public void UseFeatherFall()
       {
			_featherFallBar.Visible = true;
           _featherFallActive = true;
           PhysicsServer2D.AreaSetParam(GetViewport().FindWorld2D().Space, PhysicsServer2D.AreaParameter.Gravity, 200f);
		   _featherFallTimer.Start();
       }

	  public void onFeatherFallTtimeout()
        {
			_featherFallBar.Visible = false;
			_umbrella.Visible = false;
           PhysicsServer2D.AreaSetParam(GetViewport().FindWorld2D().Space, PhysicsServer2D.AreaParameter.Gravity, 980.0f);
		   _featherFallTimer.Stop();
		   _featherFallActive = false;
        }
	public void fallDamage()
		{
			float bloc = 32;
			float fallenHeight =_finalHeight - _initHeight;
			float MaxHeightWithoutDamage = 16*bloc;
			if (fallenHeight > MaxHeightWithoutDamage)
			{
			int damage = (int)(fallenHeight - MaxHeightWithoutDamage);
			if(_featherFallActive) damage = damage/8;
			GD.Print(damage);
			GD.Print(GetGravity());

				if (_armor > 0)
				{	
					_armor -= damage;
					if (_armor < 0) {
						_health += _armor;
						_armor = 0;
						}
				}
				else
				{
					_health -= damage;
				}
					PlayAnimation("die");
			}
			if (_health < 0)
				{
					EmitSignal(SignalName.playerDead);
				}
			_armorBar.Value = _armor;
			ArmorVisibility();
			_healthBar.Value = _health;
			_initHeight = 0f;
			_finalHeight = 0f;
		}
	}
}

