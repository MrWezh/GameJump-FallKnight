using Godot;
using System;
using FallKnight.Scripts.StateMachines;
using FallKnight.Scripts.PlayerScript;


namespace FallKnight.Scripts.StateMachines.PlayerStates
{
    public partial class IdleState : State
    {
    private Player _player;


    public override async void Ready()
    {
        _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;

    }
    public override void Enter()
    {
        _player.Velocity = Vector2.Zero; 
        _player.SetHit();
        //GD.Print("Entered Idle State");
         _player.SetAnimation("idle");
    }
    public override void Update(double delta)
    {
 

        if (!_player.IsOnFloor() && _player.Velocity.Y > 0)
        {
            stateMachine.TransitionTo("FallingState");
        }

        float direction = Input.GetAxis("move_left", "move_right");

        if (direction != 0.0f)
        {
            stateMachine.TransitionTo("WalkingState");
        }
    }
    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("move_left") || @event.IsActionPressed("move_right"))
            stateMachine.TransitionTo("RunningState");
        // Jump handled by charge logic in Update
        if(@event.IsActionPressed("jump"))
        {
            _player.SetCharging();
            stateMachine.TransitionTo("JumpingState");

        }
    }
    }
}
