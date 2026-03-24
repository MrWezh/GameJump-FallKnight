using FallKnight.Scripts.PlayerScript;
using Godot;

namespace FallKnight.Scripts.GameControlerScript
{
public partial class GameControler : Node
{
	[Export] private CanvasLayer _gameOverMensage;
	[Export] private Player _player;
	[Export] private Timer _timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
		{
			_player.playerDead += playerDead;
			_gameOverMensage.Visible = false;
			GetTree().Paused = false;
           PhysicsServer2D.AreaSetParam(GetViewport().FindWorld2D().Space, PhysicsServer2D.AreaParameter.Gravity, 980.0f);

		}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void playerDead()
	{
			GetTree().Paused = true;
			_gameOverMensage.Visible = true;
			_timer.Start();
			GD.Print();
			 
	}

	private void onTimerTimeout()
		{
			GD.Print("Timer out");
			_timer.Stop();
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/main.tscn");
		}
}
}
