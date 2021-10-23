using System;
using FluentDeploy.Config;
using Serilog;

namespace FluentDeploy
{
    public class BuiltinActions
    {
        private static void ExitWithMessage(string msg, int code)
        {
            if (code == 0)
                Log.Information(msg);
            else
                Log.Error(msg);

            Environment.Exit(code);
        }

        private static void InitSecretsFile(string[] args)
        {
            if(args.Length != 2)
                ExitWithMessage("Error invalid number of Arguments: secrets-init <path>",-1);
            
            var path = args[1];
            Log.Information("passphrase:");
            var passphrase = Console.ReadLine()?.Trim();

            if (passphrase.Length < 8)
                ExitWithMessage("Error passphrase to short (min 8 characters)", -1);
            
            new EncryptedConfigFileHandler().Init(path, passphrase);
        }
        
        private static void EditSecretsFile(string[] args)
        {
            if(args.Length != 2)
                ExitWithMessage("Error invalid number of Arguments: secrets-edit <path>",-1);
            
            var path = args[1];
            
            Log.Information("passphrase:");
            var passphrase = Console.ReadLine()?.Trim();
            new EncryptedConfigFileHandler().Edit(path, passphrase);
        }


        public static void ExecuteBuiltinActions(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    return;
                }

                switch (args[0])
                {
                    case "secrets-init":
                        InitSecretsFile(args);
                        break;
                    case "secrets-edit":
                        EditSecretsFile(args);
                        break;
                    
                    case "help":
                        ExitWithMessage("cmds: secrets-init or secrets-edit", 0);
                        break;
                    default: return;
                }

                ExitWithMessage($"{args[0]} successfully executed", 0);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Unexpected error during execution.");
                Environment.Exit(-1);
            }
        }
    }
}