using FluentDeploy.Commands;
using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public abstract class FileStateBuilderBase<T> : BaseCommandBuilder<T> where T : BaseCommandBuilder<T>
    {
        protected string _user;
        protected string _group;
        protected int _userId;
        protected int _groupId;
        protected bool _currentUser;
        protected short? _permissions;
        protected readonly FileOperationCommand _fileOperationCommand;
        private readonly IHostInfo _hostInfo;
        
        protected FileStateBuilderBase(IHostInfo info, string path, FileOperationType type)
        {
            _hostInfo = info;
            _fileOperationCommand = new FileOperationCommand()
            {
                FileOperationType = type,
                Path = path,
            };
        }

        public T Owner(string userName) => 
            FluentExec(() => _user = userName);
        
        public T Group(string groupName) => 
            FluentExec(() => _group = groupName);
        
        public T Owner(int userId) => 
            FluentExec(() => _fileOperationCommand.UserId  = userId);
        
        public T Group(int groupId) => 
            FluentExec(() =>_fileOperationCommand.GroupId = groupId);
        
        public T CurrentUserAsOwnerAndGroup() => 
            FluentExec(() => _currentUser = true);
        
        public T Permissions(short permissions) => 
            FluentExec(() => _permissions = permissions);

        protected void PrepareCommand(IExecutionContext executor)
        {
            if (_currentUser)
            {
                _fileOperationCommand.GroupId = _hostInfo.UserGroupId;
                _fileOperationCommand.UserId = _hostInfo.UserId;
            }
            else
            {
                if (_user != null) _fileOperationCommand.UserId = executor.GetUserIdOfUser(_user);
                if (_group != null) _fileOperationCommand.GroupId = executor.GetGroupIdOfUser(_group);
            }
            
            _fileOperationCommand.Permissions = _permissions;
        }
    }
}