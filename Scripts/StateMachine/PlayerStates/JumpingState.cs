using Godot;

public partial class JumpingState : State
{
    private Player _player;
    private const float MinimJumpVelocity = -100.0f;
    private const float MaxJumpVelocity = -800.0f;
    private const float ChargeRate = 1000.0f;
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
        //Cuando el jugador presiona el botón de salto, comenzamos a cargar el salto
        //Cambiando el paramentro _charging a true, lo que indica que el jugador está en proceso de cargar el salto

        if (!_player.IsOnFloor() && _player.Velocity.Y > 0)
        {
                stateMachine.TransitionTo("FallingState");
        }
    }

    public override void UpdatePhysics(double delta)
    {
                 /*Mientras el jugador mantiene presionado el botón de salto, se incrementa 
         la velocidad de salto en función del tiempo que ha pasado desde que comenzó 
         a cargar el salto, hasta un límite máximo definido por MaxChargeJump.*/
        if (_player.GetCharging())
        {
            GD.Print("Charging Jump...");
            float _jump = _player.GetJumpVelocity();
            _jump -= (float)(ChargeRate * delta);
            if (_jump < MaxJumpVelocity)
                _jump = MaxJumpVelocity;
            _player.SetJumpVelocity(_jump);
        }

        Godot.Vector2 velocity = _player.Velocity;
        //Cuando el jugador suelta el botón de salto, se detiene la carga y se transiciona al estado de salto
        // Si se alcanza el salto máximo mientras se carga, se lanza el salto automáticamente *(pediente de implementar)
        if (_player.GetCharging() && Input.IsActionJustReleased("jump") 
        ||(_player.GetJumpVelocity() == MaxJumpVelocity && _player.GetCharging())) 
        {
        
        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0.0f)
        {
           velocity.X = direction * _player.GetSpeed();
        }
            GD.Print("jump"); 
            GD.Print(_player.GetJumpVelocity());            
             velocity.Y += _player.GetJumpVelocity();
            _player.SetCharging(false);
        }
        velocity += _player.GetGravity() * (float)delta;
        _player.Velocity = velocity;
        _player.MoveAndSlide();
       
    }
    public override void Exit()
    {
        _player.SetJumpVelocity(MinimJumpVelocity); // Resetea la velocidad de salto para el próximo salto
    }


}
