using System;

namespace ClientTester.Commands.DefaultCommands
{
    public class PickupCommand : NitroxCommand
    {
        public PickupCommand()
        {
            Name = "pickup";
            Description = "Picks up an item at location.";
            Syntax = "pickup <guid> <x> <y> <z>";
        }

        public override void Execute(MultiplayerClient client, string[] args)
        {
            assertMinimumArgs(args, 4);

            client.Logic.Item.PickedUp(CommandManager.GetVectorFromArgs(args, 1), new Guid(args[0]), "");
        }
    }
}
