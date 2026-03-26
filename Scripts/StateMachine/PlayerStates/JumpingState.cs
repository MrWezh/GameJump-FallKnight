using Godot;
using FallKnight.Scripts.StateMachines;
using FallKnight.Scripts.PlayerScript;


namespace FallKnight.Scripts.StateMachines.PlayerStates
{
    public partial class JumpingState : State
    {
    private Player _player;
    

    public override void Ready()
    {
        _player = (Player)GetParent().GetParent<CharacterBody2D>() as Player;
    }
    public override void Enter()
        {
            _player.Velocity = Vector2.Zero;
            GD.Print("Entered jump State");
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
            _player.PlayAnimation("pre-jump");
            float _jump = _player.GetJumpVelocity();
            _jump -= (float)(_player.GetChargeRate() * delta);
            if (_jump < _player.GetMaxJumpVelocity())
                _jump = _player.GetMaxJumpVelocity();
            _player.SetJumpVelocity(_jump);
        }

        Godot.Vector2 velocity = _player.Velocity;
        //Cuando el jugador suelta el botón de salto, se detiene la carga y se transiciona al estado de salto
        // Si se alcanza el salto máximo mientras se carga, se lanza el salto automáticamente *(pediente de implementar)
        if (_player.GetCharging() && Input.IsActionJustReleased("jump") 
        ||(_player.GetJumpVelocity() == _player.GetMaxJumpVelocity() && _player.GetCharging())) 
        {
        _player._jumpAudio.Play();
        _player.SetAnimation("jump");
        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0.0f)
        {

           if (_player.GetPlayerCollidingWall())
                {
                    GD.Print(direction, " ", _player.GetSpeed());
                    direction *= -1;
                    _player.SetPlayerCollidingWall(false);
                }
           velocity.X = direction * _player.GetSpeed();
           
        }
            _player.PlayAnimation("jump");        
             velocity.Y += _player.GetJumpVelocity();    
            _player.SetCharging();
        }
        velocity += new Vector2(0,980.0f) * (float)delta;
        _player.Velocity = velocity;
       
    }
    public override void Exit()
    {
        _player.SetJumpVelocity(_player.GetMinJumpVelocity()); // Resetea la velocidad de salto para el próximo salto
    }

        public override void HandleInput(InputEvent @event)
        {

        }

    }
}
