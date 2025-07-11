﻿using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers.Dialog.Commando;
using SPTarkov.Server.Core.Models.Eft.Dialog;
using SPTarkov.Server.Core.Models.Eft.Profile;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;

namespace _21CustomCommandoCommand;

public record ModMetadata : AbstractModMetadata
{
    public override string Name { get; set; } = "CustomCommandoCommandExample";
    public override string Author { get; set; } = "SPTarkov";
    public override List<string>? Contributors { get; set; }
    public override string Version { get; set; } = "1.0.0";
    public override string SptVersion { get; set; } = "4.0.0";
    public override List<string>? LoadBefore { get; set; }
    public override List<string>? LoadAfter { get; set; }
    public override List<string>? Incompatibilities { get; set; }
    public override Dictionary<string, string>? ModDependencies { get; set; }
    public override string? Url { get; set; }
    public override bool? IsBundleMod { get; set; }
    public override string? Licence { get; set; } = "MIT";
}

[Injectable]
public class CustomCommandoCommand(
    DatabaseServer databaseServer,
    MailSendService mailSendService) : IChatCommand
{
    public string GetCommandPrefix()
    {
        return "test";
    }

    public string GetCommandHelp(string command)
    {
        if (command == "talk")
        {
            return "Usage: test talk";
        }

        return null;
    }

    public List<string> GetCommands()
    {
        return ["talk"];
    }

    public ValueTask<string> Handle(string command, UserDialogInfo commandHandler, string sessionId, SendMessageRequest request)
    {
        if (command == "talk")
        {
            mailSendService.SendUserMessageToPlayer(sessionId, commandHandler, $"IM TALKING! OKAY?!\nHere's the walk speed X config from the DB: {databaseServer.GetTables().Globals.Configuration.WalkSpeed.X}");
            return new ValueTask<string>(request.DialogId);
        }

        return new ValueTask<string>(string.Empty);
    }
}
