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
    ///     ContainerInspectResponse
    /// </summary>
    [DataContract]
    public class ContainerInspectResponse : IEquatable<ContainerInspectResponse>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerInspectResponse" /> class.
        /// </summary>
        /// <param name="id">The ID of the container.</param>
        /// <param name="created">The time the container was created.</param>
        /// <param name="path">The path to the command being run.</param>
        /// <param name="args">The arguments to the command being run.</param>
        /// <param name="state">state.</param>
        /// <param name="image">The container&#39;s image ID.</param>
        /// <param name="resolvConfPath">resolvConfPath.</param>
        /// <param name="hostnamePath">hostnamePath.</param>
        /// <param name="hostsPath">hostsPath.</param>
        /// <param name="logPath">logPath.</param>
        /// <param name="node">TODO.</param>
        /// <param name="name">name.</param>
        /// <param name="restartCount">restartCount.</param>
        /// <param name="driver">driver.</param>
        /// <param name="platform">platform.</param>
        /// <param name="mountLabel">mountLabel.</param>
        /// <param name="processLabel">processLabel.</param>
        /// <param name="appArmorProfile">appArmorProfile.</param>
        /// <param name="execIDs">IDs of exec instances that are running in the container..</param>
        /// <param name="hostConfig">hostConfig.</param>
        /// <param name="graphDriver">graphDriver.</param>
        /// <param name="sizeRw">The size of files that have been created or changed by this container. .</param>
        /// <param name="sizeRootFs">The total size of all the files in this container..</param>
        /// <param name="mounts">mounts.</param>
        /// <param name="config">config.</param>
        /// <param name="networkSettings">networkSettings.</param>
        public ContainerInspectResponse(string id = default, string created = default, string path = default,
            List<string> args = default, ContainerState state = default, string image = default,
            string resolvConfPath = default, string hostnamePath = default, string hostsPath = default,
            string logPath = default, object node = default, string name = default, int restartCount = default,
            string driver = default, string platform = default, string mountLabel = default,
            string processLabel = default, string appArmorProfile = default, List<string> execIDs = default,
            HostConfig hostConfig = default, GraphDriverData graphDriver = default, long sizeRw = default,
            long sizeRootFs = default, List<MountPoint> mounts = default, ContainerConfig config = default,
            NetworkSettings networkSettings = default)
        {
            ExecIDs = execIDs;
            Id = id;
            Created = created;
            Path = path;
            Args = args;
            State = state;
            Image = image;
            ResolvConfPath = resolvConfPath;
            HostnamePath = hostnamePath;
            HostsPath = hostsPath;
            LogPath = logPath;
            Node = node;
            Name = name;
            RestartCount = restartCount;
            Driver = driver;
            Platform = platform;
            MountLabel = mountLabel;
            ProcessLabel = processLabel;
            AppArmorProfile = appArmorProfile;
            ExecIDs = execIDs;
            HostConfig = hostConfig;
            GraphDriver = graphDriver;
            SizeRw = sizeRw;
            SizeRootFs = sizeRootFs;
            Mounts = mounts;
            Config = config;
            NetworkSettings = networkSettings;
        }

        /// <summary>
        ///     The ID of the container
        /// </summary>
        /// <value>The ID of the container</value>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     The time the container was created
        /// </summary>
        /// <value>The time the container was created</value>
        [DataMember(Name = "Created", EmitDefaultValue = false)]
        public string Created { get; set; }

        /// <summary>
        ///     The path to the command being run
        /// </summary>
        /// <value>The path to the command being run</value>
        [DataMember(Name = "Path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        ///     The arguments to the command being run
        /// </summary>
        /// <value>The arguments to the command being run</value>
        [DataMember(Name = "Args", EmitDefaultValue = false)]
        public List<string> Args { get; set; }

        /// <summary>
        ///     Gets or Sets State
        /// </summary>
        [DataMember(Name = "State", EmitDefaultValue = false)]
        public ContainerState State { get; set; }

        /// <summary>
        ///     The container&#39;s image ID
        /// </summary>
        /// <value>The container&#39;s image ID</value>
        [DataMember(Name = "Image", EmitDefaultValue = false)]
        public string Image { get; set; }

        /// <summary>
        ///     Gets or Sets ResolvConfPath
        /// </summary>
        [DataMember(Name = "ResolvConfPath", EmitDefaultValue = false)]
        public string ResolvConfPath { get; set; }

        /// <summary>
        ///     Gets or Sets HostnamePath
        /// </summary>
        [DataMember(Name = "HostnamePath", EmitDefaultValue = false)]
        public string HostnamePath { get; set; }

        /// <summary>
        ///     Gets or Sets HostsPath
        /// </summary>
        [DataMember(Name = "HostsPath", EmitDefaultValue = false)]
        public string HostsPath { get; set; }

        /// <summary>
        ///     Gets or Sets LogPath
        /// </summary>
        [DataMember(Name = "LogPath", EmitDefaultValue = false)]
        public string LogPath { get; set; }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <value>TODO</value>
        [DataMember(Name = "Node", EmitDefaultValue = false)]
        public object Node { get; set; }

        /// <summary>
        ///     Gets or Sets Name
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or Sets RestartCount
        /// </summary>
        [DataMember(Name = "RestartCount", EmitDefaultValue = false)]
        public int RestartCount { get; set; }

        /// <summary>
        ///     Gets or Sets Driver
        /// </summary>
        [DataMember(Name = "Driver", EmitDefaultValue = false)]
        public string Driver { get; set; }

        /// <summary>
        ///     Gets or Sets Platform
        /// </summary>
        [DataMember(Name = "Platform", EmitDefaultValue = false)]
        public string Platform { get; set; }

        /// <summary>
        ///     Gets or Sets MountLabel
        /// </summary>
        [DataMember(Name = "MountLabel", EmitDefaultValue = false)]
        public string MountLabel { get; set; }

        /// <summary>
        ///     Gets or Sets ProcessLabel
        /// </summary>
        [DataMember(Name = "ProcessLabel", EmitDefaultValue = false)]
        public string ProcessLabel { get; set; }

        /// <summary>
        ///     Gets or Sets AppArmorProfile
        /// </summary>
        [DataMember(Name = "AppArmorProfile", EmitDefaultValue = false)]
        public string AppArmorProfile { get; set; }

        /// <summary>
        ///     IDs of exec instances that are running in the container.
        /// </summary>
        /// <value>IDs of exec instances that are running in the container.</value>
        [DataMember(Name = "ExecIDs", EmitDefaultValue = true)]
        public List<string> ExecIDs { get; set; }

        /// <summary>
        ///     Gets or Sets HostConfig
        /// </summary>
        [DataMember(Name = "HostConfig", EmitDefaultValue = false)]
        public HostConfig HostConfig { get; set; }

        /// <summary>
        ///     Gets or Sets GraphDriver
        /// </summary>
        [DataMember(Name = "GraphDriver", EmitDefaultValue = false)]
        public GraphDriverData GraphDriver { get; set; }

        /// <summary>
        ///     The size of files that have been created or changed by this container.
        /// </summary>
        /// <value>The size of files that have been created or changed by this container. </value>
        [DataMember(Name = "SizeRw", EmitDefaultValue = false)]
        public long SizeRw { get; set; }

        /// <summary>
        ///     The total size of all the files in this container.
        /// </summary>
        /// <value>The total size of all the files in this container.</value>
        [DataMember(Name = "SizeRootFs", EmitDefaultValue = false)]
        public long SizeRootFs { get; set; }

        /// <summary>
        ///     Gets or Sets Mounts
        /// </summary>
        [DataMember(Name = "Mounts", EmitDefaultValue = false)]
        public List<MountPoint> Mounts { get; set; }

        /// <summary>
        ///     Gets or Sets Config
        /// </summary>
        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public ContainerConfig Config { get; set; }

        /// <summary>
        ///     Gets or Sets NetworkSettings
        /// </summary>
        [DataMember(Name = "NetworkSettings", EmitDefaultValue = false)]
        public NetworkSettings NetworkSettings { get; set; }

        /// <summary>
        ///     Returns true if ContainerInspectResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ContainerInspectResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContainerInspectResponse input)
        {
            if (input == null)
                return false;

            return
                (
                    Id == input.Id ||
                    Id != null &&
                    Id.Equals(input.Id)
                ) &&
                (
                    Created == input.Created ||
                    Created != null &&
                    Created.Equals(input.Created)
                ) &&
                (
                    Path == input.Path ||
                    Path != null &&
                    Path.Equals(input.Path)
                ) &&
                (
                    Args == input.Args ||
                    Args != null &&
                    input.Args != null &&
                    Args.SequenceEqual(input.Args)
                ) &&
                (
                    State == input.State ||
                    State != null &&
                    State.Equals(input.State)
                ) &&
                (
                    Image == input.Image ||
                    Image != null &&
                    Image.Equals(input.Image)
                ) &&
                (
                    ResolvConfPath == input.ResolvConfPath ||
                    ResolvConfPath != null &&
                    ResolvConfPath.Equals(input.ResolvConfPath)
                ) &&
                (
                    HostnamePath == input.HostnamePath ||
                    HostnamePath != null &&
                    HostnamePath.Equals(input.HostnamePath)
                ) &&
                (
                    HostsPath == input.HostsPath ||
                    HostsPath != null &&
                    HostsPath.Equals(input.HostsPath)
                ) &&
                (
                    LogPath == input.LogPath ||
                    LogPath != null &&
                    LogPath.Equals(input.LogPath)
                ) &&
                (
                    Node == input.Node ||
                    Node != null &&
                    Node.Equals(input.Node)
                ) &&
                (
                    Name == input.Name ||
                    Name != null &&
                    Name.Equals(input.Name)
                ) &&
                (
                    RestartCount == input.RestartCount ||
                    RestartCount != null &&
                    RestartCount.Equals(input.RestartCount)
                ) &&
                (
                    Driver == input.Driver ||
                    Driver != null &&
                    Driver.Equals(input.Driver)
                ) &&
                (
                    Platform == input.Platform ||
                    Platform != null &&
                    Platform.Equals(input.Platform)
                ) &&
                (
                    MountLabel == input.MountLabel ||
                    MountLabel != null &&
                    MountLabel.Equals(input.MountLabel)
                ) &&
                (
                    ProcessLabel == input.ProcessLabel ||
                    ProcessLabel != null &&
                    ProcessLabel.Equals(input.ProcessLabel)
                ) &&
                (
                    AppArmorProfile == input.AppArmorProfile ||
                    AppArmorProfile != null &&
                    AppArmorProfile.Equals(input.AppArmorProfile)
                ) &&
                (
                    ExecIDs == input.ExecIDs ||
                    ExecIDs != null &&
                    input.ExecIDs != null &&
                    ExecIDs.SequenceEqual(input.ExecIDs)
                ) &&
                (
                    HostConfig == input.HostConfig ||
                    HostConfig != null &&
                    HostConfig.Equals(input.HostConfig)
                ) &&
                (
                    GraphDriver == input.GraphDriver ||
                    GraphDriver != null &&
                    GraphDriver.Equals(input.GraphDriver)
                ) &&
                (
                    SizeRw == input.SizeRw ||
                    SizeRw != null &&
                    SizeRw.Equals(input.SizeRw)
                ) &&
                (
                    SizeRootFs == input.SizeRootFs ||
                    SizeRootFs != null &&
                    SizeRootFs.Equals(input.SizeRootFs)
                ) &&
                (
                    Mounts == input.Mounts ||
                    Mounts != null &&
                    input.Mounts != null &&
                    Mounts.SequenceEqual(input.Mounts)
                ) &&
                (
                    Config == input.Config ||
                    Config != null &&
                    Config.Equals(input.Config)
                ) &&
                (
                    NetworkSettings == input.NetworkSettings ||
                    NetworkSettings != null &&
                    NetworkSettings.Equals(input.NetworkSettings)
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
            sb.Append("class ContainerInspectResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Created: ").Append(Created).Append("\n");
            sb.Append("  Path: ").Append(Path).Append("\n");
            sb.Append("  Args: ").Append(Args).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  ResolvConfPath: ").Append(ResolvConfPath).Append("\n");
            sb.Append("  HostnamePath: ").Append(HostnamePath).Append("\n");
            sb.Append("  HostsPath: ").Append(HostsPath).Append("\n");
            sb.Append("  LogPath: ").Append(LogPath).Append("\n");
            sb.Append("  Node: ").Append(Node).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  RestartCount: ").Append(RestartCount).Append("\n");
            sb.Append("  Driver: ").Append(Driver).Append("\n");
            sb.Append("  Platform: ").Append(Platform).Append("\n");
            sb.Append("  MountLabel: ").Append(MountLabel).Append("\n");
            sb.Append("  ProcessLabel: ").Append(ProcessLabel).Append("\n");
            sb.Append("  AppArmorProfile: ").Append(AppArmorProfile).Append("\n");
            sb.Append("  ExecIDs: ").Append(ExecIDs).Append("\n");
            sb.Append("  HostConfig: ").Append(HostConfig).Append("\n");
            sb.Append("  GraphDriver: ").Append(GraphDriver).Append("\n");
            sb.Append("  SizeRw: ").Append(SizeRw).Append("\n");
            sb.Append("  SizeRootFs: ").Append(SizeRootFs).Append("\n");
            sb.Append("  Mounts: ").Append(Mounts).Append("\n");
            sb.Append("  Config: ").Append(Config).Append("\n");
            sb.Append("  NetworkSettings: ").Append(NetworkSettings).Append("\n");
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
            return Equals(input as ContainerInspectResponse);
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
                if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                if (Created != null)
                    hashCode = hashCode * 59 + Created.GetHashCode();
                if (Path != null)
                    hashCode = hashCode * 59 + Path.GetHashCode();
                if (Args != null)
                    hashCode = hashCode * 59 + Args.GetHashCode();
                if (State != null)
                    hashCode = hashCode * 59 + State.GetHashCode();
                if (Image != null)
                    hashCode = hashCode * 59 + Image.GetHashCode();
                if (ResolvConfPath != null)
                    hashCode = hashCode * 59 + ResolvConfPath.GetHashCode();
                if (HostnamePath != null)
                    hashCode = hashCode * 59 + HostnamePath.GetHashCode();
                if (HostsPath != null)
                    hashCode = hashCode * 59 + HostsPath.GetHashCode();
                if (LogPath != null)
                    hashCode = hashCode * 59 + LogPath.GetHashCode();
                if (Node != null)
                    hashCode = hashCode * 59 + Node.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (RestartCount != null)
                    hashCode = hashCode * 59 + RestartCount.GetHashCode();
                if (Driver != null)
                    hashCode = hashCode * 59 + Driver.GetHashCode();
                if (Platform != null)
                    hashCode = hashCode * 59 + Platform.GetHashCode();
                if (MountLabel != null)
                    hashCode = hashCode * 59 + MountLabel.GetHashCode();
                if (ProcessLabel != null)
                    hashCode = hashCode * 59 + ProcessLabel.GetHashCode();
                if (AppArmorProfile != null)
                    hashCode = hashCode * 59 + AppArmorProfile.GetHashCode();
                if (ExecIDs != null)
                    hashCode = hashCode * 59 + ExecIDs.GetHashCode();
                if (HostConfig != null)
                    hashCode = hashCode * 59 + HostConfig.GetHashCode();
                if (GraphDriver != null)
                    hashCode = hashCode * 59 + GraphDriver.GetHashCode();
                if (SizeRw != null)
                    hashCode = hashCode * 59 + SizeRw.GetHashCode();
                if (SizeRootFs != null)
                    hashCode = hashCode * 59 + SizeRootFs.GetHashCode();
                if (Mounts != null)
                    hashCode = hashCode * 59 + Mounts.GetHashCode();
                if (Config != null)
                    hashCode = hashCode * 59 + Config.GetHashCode();
                if (NetworkSettings != null)
                    hashCode = hashCode * 59 + NetworkSettings.GetHashCode();
                return hashCode;
            }
        }
    }
}