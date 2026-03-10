using Godot;


public partial class JumpingState : State
{
    private Player _player;

    public override async void Ready()
    {
        _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;
    }
    public override void Enter()
    {
        GD.Print("Entered Jumping State");
    }

    public override void Update(double delta)
    {
        if (!_player.IsOnFloor() && _player.Velocity.Y >= 0)
        {
                stateMachine.TransitionTo("FallingState");
        }
    }

    public override void UpdatePhysics(double delta)
    {
        
        Godot.Vector2 velocity = _player.Velocity;

        if (_player.IsOnFloor())
            velocity.Y += _player.GetJumpVelocity();
            velocity += _player.GetGravity() * (float)delta;

        _player.Velocity = velocity;
        _player.MoveAndSlide();

    }

}
