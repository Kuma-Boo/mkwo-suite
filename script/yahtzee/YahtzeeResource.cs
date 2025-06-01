using Godot;

namespace MKWO.Yahtzee;

public partial class YahtzeeResource : Resource
{
	/// <summary> The category or character's name. </summary>
	[Export] public string Name { get; private set; }
}
