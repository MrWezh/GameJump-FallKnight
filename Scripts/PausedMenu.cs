using Godot;
using System;

public partial class PausedMenu : CanvasLayer
{
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
				Visible = !Visible;
                GetTree().Paused = Visible;
            }
    }

    public void onResetPressed()
    {
        GetTree().ReloadCurrentScene();
    }
    public void onExitPressed()
    {
        GetTree().Quit();
    }
}
