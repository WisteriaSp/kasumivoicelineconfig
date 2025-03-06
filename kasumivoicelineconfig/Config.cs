using KasumiVoicelineConfig.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using System.Reflection;

namespace KasumiVoicelineConfig.Configuration
{
	public class Config : Configurable<Config>
	{
        /*
            User Properties:
                - Please put all of your configurable properties here.

            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs

            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
        */

        [Category("Audio")]
        [DisplayName("Navi Patch")]
        [Description("Futaba says Violet instead of Joker. Requires CBT.")]
        [DefaultValue(true)]
        public bool NaviPatch { get; set; } = true;

        [Category("Audio")]
        [DisplayName("New Skill Comments")]
        [Description("Adds Sumi lines for commenting when another party member unlocks a new skill. (eg: \"I'd love to see that in action!\")")]
        [DefaultValue(true)]
        public bool Comments { get; set; } = true;

        [Category("Audio")]
        [DisplayName("Persona Swap Audio")]
        [Description("Replaces persona summoning lines and Arsene's voice to match Cendrillon/Vanadis/Ella, for use with the main mod's \"Violet's Personas\" config option.)")]
        [DefaultValue(false)]
        public bool PersonaSwapAudio { get; set; } = false;
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
	public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}