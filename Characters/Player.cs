using Godot;
using System;

public class Player : Character
{
	Hitbox SwordHitbox;
	public override void _Process(float delta)
	{
		SwordHitbox = GetNode<Node2D>("Sword").GetNode<Node2D>("SwordParent").GetNode<Sprite>("Sprite").GetNode<Hitbox>("Hitbox");
		AnimatedSprite Character = this.GetNode<AnimatedSprite>("SpriteAnimation");
		Node2D Sword = GetNode<Node2D>("Sword");
		Vector2 MouseDirection = (Character.GlobalPosition - GetGlobalMousePosition());
		if (MouseDirection.x < 0 && Character.FlipH)
		{
			Character.FlipH = false;
		}
		else if (MouseDirection.x > 0 && Character.FlipH == false)
		{
			Character.FlipH = true;
		}
		Sword.Rotation = (GetLocalMousePosition() - Character.Position).Angle();
		SwordHitbox.KnockbackDirection = (GetLocalMousePosition() - Character.Position);
		if (Sword.Scale.y == 1 && MouseDirection.x > 0){Sword.Scale = new Vector2 (1, -1);}
		else if (Sword.Scale.y == -1 && MouseDirection.x < 0){Sword.Scale = new Vector2 (1, 1);}

	}

	public void GetInput()
	{
		AnimationPlayer SwordAnimationPlayer = GetNode<Node2D>("Sword").GetNode<AnimationPlayer>("SwordAnimationPlayer");
		MovementDirection = Vector2.Zero;
		MovementDirection.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		MovementDirection.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		if (Input.IsActionJustPressed("ui_attack") && !SwordAnimationPlayer.IsPlaying()){SwordAnimationPlayer.Play("Attack");}
	}
}
