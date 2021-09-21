using System;
using System.IO;
using System.Net.Http;
using FluentDeploy.Commands;
using FluentDeploy.Exceptions;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Etc
{
    public class Gpg : BaseCommandBuilder<Gpg>
    {
        private string _keyUrl = null;
        private string _keyRingPath = null;

        public static Gpg InstallFromUrl(string sourceUrl, string targetPath) => new Gpg()
        {
            _keyUrl = sourceUrl,
            _keyRingPath = targetPath
        };
        
        protected override void Execute(IExecutionContext context)
        {
            var client = new HttpClient();
            var response = client.GetAsync(_keyUrl).Result;

            if (!response.IsSuccessStatusCode)
                throw new FluentDeployException($"Error, invalid return code while downloading key file, invalid return code {response.StatusCode}");

            var keyContent = response.Content.ReadAsStringAsync().Result;

            var tmpFilePath = Path.Combine(context.SystemTmpPath, $"tmp_key_file_{DateTime.Now.Ticks}");

            FileSystem.FileSystem.File(context, tmpFilePath)
                .Content(keyContent)
                .ExecuteOn(context);

            context.ExecuteCommand(ConsoleCommand.Exec($"gpg")
                .WithArguments("--batch", "--dearmor", "-o", _keyRingPath, tmpFilePath));

            FileSystem.FileSystem.Delete(tmpFilePath)
                .ExecuteOn(context);
        }
    }
}