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

      if (!_player.IsOnFloor())
        {
            Vector2 velocity = _player.Velocity;
            velocity += _player.GetGravity() * (float)delta;
            if (_player.Velocity.Y < 0)
                stateMachine.TransitionTo("JumpingState");
            else{
                stateMachine.TransitionTo("FallingState");
            }
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
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpingState");
    }
}
