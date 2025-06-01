using Godot;

namespace MKWO.Yahtzee;

public partial class YahtzeeCategory : YahtzeeResource
{
	/// <summary> The list of characters needed to complete the set. Used to write the description. </summary>
	[Export] public YahtzeeCharacter[] characters;
	/// <summary> The number of points to award upon completion of the category. </summary>
	[Export] public int Points { get; private set; }
}
