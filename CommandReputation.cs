using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReputationManager
{
    public class CommandReputation : IRocketCommand
    {
        public string Name => "reputation";

        public string Help => "Change someone's reputation.";

        public string Syntax => "[player] <reputation>";

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public List<string> Permissions => new List<string>() { "reputation" };

        public List<string> Aliases => new List<string>() { "rep" };

        public void Execute(IRocketPlayer player, string[] parameters)
        {
            if (parameters.Length == 0 || parameters.Length > 2)
            {
                UnturnedChat.Say(player, ReputationManager.Instance.Translate("help_reputation"));
                return;
            }

            if (parameters.Length == 1)
            {
                if (player is ConsolePlayer)
                {
                    UnturnedChat.Say(player, ReputationManager.Instance.Translate("console_cannot_call"), UnityEngine.Color.red);
                    return;
                }

                if (!int.TryParse(parameters[0], out int reputation))
                {
                    UnturnedChat.Say(player, ReputationManager.Instance.Translate("invalid_reputation"), UnityEngine.Color.red);
                    return;
                }

                ReputationManager.SetReputation((UnturnedPlayer)player, reputation);

                UnturnedChat.Say(player, ReputationManager.Instance.Translate("reputation_set_success", reputation));
            }
            else
            {
                if (!player.HasPermission("reputation.other"))
                {
                    UnturnedChat.Say(player, ReputationManager.Instance.Translate("no_permission"), UnityEngine.Color.red);
                    return;
                }

                UnturnedPlayer target = UnturnedPlayer.FromName(parameters[0]);
                if (target == null)
                {
                    UnturnedChat.Say(player, ReputationManager.Instance.Translate("player_not_found"), UnityEngine.Color.red);
                    return;
                }

                if (!int.TryParse(parameters[1], out int reputation))
                {
                    UnturnedChat.Say(player, ReputationManager.Instance.Translate("invalid_reputation"), UnityEngine.Color.red);
                    return;
                }

                ReputationManager.SetReputation(target, reputation);

                UnturnedChat.Say(player, ReputationManager.Instance.Translate("reputation_set_other_success", target.DisplayName, reputation));
            }
        }
    }
}
