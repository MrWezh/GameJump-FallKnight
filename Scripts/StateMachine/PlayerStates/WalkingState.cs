using Godot;
using GodotPlugins.Game;
using System;
using FallKnight.Scripts.StateMachines;
using FallKnight.Scripts.PlayerScript;


namespace FallKnight.Scripts.StateMachines.PlayerStates
{
    public partial class WalkingState : State
    {
        private Player _player;
        Vector2 velocity;

        public override void Ready()
        {
            _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;
            velocity = _player.Velocity;
        }
        public override void Enter()
        {
            GD.Print("Entered walk State");
        if(_player.GetFeatherFallActive()) _player.SetAnimation("ParaguasWalk");
        else if(_player.GetArmorBarVisibility())  _player.SetAnimation("ArmorWalk");
        else if(_player.GetFeatherFallActive() && !_player.GetArmorBarVisibility()) _player.SetAnimation("walk");
        else _player.SetAnimation("walk");
            

        }

        public override void Update(double delta)
        {

            if (!_player.IsOnFloor())
            {
                stateMachine.TransitionTo("FallingState");
            }
            else if(_player.IsOnFloor()&&_player.Velocity.X == 0)
            {
                stateMachine.TransitionTo("IdleState");
            }

        }

        public override void UpdatePhysics(double delta)
        {


            float direction = Input.GetAxis("move_left", "move_right");
            if (direction != 0.0f)
            {
                velocity.X = direction * _player.GetSpeed();
            }
            else
            {
                velocity.X = Mathf.MoveToward(_player.Velocity.X, 0, _player.GetSpeed());
            }

            _player.Velocity = velocity;
        }

        public override void HandleInput(InputEvent @event)
        {
            if(@event.IsActionPressed("jump"))
            {
                _player.SetCharging();
                stateMachine.TransitionTo("JumpingState");
            }
        }


    }
}
