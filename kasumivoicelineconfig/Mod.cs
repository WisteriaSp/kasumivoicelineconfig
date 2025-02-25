using KasumiVoicelineConfig.Configuration;
using KasumiVoicelineConfig.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using CriFs.V2.Hook.Interfaces;
using Ryo.Interfaces;

namespace KasumiVoicelineConfig
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {
        /// <summary>
        /// Provides access to the mod loader API.
        /// </summary>
        private readonly IModLoader _modLoader;
    
        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;
    
        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private readonly ILogger _logger;
    
        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;
    
        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;
    
        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;
    
        public Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

            var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId); // modDir variable for file emulation

            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic

            // Define controllers and other variables, set warning messages

            var criFsController = _modLoader.GetController<ICriFsRedirectorApi>();
            if (criFsController == null || !criFsController.TryGetTarget(out var criFsApi))
            {
                _logger.WriteLine($"Something in CriFS broke! p5rpc.kasumivoice will not load properly!", System.Drawing.Color.Red);
                return;
            }

            var RyoController = _modLoader.GetController<IRyoApi>();
            if (RyoController == null || !RyoController.TryGetTarget(out var ryo))
            {
                _logger.WriteLine($"Something in Ryo broke! p5rpc.kasumivoice will not load properly!", System.Drawing.Color.Red);
                return;
            }

            // Set configuration options

            // Futaba patch
            if (_configuration.NaviPatch)
            {
                criFsApi.AddProbingPath("OptionalModFiles\\FutabaPatch");
            }

            if (_configuration.Comments)
            {
                var audioFolder = Path.Combine(modDir, "OptionalModFiles", "Audio", "NewSkill");
                if (Directory.Exists(audioFolder))
                    ryo.AddAudioPath(audioFolder, null);
                    _logger.WriteLine($"Comment config loaded!", System.Drawing.Color.Purple);
            }

        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion
    
        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
    }
}