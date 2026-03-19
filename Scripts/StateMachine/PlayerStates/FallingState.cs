using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using FallKnight.Scripts.PlayerScript;

namespace FallKnight.Scripts.StateMachines.PlayerStates
{
    public partial class FallingState : State
    {
        private Player _player;

        public override void Ready()
        {
            _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;

        }

        public override void Enter()
        {
            GD.Print("Entered Falling State");
            _player.SetAnimation("fall");
            _player.setInitHeight(_player.Position.Y);
        }

        public override void Update(double delta)
        {
            if (_player.IsOnFloor())
            {
                if (_player.IsOnFloor())
            _player.setFinalHeight(_player.Position.Y);
                    stateMachine.TransitionTo("IdleState");
            }
        }
        public override void UpdatePhysics(double delta)
        {
            Vector2 velocity = _player.Velocity;
            if (!_player.IsOnFloor())
            {
                velocity += _player.GetGravity() * (float)delta;

            }
            _player.Velocity = velocity;
        }

    }
}