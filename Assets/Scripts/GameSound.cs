/// <summary>
/// This enum defines the sound types available to play.
/// Each enum value can have AudioClips assigned to it in the SoundManager's Inspector pane.
/// </summary>
public enum GameSound
{
    // It's advisable to keep 'None' as the first option, since it helps exposing this enum in the Inspector.
    // If the first option is already an actual value, then there is no "nothing selected" option.
    None,
    S_ButtonPressed, // UI DONE
    S_CaesarMove, // DONE
    S_CharacterSelection, // Chara click DONE
    S_DanceMoveValidation1, // NO
    S_DanceMoveValidation2, // NO
    S_DanceMoveValidation3, // NO
    S_DanceMoveValidation4, // NO
    S_DanceMoveValidation5, // NO
    S_DaveJump, // double movement capacity
    S_DaveMove, // DONE
    // S_GrooveUpLoop,
    S_MultiplierAppear, // on combo DONE
    S_NewTurnIn, // DONE
    S_NewTurnOut, // DONE
    S_PatternImpactValidation, // DONE
    S_RichardBoost,
    S_RichardMove, // DONE
    S_GrooveBarDown, // DONE
    S_GrooveBarUp, // DONE
    S_WallUp, // DONE
    S_LDTileAppear, // DONE
    S_AntiGrooveTileEffect, // DONE
    S_IceTileEffect, // DONE
}
