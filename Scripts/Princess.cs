using Godot;
using System;
using FallKnight.Scripts.PlayerScript;
namespace FallKnight.Scripts.PrincessScript
{
     public partial class Princess : Area2D
{
        [Signal] public delegate void playerWinEventHandler(string arg);
	public void onPlayerWin(Node2D body)
        {
            if (body is Player player)
            {
                EmitSignal(SignalName.playerWin, "player_win");
            }
        }
}

}
   
