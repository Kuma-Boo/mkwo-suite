using Godot;
using Godot.Collections;
using MKWO.Resources;

namespace MKWO.Yahtzee;

public partial class YahtzeeManager : Node
{
	[Export] private CharacterData[] characters;
	[Export] private YahtzeeCategoryData[] categories;

	[Export] private YahtzeeInput[] characterInputs;
	[Export] private YahtzeeInput[] categoryInputs;

	[Export] private Label characterTotalLabel;
	[Export] private Label finalTotalLabel;

	public override void _Ready()
	{
		for (int i = 0; i < characterInputs.Length; i++)
			characterInputs[i].ValueUpdated += RedrawPoints;

		for (int i = 0; i < categoryInputs.Length; i++)
			categoryInputs[i].ValueUpdated += RedrawPoints;

		Reset();
	}

	/// <summary>
	/// Resets all values and re-randomizes the characters/categories.
	/// </summary>
	public void Reset()
	{
		// BE SURE THAT THERE ARE ENOUGH CHARACTERS/CATEGORIES. OTHERWISE, THIS WILL BREAK
		Array<CharacterData> remainingCharacters = [.. characters];
		Array<YahtzeeCategoryData> remainingCategories = [.. categories];

		int index;
		RandomNumberGenerator randomNumberGenerator = new();

		for (int i = 0; i < characterInputs.Length; i++)
		{
			index = randomNumberGenerator.RandiRange(0, remainingCharacters.Count - 1);
			characterInputs[i].SetResource(remainingCharacters[index]);
			remainingCharacters.RemoveAt(index);
		}

		for (int i = 0; i < characterInputs.Length; i++)
		{
			index = randomNumberGenerator.RandiRange(0, remainingCategories.Count - 1);
			categoryInputs[i].SetResource(remainingCategories[index]);
			remainingCategories.RemoveAt(index);
		}

		RedrawPoints();
	}

	public void RedrawPoints()
	{
		int characterTotal = 0;
		for (int i = 0; i < characterInputs.Length; i++)
			characterTotal += characterInputs[i].Points;

		int finalTotal = characterTotal;
		for (int i = 0; i < categoryInputs.Length; i++)
			finalTotal += categoryInputs[i].Points;

		characterTotalLabel.Text = characterTotal.ToString();
		finalTotalLabel.Text = finalTotal.ToString();
	}
}
