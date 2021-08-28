/*
 * Docker Engine API
 *
 * The Engine API is an HTTP API served by Docker Engine. It is the API the Docker client uses to communicate with the Engine, so everything the Docker client can do can be done with the API.  Most of the client's commands map directly to API endpoints (e.g. `docker ps` is `GET /containers/json`). The notable exception is running containers, which consists of several API calls.  # Errors  The API uses standard HTTP status codes to indicate the success or failure of the API call. The body of the response will be JSON in the following format:  ``` {   \"message\": \"page not found\" } ```  # Versioning  The API is usually changed in each release, so API calls are versioned to ensure that clients don't break. To lock to a specific version of the API, you prefix the URL with its version, for example, call `/v1.30/info` to use the v1.30 version of the `/info` endpoint. If the API version specified in the URL is not supported by the daemon, a HTTP `400 Bad Request` error message is returned.  If you omit the version-prefix, the current version of the API (v1.40) is used. For example, calling `/info` is the same as calling `/v1.40/info`. Using the API without a version-prefix is deprecated and will be removed in a future release.  Engine releases in the near future should support this version of the API, so your client will continue to work even if it is talking to a newer Engine.  The API uses an open schema model, which means server may add extra properties to responses. Likewise, the server will ignore any extra query parameters and request body properties. When you write clients, you need to ignore additional properties in responses to ensure they do not break when talking to newer daemons.   # Authentication  Authentication for registries is handled client side. The client has to send authentication details to various endpoints that need to communicate with registries, such as `POST /images/(name)/push`. These are sent as `X-Registry-Auth` header as a [base64url encoded](https://tools.ietf.org/html/rfc4648#section-5) (JSON) string with the following structure:  ``` {   \"username\": \"string\",   \"password\": \"string\",   \"email\": \"string\",   \"serveraddress\": \"string\" } ```  The `serveraddress` is a domain/IP without a protocol. Throughout this structure, double quotes are required.  If you have already got an identity token from the [`/auth` endpoint](#operation/SystemAuth), you can just pass this instead of credentials:  ``` {   \"identitytoken\": \"9cbaf023786cd7...\" } ``` 
 *
 * The version of the OpenAPI document: 1.40
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     Response of Engine API: GET \&quot;/version\&quot;
    /// </summary>
    [DataContract]
    public class SystemVersion : IEquatable<SystemVersion>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SystemVersion" /> class.
        /// </summary>
        /// <param name="platform">platform.</param>
        /// <param name="components">Information about system components .</param>
        /// <param name="version">The version of the daemon.</param>
        /// <param name="apiVersion">The default (and highest) API version that is supported by the daemon .</param>
        /// <param name="minAPIVersion">The minimum API version that is supported by the daemon .</param>
        /// <param name="gitCommit">The Git commit of the source code that was used to build the daemon .</param>
        /// <param name="goVersion">The version Go used to compile the daemon, and the version of the Go runtime in use. .</param>
        /// <param name="os">The operating system that the daemon is running on (\&quot;linux\&quot; or \&quot;windows\&quot;) .</param>
        /// <param name="arch">The architecture that the daemon is running on .</param>
        /// <param name="kernelVersion">
        ///     The kernel version (&#x60;uname -r&#x60;) that the daemon is running on.  This field is
        ///     omitted when empty. .
        /// </param>
        /// <param name="experimental">
        ///     Indicates if the daemon is started with experimental features enabled.  This field is
        ///     omitted when empty / false. .
        /// </param>
        /// <param name="buildTime">The date and time that the daemon was compiled. .</param>
        public SystemVersion(SystemVersionPlatform platform = default,
            List<SystemVersionComponents> components = default, string version = default, string apiVersion = default,
            string minAPIVersion = default, string gitCommit = default, string goVersion = default, string os = default,
            string arch = default, string kernelVersion = default, bool experimental = default,
            string buildTime = default)
        {
            Platform = platform;
            Components = components;
            _Version = version;
            ApiVersion = apiVersion;
            MinAPIVersion = minAPIVersion;
            GitCommit = gitCommit;
            GoVersion = goVersion;
            Os = os;
            Arch = arch;
            KernelVersion = kernelVersion;
            Experimental = experimental;
            BuildTime = buildTime;
        }

        /// <summary>
        ///     Gets or Sets Platform
        /// </summary>
        [DataMember(Name = "Platform", EmitDefaultValue = false)]
        public SystemVersionPlatform Platform { get; set; }

        /// <summary>
        ///     Information about system components
        /// </summary>
        /// <value>Information about system components </value>
        [DataMember(Name = "Components", EmitDefaultValue = false)]
        public List<SystemVersionComponents> Components { get; set; }

        /// <summary>
        ///     The version of the daemon
        /// </summary>
        /// <value>The version of the daemon</value>
        [DataMember(Name = "Version", EmitDefaultValue = false)]
        public string _Version { get; set; }

        /// <summary>
        ///     The default (and highest) API version that is supported by the daemon
        /// </summary>
        /// <value>The default (and highest) API version that is supported by the daemon </value>
        [DataMember(Name = "ApiVersion", EmitDefaultValue = false)]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     The minimum API version that is supported by the daemon
        /// </summary>
        /// <value>The minimum API version that is supported by the daemon </value>
        [DataMember(Name = "MinAPIVersion", EmitDefaultValue = false)]
        public string MinAPIVersion { get; set; }

        /// <summary>
        ///     The Git commit of the source code that was used to build the daemon
        /// </summary>
        /// <value>The Git commit of the source code that was used to build the daemon </value>
        [DataMember(Name = "GitCommit", EmitDefaultValue = false)]
        public string GitCommit { get; set; }

        /// <summary>
        ///     The version Go used to compile the daemon, and the version of the Go runtime in use.
        /// </summary>
        /// <value>The version Go used to compile the daemon, and the version of the Go runtime in use. </value>
        [DataMember(Name = "GoVersion", EmitDefaultValue = false)]
        public string GoVersion { get; set; }

        /// <summary>
        ///     The operating system that the daemon is running on (\&quot;linux\&quot; or \&quot;windows\&quot;)
        /// </summary>
        /// <value>The operating system that the daemon is running on (\&quot;linux\&quot; or \&quot;windows\&quot;) </value>
        [DataMember(Name = "Os", EmitDefaultValue = false)]
        public string Os { get; set; }

        /// <summary>
        ///     The architecture that the daemon is running on
        /// </summary>
        /// <value>The architecture that the daemon is running on </value>
        [DataMember(Name = "Arch", EmitDefaultValue = false)]
        public string Arch { get; set; }

        /// <summary>
        ///     The kernel version (&#x60;uname -r&#x60;) that the daemon is running on.  This field is omitted when empty.
        /// </summary>
        /// <value>The kernel version (&#x60;uname -r&#x60;) that the daemon is running on.  This field is omitted when empty. </value>
        [DataMember(Name = "KernelVersion", EmitDefaultValue = false)]
        public string KernelVersion { get; set; }

        /// <summary>
        ///     Indicates if the daemon is started with experimental features enabled.  This field is omitted when empty / false.
        /// </summary>
        /// <value>
        ///     Indicates if the daemon is started with experimental features enabled.  This field is omitted when empty /
        ///     false.
        /// </value>
        [DataMember(Name = "Experimental", EmitDefaultValue = false)]
        public bool Experimental { get; set; }

        /// <summary>
        ///     The date and time that the daemon was compiled.
        /// </summary>
        /// <value>The date and time that the daemon was compiled. </value>
        [DataMember(Name = "BuildTime", EmitDefaultValue = false)]
        public string BuildTime { get; set; }

        /// <summary>
        ///     Returns true if SystemVersion instances are equal
        /// </summary>
        /// <param name="input">Instance of SystemVersion to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SystemVersion input)
        {
            if (input == null)
                return false;

            return
                (
                    Platform == input.Platform ||
                    Platform != null &&
                    Platform.Equals(input.Platform)
                ) &&
                (
                    Components == input.Components ||
                    Components != null &&
                    input.Components != null &&
                    Components.SequenceEqual(input.Components)
                ) &&
                (
                    _Version == input._Version ||
                    _Version != null &&
                    _Version.Equals(input._Version)
                ) &&
                (
                    ApiVersion == input.ApiVersion ||
                    ApiVersion != null &&
                    ApiVersion.Equals(input.ApiVersion)
                ) &&
                (
                    MinAPIVersion == input.MinAPIVersion ||
                    MinAPIVersion != null &&
                    MinAPIVersion.Equals(input.MinAPIVersion)
                ) &&
                (
                    GitCommit == input.GitCommit ||
                    GitCommit != null &&
                    GitCommit.Equals(input.GitCommit)
                ) &&
                (
                    GoVersion == input.GoVersion ||
                    GoVersion != null &&
                    GoVersion.Equals(input.GoVersion)
                ) &&
                (
                    Os == input.Os ||
                    Os != null &&
                    Os.Equals(input.Os)
                ) &&
                (
                    Arch == input.Arch ||
                    Arch != null &&
                    Arch.Equals(input.Arch)
                ) &&
                (
                    KernelVersion == input.KernelVersion ||
                    KernelVersion != null &&
                    KernelVersion.Equals(input.KernelVersion)
                ) &&
                (
                    Experimental == input.Experimental ||
                    Experimental != null &&
                    Experimental.Equals(input.Experimental)
                ) &&
                (
                    BuildTime == input.BuildTime ||
                    BuildTime != null &&
                    BuildTime.Equals(input.BuildTime)
                );
        }

        /// <summary>
        ///     To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SystemVersion {\n");
            sb.Append("  Platform: ").Append(Platform).Append("\n");
            sb.Append("  Components: ").Append(Components).Append("\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
            sb.Append("  ApiVersion: ").Append(ApiVersion).Append("\n");
            sb.Append("  MinAPIVersion: ").Append(MinAPIVersion).Append("\n");
            sb.Append("  GitCommit: ").Append(GitCommit).Append("\n");
            sb.Append("  GoVersion: ").Append(GoVersion).Append("\n");
            sb.Append("  Os: ").Append(Os).Append("\n");
            sb.Append("  Arch: ").Append(Arch).Append("\n");
            sb.Append("  KernelVersion: ").Append(KernelVersion).Append("\n");
            sb.Append("  Experimental: ").Append(Experimental).Append("\n");
            sb.Append("  BuildTime: ").Append(BuildTime).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as SystemVersion);
        }

        /// <summary>
        ///     Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                if (Platform != null)
                    hashCode = hashCode * 59 + Platform.GetHashCode();
                if (Components != null)
                    hashCode = hashCode * 59 + Components.GetHashCode();
                if (_Version != null)
                    hashCode = hashCode * 59 + _Version.GetHashCode();
                if (ApiVersion != null)
                    hashCode = hashCode * 59 + ApiVersion.GetHashCode();
                if (MinAPIVersion != null)
                    hashCode = hashCode * 59 + MinAPIVersion.GetHashCode();
                if (GitCommit != null)
                    hashCode = hashCode * 59 + GitCommit.GetHashCode();
                if (GoVersion != null)
                    hashCode = hashCode * 59 + GoVersion.GetHashCode();
                if (Os != null)
                    hashCode = hashCode * 59 + Os.GetHashCode();
                if (Arch != null)
                    hashCode = hashCode * 59 + Arch.GetHashCode();
                if (KernelVersion != null)
                    hashCode = hashCode * 59 + KernelVersion.GetHashCode();
                if (Experimental != null)
                    hashCode = hashCode * 59 + Experimental.GetHashCode();
                if (BuildTime != null)
                    hashCode = hashCode * 59 + BuildTime.GetHashCode();
                return hashCode;
            }
        }
    }
}