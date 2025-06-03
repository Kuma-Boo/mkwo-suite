using Godot;
using MKWO.Resources;

public partial class RandomizerManager : Control
{
	[Export] private CharacterData[] characters;
	[Export] private CharacterData[] npcs;
	[Export] private VehicleData[] vehicles;

	private enum CharacterMode
	{
		MainOnly, // Only allow main drivers
		NpcOnly, // Only allow npc drivers
		All // Allow any driver to be selected
	}
	private CharacterMode characterMode;

	[Export] private TextureRect characterRect;
	[Export] private TextureRect vehicleRect;

	private void UpdateRandomCombo()
	{
		RandomNumberGenerator rng = new();

		// TO-DO update these to use renders instead
		characterRect.Texture = GenerateRandomCharacter(rng).characterSprite;
		vehicleRect.Texture = GenerateRandomVehicle(rng).vehicleSprite;
	}

	private CharacterData GenerateRandomCharacter(RandomNumberGenerator rng)
	{
		if (characterMode == CharacterMode.MainOnly)
			return characters[rng.RandiRange(0, characters.Length - 1)];

		if (characterMode == CharacterMode.NpcOnly)
			return npcs[rng.RandiRange(0, npcs.Length - 1)];

		int characterIndex = rng.RandiRange(0, (characters.Length - 1) + (npcs.Length - 1));
		if (characterIndex >= characters.Length)
			return npcs[characterIndex - characters.Length];

		return characters[characterIndex];
	}

	private VehicleData GenerateRandomVehicle(RandomNumberGenerator rng)
		=> vehicles[rng.RandiRange(0, vehicles.Length - 1)];

	private void UpdateCharacterModeSelection(int selection)
		=> characterMode = (CharacterMode)selection;
}
