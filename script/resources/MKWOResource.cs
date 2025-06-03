using Godot;

namespace MKWO.Resources;

public partial class MKWOResource : Resource
{
	/// <summary> The category or character's name. </summary>
	[Export] public string Name { get; private set; }
}
