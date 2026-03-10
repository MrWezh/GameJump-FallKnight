using Godot;
using System;

public partial class FallingState : State
{
    private Player _player;

    public override async void Ready()
    {
    }


    public override void Enter()
    {
        //_player.SetAnimation("fall");
    }

    public override void Update(double delta)
    {
		if (_player.IsOnFloor())
		{
			if (_player.Velocity.X == 0)
				stateMachine.TransitionTo("IdleMovementState");
			else
				stateMachine.TransitionTo("RunningMovementState");

        }
    
    }

    public override void UpdatePhysics(double delta)
    {

        Vector2 velocity = _player.Velocity;
        float direction = Input.GetAxis("move_left", "move_right");

        if (!_player.IsOnFloor())
        {
            velocity += _player.GetGravity() * (float)delta;

            if (direction!=0.0f)
            {
                //velocity.X = direction * _player.GetSpeed();
            }

        }
        _player.Velocity = velocity;
		_player.MoveAndSlide();
    }

}
