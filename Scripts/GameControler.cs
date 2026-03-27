using Godot;
using FallKnight.Scripts.PlayerScript;
using FallKnight.Scripts.PrincessScript;

namespace FallKnight.Scripts.GameControlerScript
{
public partial class GameControler :Node
{
	public Tween _tween;
	[Export] private CanvasLayer _gameFinishMensage;
	[Export] private Player _player;
	[Export] private Princess _princess;
	[Export] private Timer _timer;
	[Export] private AudioStreamPlayer _backgoundAudio;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
		{
			_tween = GetTree().CreateTween();
			_backgoundAudio.VolumeDb = -50.0f;
			_tween.TweenProperty(_backgoundAudio, "volume_db", -15.0f, 1.0f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);
			_player.playerDead += gameFinish;
			_princess.playerWin += gameFinish;
			_gameFinishMensage.Visible = false;
			GetTree().Paused = false;
           PhysicsServer2D.AreaSetParam(GetViewport().FindWorld2D().Space, PhysicsServer2D.AreaParameter.Gravity, 980.0f);

		   if(_timer == null)
		   	_timer = GetNode<Timer>("GamePausedTimer");

		}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

        public override void _Input(InputEvent @event)
        {
        }

	private void gameFinish(string arg)
	{
			_gameFinishMensage.Visible = true;
			switch (arg)
			{
				case "player_dead":
					_gameFinishMensage.GetNode<AudioStreamPlayer>("GameOver").Play();
					_gameFinishMensage.GetNode<Label>("Lose").Visible=true;
					_gameFinishMensage.GetNode<Label>("Win").Visible=false;

				break;
				case "player_win":
					_gameFinishMensage.GetNode<AudioStreamPlayer>("Victory").Play();
					_gameFinishMensage.GetNode<Label>("Win").Visible=true;
					_gameFinishMensage.GetNode<Label>("Lose").Visible=false;
				break;
				default:
				GD.Print("Error: argument Invalid");
				break;
			}

				GetTree().Paused = true;
				_timer.Start();
            
	}
	private void onTimerTimeout()
		{
			_timer.Stop();
			GetTree().ReloadCurrentScene();
		}
        public override void _ExitTree()
        {
            _player.playerDead -= gameFinish;
			_princess.playerWin -= gameFinish;
        }
}

}
