using Godot;
using GodotPlugins.Game;
using System;

public partial class WalkingState : State
{
    private Player _player;

    public override async void Ready()
    {
        _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;
    }
    public override void Enter()
    {
         GD.Print("Entered Walking State");
     
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
        Vector2 velocity = _player.Velocity;
        
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
		_player.MoveAndSlide();
    }

    public override void HandleInput(InputEvent @event)
    {
		if (@event.IsActionPressed("jump"))
			stateMachine.TransitionTo("JumpingState");
    }


}
