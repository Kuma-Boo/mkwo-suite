using Godot;
using MKWO.Resources;

namespace MKWO.Yahtzee;

/// <summary> Represents a character/type, along with its sprite/description and input value. </summary>
public partial class YahtzeeInput : Control
{
	[Signal] public delegate void ValueUpdatedEventHandler();

	[Export] private MKWOResource resource;
	[Export] private Label nameLabel;
	[Export] private TextureRect characterSprite;
	[Export] private TextureRect characterShadowSprite;
	/// <summary> The number of points assigned to the current input. </summary>
	[Export] private LineEdit pointEdit;
	[Export] private Label categoryPointLabel;
	[Export] private Label categoryDescriptionLabel;
	private bool isCharacter;
	public int Points { get; private set; }

	public override void _Ready()
	{
		pointEdit.TextSubmitted += ValueChanged;
		pointEdit.FocusExited += ValueChanged;
	}

	public void SetResource(MKWOResource newResource)
	{
		resource = newResource;

		isCharacter = resource is CharacterData;

		nameLabel.Text = resource.Name;
		characterSprite.Visible = isCharacter;
		pointEdit.Text = string.Empty;
		Points = 0;
		categoryPointLabel.Visible = !isCharacter;
		categoryDescriptionLabel.Visible = !isCharacter;

		if (isCharacter)
		{
			characterSprite.Texture = (resource as CharacterData).characterSprite;
			characterShadowSprite.Texture = characterSprite.Texture;
			return;
		}

		YahtzeeCategoryData category = resource as YahtzeeCategoryData;
		categoryPointLabel.Text = category.Points != 0 ? $"({category.Points} points)" : "(??? points)";

		// Construct the description string
		string categoryDescription = string.Empty;
		foreach (CharacterData character in category.characters)
			categoryDescription += ", " + character.Name;

		if (!string.IsNullOrEmpty(categoryDescription))
		{
			// Trim excess description text
			categoryDescription = categoryDescription.TrimPrefix(", ");
		}

		categoryDescriptionLabel.Text = categoryDescription;
	}

	private void ValidateInput()
	{
		if (string.IsNullOrEmpty(pointEdit.Text) || !pointEdit.Text.IsValidInt() || pointEdit.Text.ToInt() < 0)
		{
			pointEdit.Text = string.Empty; // Invalid input
			Points = 0;
		}
		else
		{
			Points = pointEdit.Text.ToInt();
		}

		EmitSignal(SignalName.ValueUpdated);
	}

	private void ValueChanged(string text)
	{
		pointEdit.ReleaseFocus();
		ValidateInput();
	}

	private void ValueChanged() => ValidateInput();
}
