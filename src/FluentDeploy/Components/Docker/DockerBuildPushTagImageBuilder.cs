using System;
using System.Collections.Generic;
using System.IO;
using FluentDeploy.Commands;
using FluentDeploy.Execution;

namespace FluentDeploy.Components.Docker
{
    public class DockerBuildPushTagImageBuilder : ICommandBuilder
    {
        private string _dockerDir;
        private string _dockerFile;
        private string _imageeName;
        private string _imageTagName;
        private string _remotePath;
        private string _tagName;
        
        private bool _noCache;
        
        private bool _dockerBuild;
        private bool _dockerTag;
        private bool _dockerPush;

        public DockerBuildPushTagImageBuilder BuildImage(string dockerDir, string localImageName, string dockerFile)
        {
            _dockerDir = dockerDir;
            _imageTagName = localImageName;
            _dockerFile = dockerFile;
            _dockerBuild = true;
            return this;
        }
        
        public DockerBuildPushTagImageBuilder Image(string localImageName)
        {
            _imageTagName = localImageName;
            return this;
        }

        public DockerBuildPushTagImageBuilder NoCache()
        {
            _noCache = true;
            return this;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName">tag name without remote path, remote path will me added at push</param>
        /// <returns></returns>
        public DockerBuildPushTagImageBuilder Tag(string tagName)
        {
            _imageTagName = tagName;
            _dockerTag = true;
            return this;
        }
        
        public DockerBuildPushTagImageBuilder Push(string remotePath)
        {
            _remotePath = remotePath;
            _dockerPush = true;
            return this;
        }
        
        public DockerBuildPushTagImageBuilder GetCompleteImageUrl(out string imageName)
        {
            imageName = new Uri(new Uri(_remotePath), _tagName).ToString();
            return this;
        }

        public ExecutionUnit BuildCommands(IHostInfo hostInfo)
        {
            throw new NotImplementedException();
        }

        public void BuildCommands(ICommandContext commandContext)
        {
            throw new NotImplementedException();
        }
    }
}