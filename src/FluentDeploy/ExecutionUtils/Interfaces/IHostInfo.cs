using FluentDeploy.DistributionVariants;

namespace FluentDeploy.ExecutionUtils.Interfaces
{
    public interface IHostInfo
    {
        bool PackageManagerMirrorsUpdated { get; }
        int UserId { get;  }
        int UserGroupId { get;  }
        IDistributionVariant DistributionVariant { get;  }
    }
}