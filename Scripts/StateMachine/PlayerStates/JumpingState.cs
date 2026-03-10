using Godot;


public partial class JumpingState : State
{
    private Player _player;

    public override async void Ready()
    {


    }
    public override void Enter()
    {

    }

    public override void Update(double delta)
    {
        if (!_player.IsOnFloor() && _player.Velocity.Y >= 0)
        {
                stateMachine.TransitionTo("FallingMovementState");
        }
    }

    public override void UpdatePhysics(double delta)
    {
        
        Godot.Vector2 velocity = _player.Velocity;
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
