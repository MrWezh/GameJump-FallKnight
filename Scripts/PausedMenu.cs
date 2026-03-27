using FallKnight.Scripts.PlayerScript;
using Godot;
using System;

public partial class PausedMenu : CanvasLayer
{
    [Export] private CanvasLayer _gameFinish;
    [Export] private Timer _gameFinishTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {

            if (@event.IsActionPressed("pause"))
            {
                _gameFinishTimer.Paused=!Visible;
				Visible = !Visible;
               if(!_gameFinish.Visible) GetTree().Paused = Visible;
            }
    }

    private void onResetPressed()
    {
        GetTree().ReloadCurrentScene();
    }
    private void onExitPressed()
    {
        GetTree().Quit();
    }
    private void onContinuePressed()
    {
        Visible = false;
        GetTree().Paused = false;
    }

    void onCheckBoxToggled(bool checkbox)
    {
        if (checkbox)
        {
            GetParent().GetParent().GetNode<AudioStreamPlayer>("BackGround").Stop();
        }
        else GetParent().GetParent().GetNode<AudioStreamPlayer>("BackGround").Play();
    }
    public override void _ExitTree()
    {
        _gameFinishTimer.Paused = false;
    }
}
