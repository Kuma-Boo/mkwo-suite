using Godot;

namespace MKWO.Resources;

public partial class YahtzeeCategoryData : MKWOResource
{
	/// <summary> The list of characters needed to complete the set. Used to write the description. </summary>
	[Export] public CharacterData[] characters;
	/// <summary> The number of points to award upon completion of the category. </summary>
	[Export] public int Points { get; private set; }
}
