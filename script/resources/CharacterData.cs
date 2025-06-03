using Godot;

namespace MKWO.Resources;

public partial class CharacterData : MKWOResource
{
	/// <summary> The sprite that appears on the input. </summary>
	[Export] public Texture2D characterSprite;
	/// <summary> The driver's high-quality render. </summary>
	[Export] public Texture2D characterRender;
}
