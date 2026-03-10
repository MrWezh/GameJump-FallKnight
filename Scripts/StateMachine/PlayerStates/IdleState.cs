using Godot;
using System;

public partial class IdleState : State
{
    private Player _player;


    public override async void Ready()
    {
        _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;

    }
    public override void Enter()
    {
        GD.Print("Entered Idle State");
         //_player.SetAnimation("idle");
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
            _player.SetCharging(true);
            stateMachine.TransitionTo("JumpingState");

        }
    }
}
