using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FluentDeploy.Config
{
    public class ConfigLoader
    {
        private Dictionary<string, List<HostInformation>> _inventory = new();
        private Dictionary<string, Dictionary<string, string>> _variables = new ();

        public ConfigLoader()
        { }

        private void MergeInventory(Dictionary<string, List<HostInformation>> inventory)
        {
            foreach (var host in inventory)
            {
                if(!_inventory.ContainsKey(host.Key))
                    _inventory.Add(host.Key, new List<HostInformation>());
                
                _inventory[host.Key].AddRange(host.Value);
            }
        }

        private void MergeVariables(Dictionary<string, Dictionary<string, string>> vars)
        {
            foreach (var variables in vars)
            {
                if(!_variables.ContainsKey(variables.Key))
                    _variables.Add(variables.Key, new Dictionary<string, string>());

                var varDict = _variables[variables.Key];

                foreach (var values in variables.Value)
                {
                    if(!varDict.ContainsKey(values.Key))
                    {
                        varDict.Add(values.Key, values.Value);
                        continue;
                    }

                    varDict[values.Key] = values.Value;
                }
            }
        }

        public ConfigLoader AddInventoryFile(string path)
        {
            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(File.OpenText(path));
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var hosts = deserializer.Deserialize<Dictionary<string, List<HostInformation>>>(parser);
            MergeInventory(hosts);
            return this;
        }

        public ConfigLoader AddVariablesFiles(params string[] paths)
        {
            foreach (var path in paths)
            {
                AddVariablesFile(path);
            }
            return this;
        }
        
        public ConfigLoader AddInventoryFiles(params string[] paths)
        {
            foreach (var path in paths)
            {
                AddInventoryFile(path);
            }
            return this;
        }

        public ConfigLoader AddVariablesFile(string path)
        {
            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(File.OpenText(path));
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser);
            MergeVariables(vars);
            return this;
        }
        
        public ConfigLoader AddCombinedFile(string path)
        {
            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(File.OpenText(path));
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var hosts = deserializer.Deserialize<Dictionary<string, List<HostInformation>>>(parser);
            MergeInventory(hosts);

            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser) ?? 
                       new Dictionary<string, Dictionary<string, string>>();
            MergeVariables(vars);
            return this;
        }

        public ConfigLoader AddEncryptedVariablesFile(string path, string passphrase)
        {
            var encHandler = new EncryptedConfigFileHandler();
            encHandler.Load(path, passphrase);
            var content = encHandler.GetDecryptedFileContent();
            
            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(new StringReader(content));
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser);
            MergeVariables(vars);
            
            return this;
        }

        public BasicConfig Build()
        {
            return new BasicConfig() {GroupHosts = _inventory, GroupHostVars = _variables};
        }
    }
}