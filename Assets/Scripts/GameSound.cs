/// <summary>
/// This enum defines the sound types available to play.
/// Each enum value can have AudioClips assigned to it in the SoundManager's Inspector pane.
/// </summary>
public enum GameSound
{
    // It's advisable to keep 'None' as the first option, since it helps exposing this enum in the Inspector.
    // If the first option is already an actual value, then there is no "nothing selected" option.
    None,
    S_ButtonPressed,
    S_CaesarMove,
    S_CharacterSelection,
    S_DanceMoveValidation1,
    S_DanceMoveValidation2,
    S_DanceMoveValidation3,
    S_DanceMoveValidation4,
    S_DanceMoveValidation5,
    S_DaveJump,
    S_DaveMove,
    S_GrooveUpLoop,
    S_MultiplierAppear,
    S_NewTurnIn,
    S_NewTurnOut,
    S_PatternImpactValidation,
    S_RichardBoost,
    ThatSoundS_RichardMove,
}
