using Godot;
using System;

public partial class Menu : Control
{
	private void onStartPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
    }
	private void onExitPressed()
    {
        GetTree().Quit();
    }
}
