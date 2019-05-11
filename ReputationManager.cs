using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Unturned.Player;
using Rocket.API.Collections;
using Logger = Rocket.Core.Logging.Logger;

namespace ReputationManager
{
    public class ReputationManager : RocketPlugin
    {
        public static ReputationManager Instance;
        public static string Discord = "https://discord.gg/Q89FmUk";

        protected override void Load()
        {
            Logger.Log("Plugin loaded Correctly. Made by IAmSilk & Fixed by educatalan02 - Support: " + Discord);
            Logger.Log("Version: " + Assembly.GetName().Version);
            Instance = this;
        }

        public static void SetReputation(UnturnedPlayer player, int reputation)
        {
            // player.Reputation.set adds the value. Work around to set it specifically.
            player.Reputation = reputation - player.Reputation;
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "help_reputation", "/reputation [player] <amount> - Set your or another player's reputation to the specified amount." },
            { "reputation_set_success", "Successfully set your reputation to {0}." },
            { "reputation_set_other_success", "Successfully set {0}'s reputation to {1}." },
            { "player_not_found", "The specified player cannot be found." },
            { "console_cannot_call", "The console cannot set it's own reputation." },
            { "invalid_reputation", "The specified reputation is invalid." },
            { "no_permission", "You do not have permission to do this." },
            { "invalid_parameters", "Invalid parameters." },
        };
    }
}
