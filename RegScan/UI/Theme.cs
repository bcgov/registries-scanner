using System.Drawing;

namespace RegScan.UI
{
    public static class Theme
    {
    // --------- Colour: surface ---------
    public static readonly Color BackgroundPrimary = BaseColourTokens.White;
    public static readonly Color BackgroundSecondary = BaseColourTokens.Grey100;
    public static readonly Color BackgroundInverse = BaseColourTokens.BCBlue;
    public static readonly Color None = BaseColourTokens.Transparent;

    // --------- Colour: text ---------
    public static readonly Color TextPrimary = BaseColourTokens.TextPrimary;
    public static readonly Color TextSecondary = BaseColourTokens.TextSecondary;
    public static readonly Color TextInverse = BaseColourTokens.White;
    public static readonly Color TextDisabled = BaseColourTokens.Grey70;
    public static readonly Color Link = BaseColourTokens.Blue70;

    // --------- Colour: button styling ---------
    // Primary
    public static readonly Color PrimaryButtonBackground = BaseColourTokens.BCBlue;
    public static readonly Color PrimaryButtonHover = BaseColourTokens.Blue70;
    public static readonly Color PrimaryButtonPressed = BaseColourTokens.Blue110;
    // Secondary
    public static readonly Color SecondaryButtonBackground = BaseColourTokens.White;
    public static readonly Color SecondaryButtonOutline = BaseColourTokens.Grey100;
    public static readonly Color SecondaryButtonHoverPressed = BaseColourTokens.Grey10;
    // Tertiary 
    public static readonly Color SecondaryButtonPressed = BaseColourTokens.Grey10;
    public static readonly Color TertiaryButtonHoverPressed = BaseColourTokens.Grey10;

    // --------- Colour: status ---------
    public static readonly Color DangerBackground = BaseColourTokens.DangerDefault;
    public static readonly Color DangerHover = BaseColourTokens.DangerLight;
    public static readonly Color SuccessBackground = BaseColourTokens.Success;
    public static readonly Color WarningBackground = BaseColourTokens.Warning;
    public static readonly Color WarningBackgroundLight = BaseColourTokens.WarningLight;
    public static readonly Color InfoBackground = BaseColourTokens.Blue90;
    public static readonly Color Disabled = BaseColourTokens.Grey10;
    public static readonly Color FocusOutline = BaseColourTokens.Gold;

    // --------- Typography ---------

    /// <summary>
    /// Font family — falls back to Segoe UI when BC Sans is not installed
    /// on the workstation.
    /// </summary>
    public const string FontFamily = "BC Sans, Segoe UI";
    public const string FontFamilyLight = "BC Sans Light, Segoe UI";

    /// <summary>Body / default text.</summary>
    public static readonly Font FontSmall = new Font(FontFamily, 10, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font FontBody = new Font(FontFamily, 11, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font FontBodySmall = new Font(FontFamilyLight, 11, FontStyle.Regular, GraphicsUnit.Point);

        /// <summary>Standard heading levels</summary>
        public static readonly Font H1 = new Font(FontFamily, 14, FontStyle.Bold, GraphicsUnit.Point);
    public static readonly Font H2 = new Font(FontFamily, 12, FontStyle.Bold, GraphicsUnit.Point);
    public static readonly Font H3 = new Font(FontFamily, 12, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font H4 = new Font(FontFamilyLight, 12, FontStyle.Regular, GraphicsUnit.Point);

    /// <summary>Button label.</summary>
    public static readonly Font FontButton = new Font(FontFamily, 10F, FontStyle.Bold);
    }

    /// <summary>
    /// Attempting to remove any and all 'magic' numbers from the code base. These all relate to
    /// styling options. 
    /// </summary>
    public static class Const
    {
        // --------- Spacing (4 px base) ---------

        public const byte SpacingXs = 4;
        public const byte SpacingSm = 8;
        public const byte SpacingMd = 16;
        public const byte SpacingLg = 24;
        public const byte SpacingXl = 32;

        // --------- Sizing ---------

        // Heights -- increase by 4
        public const byte ControlHeightSm = 24;
        public const byte ControlHeightMd = 32;
        public const byte ControlHeightLg = 40;
        public const byte ControlHeightXl = 48;
        // Lengths -- increase by 16
        public const int ControlLengthSm = 60;
        public const int ControlLengthMd = 76;
        public const int ControlLengthLg = 92;
        public const int ControlLengthXl = 108;
        public const int ControlLengthTxtField = 178;

        // --------- Sizing Control ---------
        /// <summary>
        /// Using bytes as that is the smallest storage for an int possible (8 bit unsigned)
        /// MUST be const -> static readonly will not work in switch-case statements
        /// </summary>

        // 1 = small, 2 = medium, 3 = large, 4 = xtra large.
        public const byte SM = 1;
        public const byte MD = 2;
        public const byte LG = 3;
        public const byte XL = 4;
        // 1 = primary, 2 = secondary, 3 = tertiary.
        public const byte PRIMARY = 1;
        public const byte SECONDARY = 2;
        public const byte TERTIARY = 3;
    }

    /// <summary>
    /// Single source of truth for design tokens used in the application. Tokens are intentionally
    /// <c>readonly</c> and strongly-named such that no raw hex / pixel values ever appear in form
    /// designers or services. This will also allow for quick changes to overall theme, and the
    /// ability to add to the theme easily. Attempted to match names and styling settings to the
    /// B.C. Design System as closely as possible. 
    /// For more information on B.C. Design System see:
    /// https://github.com/bcgov/design-system
    /// </summary>
    /// <remarks>
    /// 1.  Use caution when updating these values. They are referenced for various elements above
    /// changes may have wider affects than anticipated. 
    /// 2.  Using colour variable from this class directly in a form element is discourcged. This
    /// will reduce the risks of the first remark.
    /// 3.  Always check if a colour already exists in this class before adding.
    /// </remarks>
    public static class BaseColourTokens
    {
        // Basics
        public static readonly Color White = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color Gold = ColorTranslator.FromHtml("#FCBA19");
        public static readonly Color Transparent = ColorTranslator.FromHtml("#ffffff00");

        // Grey
        public static readonly Color Grey10 = ColorTranslator.FromHtml("#EDEBE9");
        public static readonly Color Grey70 = ColorTranslator.FromHtml("#9F9D9C");
        public static readonly Color Grey100 = ColorTranslator.FromHtml("#353433");

        // Blues
        public static readonly Color BCBlue = ColorTranslator.FromHtml("#013366");
        public static readonly Color Blue50 = ColorTranslator.FromHtml("#3B99FC");
        public static readonly Color Blue70 = ColorTranslator.FromHtml("#1E5189");
        public static readonly Color Blue90 = ColorTranslator.FromHtml("#053662");
        public static readonly Color Blue110 = ColorTranslator.FromHtml("#01264C");

        // Text
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#2D2D2D");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#474543");

        // Statuses
        public static readonly Color DangerDefault = ColorTranslator.FromHtml("#CE3E39");
        public static readonly Color DangerLight = ColorTranslator.FromHtml("#A2312D");
        public static readonly Color Success = ColorTranslator.FromHtml("#42814A");
        public static readonly Color Warning = ColorTranslator.FromHtml("#F8BB47");
        public static readonly Color WarningLight = ColorTranslator.FromHtml("#FEF1D8");
    }
}
