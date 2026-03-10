using Godot;
using System;

public partial class IdleState : State
{
    private Player _player;

    public override async void Ready()
    {
  
    }
    public override void Enter()
    {
        //_player.SetAnimation("default");
    }

    public override void Update(double delta)
    {

      if (!_player.IsOnFloor())
        {
            Vector2 velocity = _player.Velocity;
            velocity += _player.GetGravity() * (float)delta;
            if (_player.Velocity.Y < 0)
                stateMachine.TransitionTo("JumpingMovementState");
            else{
                GD.Print("Falling from Idle");
                stateMachine.TransitionTo("FallingMovementState");
            }
        }

      float direction = Input.GetAxis("move_left", "move_right");


        if (direction != 0.0f)
        {
            stateMachine.TransitionTo("RunningMovementState");
        }
    }
    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("move_left") || @event.IsActionPressed("move_right"))
            stateMachine.TransitionTo("RunningMovementState");
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpingMovementState");
    }
}
