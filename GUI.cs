using Godot;
using System;

public class GUI : CanvasLayer
{
	const int MinHealth = 42;
	float MaxPlayerHp = 3;
	Character Player;
	TextureProgress HealthBar;
	Tween HealthBarTween;
	public override void _Ready()
	{
		Player = this.GetParent().GetNode<Character>("Player");
		HealthBar = this.GetNode<TextureProgress>("HealthBar");
		HealthBarTween = HealthBar.GetNode<Tween>("Tween");
		MaxPlayerHp = Player.Health;
		UpdateHealthBar(100);
	}
	public void UpdateHealthBar(int NewHealth)
	{
		var NewHealthBarTween = HealthBarTween.InterpolateProperty(HealthBar, "value", HealthBar.Value, NewHealth, 0.5F, Tween.TransitionType.Quint, Tween.EaseType.Out);
		NewHealthBarTween = HealthBarTween.Start();
	}
	void NewHealth(int NewHp, string Name)
	{
		if(Name == "Player")
		{
			if (NewHp == 4){UpdateHealthBar(10);}
			if (NewHp == 3){UpdateHealthBar(7);}
			if (NewHp == 2){UpdateHealthBar(6);}
			if (NewHp == 1){UpdateHealthBar(5);}
			if (NewHp == 0){UpdateHealthBar(0);}
		}
	}
}
