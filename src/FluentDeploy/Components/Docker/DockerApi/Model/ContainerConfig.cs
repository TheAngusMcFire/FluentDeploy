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
    ///     Configuration for a container that is portable between hosts
    /// </summary>
    [DataContract]
    public class ContainerConfig : IEquatable<ContainerConfig>, IValidatableObject
    {
        public ContainerConfig()
        {
        
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerConfig" /> class.
        /// </summary>
        /// <param name="hostname">The hostname to use for the container, as a valid RFC 1123 hostname..</param>
        /// <param name="domainname">The domain name to use for the container..</param>
        /// <param name="user">The user that commands are run as inside the container..</param>
        /// <param name="attachStdin">Whether to attach to &#x60;stdin&#x60;. (default to false).</param>
        /// <param name="attachStdout">Whether to attach to &#x60;stdout&#x60;. (default to true).</param>
        /// <param name="attachStderr">Whether to attach to &#x60;stderr&#x60;. (default to true).</param>
        /// <param name="exposedPorts">
        ///     An object mapping ports to an empty object in the form:  &#x60;{\&quot;&lt;port&gt;/&lt;
        ///     tcp|udp|sctp&gt;\&quot;: {}}&#x60; .
        /// </param>
        /// <param name="tty">
        ///     Attach standard streams to a TTY, including &#x60;stdin&#x60; if it is not closed.  (default to
        ///     false).
        /// </param>
        /// <param name="openStdin">Open &#x60;stdin&#x60; (default to false).</param>
        /// <param name="stdinOnce">Close &#x60;stdin&#x60; after one attached client disconnects (default to false).</param>
        /// <param name="env">
        ///     A list of environment variables to set inside the container in the form &#x60;[\&quot;VAR&#x3D;value\
        ///     &quot;, ...]&#x60;. A variable without &#x60;&#x3D;&#x60; is removed from the environment, rather than to have an
        ///     empty value. .
        /// </param>
        /// <param name="cmd">Command to run specified as a string or an array of strings. .</param>
        /// <param name="healthcheck">healthcheck.</param>
        /// <param name="argsEscaped">Command is already escaped (Windows only).</param>
        /// <param name="image">The name of the image to use when creating the container/ .</param>
        /// <param name="volumes">An object mapping mount point paths inside the container to empty objects. .</param>
        /// <param name="workingDir">The working directory for commands to run in..</param>
        /// <param name="entrypoint">
        ///     The entry point for the container as a string or an array of strings.  If the array consists
        ///     of exactly one empty string (&#x60;[\&quot;\&quot;]&#x60;) then the entry point is reset to system default (i.e.,
        ///     the entry point used by docker when there is no &#x60;ENTRYPOINT&#x60; instruction in the &#x60;Dockerfile&#x60;).
        ///     .
        /// </param>
        /// <param name="networkDisabled">Disable networking for the container..</param>
        /// <param name="macAddress">MAC address of the container..</param>
        /// <param name="onBuild">&#x60;ONBUILD&#x60; metadata that were defined in the image&#39;s &#x60;Dockerfile&#x60;. .</param>
        /// <param name="labels">User-defined key/value metadata..</param>
        /// <param name="stopSignal">Signal to stop a container as a string or unsigned integer.  (default to &quot;SIGTERM&quot;).</param>
        /// <param name="stopTimeout">Timeout to stop a container in seconds..</param>
        /// <param name="shell">Shell for when &#x60;RUN&#x60;, &#x60;CMD&#x60;, and &#x60;ENTRYPOINT&#x60; uses a shell. .</param>
        public ContainerConfig(string hostname = default, string domainname = default, string user = default,
            bool attachStdin = false, bool attachStdout = true, bool attachStderr = true,
            Dictionary<string, object> exposedPorts = default, bool tty = false, bool openStdin = false,
            bool stdinOnce = false, List<string> env = default, List<string> cmd = default,
            HealthConfig healthcheck = default, bool argsEscaped = default, string image = default,
            Dictionary<string, object> volumes = default, string workingDir = default,
            List<string> entrypoint = default, bool networkDisabled = default, string macAddress = default,
            List<string> onBuild = default, Dictionary<string, string> labels = default, string stopSignal = "SIGTERM",
            int stopTimeout = default, List<string> shell = default)
        {
            Hostname = hostname;
            Domainname = domainname;
            User = user;
            // use default value if no "attachStdin" provided
            if (attachStdin == null)
                AttachStdin = false;
            else
                AttachStdin = attachStdin;
            // use default value if no "attachStdout" provided
            if (attachStdout == null)
                AttachStdout = true;
            else
                AttachStdout = attachStdout;
            // use default value if no "attachStderr" provided
            if (attachStderr == null)
                AttachStderr = true;
            else
                AttachStderr = attachStderr;
            ExposedPorts = exposedPorts;
            // use default value if no "tty" provided
            if (tty == null)
                Tty = false;
            else
                Tty = tty;
            // use default value if no "openStdin" provided
            if (openStdin == null)
                OpenStdin = false;
            else
                OpenStdin = openStdin;
            // use default value if no "stdinOnce" provided
            if (stdinOnce == null)
                StdinOnce = false;
            else
                StdinOnce = stdinOnce;
            Env = env;
            Cmd = cmd;
            Healthcheck = healthcheck;
            ArgsEscaped = argsEscaped;
            Image = image;
            Volumes = volumes;
            WorkingDir = workingDir;
            Entrypoint = entrypoint;
            NetworkDisabled = networkDisabled;
            MacAddress = macAddress;
            OnBuild = onBuild;
            Labels = labels;
            // use default value if no "stopSignal" provided
            if (stopSignal == null)
                StopSignal = "SIGTERM";
            else
                StopSignal = stopSignal;
            StopTimeout = stopTimeout;
            Shell = shell;
        }

        /// <summary>
        ///     The hostname to use for the container, as a valid RFC 1123 hostname.
        /// </summary>
        /// <value>The hostname to use for the container, as a valid RFC 1123 hostname.</value>
        [DataMember(Name = "Hostname", EmitDefaultValue = false)]
        public string Hostname { get; set; }

        /// <summary>
        ///     The domain name to use for the container.
        /// </summary>
        /// <value>The domain name to use for the container.</value>
        [DataMember(Name = "Domainname", EmitDefaultValue = false)]
        public string Domainname { get; set; }

        /// <summary>
        ///     The user that commands are run as inside the container.
        /// </summary>
        /// <value>The user that commands are run as inside the container.</value>
        [DataMember(Name = "User", EmitDefaultValue = false)]
        public string User { get; set; }

        /// <summary>
        ///     Whether to attach to &#x60;stdin&#x60;.
        /// </summary>
        /// <value>Whether to attach to &#x60;stdin&#x60;.</value>
        [DataMember(Name = "AttachStdin", EmitDefaultValue = false)]
        public bool AttachStdin { get; set; }

        /// <summary>
        ///     Whether to attach to &#x60;stdout&#x60;.
        /// </summary>
        /// <value>Whether to attach to &#x60;stdout&#x60;.</value>
        [DataMember(Name = "AttachStdout", EmitDefaultValue = false)]
        public bool AttachStdout { get; set; }

        /// <summary>
        ///     Whether to attach to &#x60;stderr&#x60;.
        /// </summary>
        /// <value>Whether to attach to &#x60;stderr&#x60;.</value>
        [DataMember(Name = "AttachStderr", EmitDefaultValue = false)]
        public bool AttachStderr { get; set; }

        /// <summary>
        ///     An object mapping ports to an empty object in the form:  &#x60;{\&quot;&lt;port&gt;/&lt;tcp|udp|sctp&gt;\&quot;:
        ///     {}}&#x60;
        /// </summary>
        /// <value>
        ///     An object mapping ports to an empty object in the form:  &#x60;{\&quot;&lt;port&gt;/&lt;tcp|udp|sctp&gt;\&quot;:
        ///     {}}&#x60;
        /// </value>
        [DataMember(Name = "ExposedPorts", EmitDefaultValue = false)]
        public Dictionary<string, object> ExposedPorts { get; set; }

        /// <summary>
        ///     Attach standard streams to a TTY, including &#x60;stdin&#x60; if it is not closed.
        /// </summary>
        /// <value>Attach standard streams to a TTY, including &#x60;stdin&#x60; if it is not closed. </value>
        [DataMember(Name = "Tty", EmitDefaultValue = false)]
        public bool Tty { get; set; }

        /// <summary>
        ///     Open &#x60;stdin&#x60;
        /// </summary>
        /// <value>Open &#x60;stdin&#x60;</value>
        [DataMember(Name = "OpenStdin", EmitDefaultValue = false)]
        public bool OpenStdin { get; set; }

        /// <summary>
        ///     Close &#x60;stdin&#x60; after one attached client disconnects
        /// </summary>
        /// <value>Close &#x60;stdin&#x60; after one attached client disconnects</value>
        [DataMember(Name = "StdinOnce", EmitDefaultValue = false)]
        public bool StdinOnce { get; set; }

        /// <summary>
        ///     A list of environment variables to set inside the container in the form &#x60;[\&quot;VAR&#x3D;value\&quot;, ...]
        ///     &#x60;. A variable without &#x60;&#x3D;&#x60; is removed from the environment, rather than to have an empty value.
        /// </summary>
        /// <value>
        ///     A list of environment variables to set inside the container in the form &#x60;[\&quot;VAR&#x3D;value\&quot;,
        ///     ...]&#x60;. A variable without &#x60;&#x3D;&#x60; is removed from the environment, rather than to have an empty
        ///     value.
        /// </value>
        [DataMember(Name = "Env", EmitDefaultValue = false)]
        public List<string> Env { get; set; }

        /// <summary>
        ///     Command to run specified as a string or an array of strings.
        /// </summary>
        /// <value>Command to run specified as a string or an array of strings. </value>
        [DataMember(Name = "Cmd", EmitDefaultValue = false)]
        public List<string> Cmd { get; set; }

        /// <summary>
        ///     Gets or Sets Healthcheck
        /// </summary>
        [DataMember(Name = "Healthcheck", EmitDefaultValue = false)]
        public HealthConfig Healthcheck { get; set; }

        /// <summary>
        ///     Command is already escaped (Windows only)
        /// </summary>
        /// <value>Command is already escaped (Windows only)</value>
        [DataMember(Name = "ArgsEscaped", EmitDefaultValue = false)]
        public bool ArgsEscaped { get; set; }

        /// <summary>
        ///     The name of the image to use when creating the container/
        /// </summary>
        /// <value>The name of the image to use when creating the container/ </value>
        [DataMember(Name = "Image", EmitDefaultValue = false)]
        public string Image { get; set; }

        /// <summary>
        ///     An object mapping mount point paths inside the container to empty objects.
        /// </summary>
        /// <value>An object mapping mount point paths inside the container to empty objects. </value>
        [DataMember(Name = "Volumes", EmitDefaultValue = false)]
        public Dictionary<string, object> Volumes { get; set; }

        /// <summary>
        ///     The working directory for commands to run in.
        /// </summary>
        /// <value>The working directory for commands to run in.</value>
        [DataMember(Name = "WorkingDir", EmitDefaultValue = false)]
        public string WorkingDir { get; set; }

        /// <summary>
        ///     The entry point for the container as a string or an array of strings.  If the array consists of exactly one empty
        ///     string (&#x60;[\&quot;\&quot;]&#x60;) then the entry point is reset to system default (i.e., the entry point used
        ///     by docker when there is no &#x60;ENTRYPOINT&#x60; instruction in the &#x60;Dockerfile&#x60;).
        /// </summary>
        /// <value>
        ///     The entry point for the container as a string or an array of strings.  If the array consists of exactly one
        ///     empty string (&#x60;[\&quot;\&quot;]&#x60;) then the entry point is reset to system default (i.e., the entry point
        ///     used by docker when there is no &#x60;ENTRYPOINT&#x60; instruction in the &#x60;Dockerfile&#x60;).
        /// </value>
        [DataMember(Name = "Entrypoint", EmitDefaultValue = false)]
        public List<string> Entrypoint { get; set; }

        /// <summary>
        ///     Disable networking for the container.
        /// </summary>
        /// <value>Disable networking for the container.</value>
        [DataMember(Name = "NetworkDisabled", EmitDefaultValue = false)]
        public bool NetworkDisabled { get; set; }

        /// <summary>
        ///     MAC address of the container.
        /// </summary>
        /// <value>MAC address of the container.</value>
        [DataMember(Name = "MacAddress", EmitDefaultValue = false)]
        public string MacAddress { get; set; }

        /// <summary>
        ///     &#x60;ONBUILD&#x60; metadata that were defined in the image&#39;s &#x60;Dockerfile&#x60;.
        /// </summary>
        /// <value>&#x60;ONBUILD&#x60; metadata that were defined in the image&#39;s &#x60;Dockerfile&#x60;. </value>
        [DataMember(Name = "OnBuild", EmitDefaultValue = false)]
        public List<string> OnBuild { get; set; }

        /// <summary>
        ///     User-defined key/value metadata.
        /// </summary>
        /// <value>User-defined key/value metadata.</value>
        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public Dictionary<string, string> Labels { get; set; }

        /// <summary>
        ///     Signal to stop a container as a string or unsigned integer.
        /// </summary>
        /// <value>Signal to stop a container as a string or unsigned integer. </value>
        [DataMember(Name = "StopSignal", EmitDefaultValue = false)]
        public string StopSignal { get; set; }

        /// <summary>
        ///     Timeout to stop a container in seconds.
        /// </summary>
        /// <value>Timeout to stop a container in seconds.</value>
        [DataMember(Name = "StopTimeout", EmitDefaultValue = false)]
        public int StopTimeout { get; set; }

        /// <summary>
        ///     Shell for when &#x60;RUN&#x60;, &#x60;CMD&#x60;, and &#x60;ENTRYPOINT&#x60; uses a shell.
        /// </summary>
        /// <value>Shell for when &#x60;RUN&#x60;, &#x60;CMD&#x60;, and &#x60;ENTRYPOINT&#x60; uses a shell. </value>
        [DataMember(Name = "Shell", EmitDefaultValue = false)]
        public List<string> Shell { get; set; }
        
        public HostConfig HostConfig { get; set; }
        public NetworkingConfig NetworkingConfig { get; set; }

        /// <summary>
        ///     Returns true if ContainerConfig instances are equal
        /// </summary>
        /// <param name="input">Instance of ContainerConfig to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContainerConfig input)
        {
            if (input == null)
                return false;

            return
                (
                    Hostname == input.Hostname ||
                    Hostname != null &&
                    Hostname.Equals(input.Hostname)
                ) &&
                (
                    Domainname == input.Domainname ||
                    Domainname != null &&
                    Domainname.Equals(input.Domainname)
                ) &&
                (
                    User == input.User ||
                    User != null &&
                    User.Equals(input.User)
                ) &&
                (
                    AttachStdin == input.AttachStdin ||
                    AttachStdin != null &&
                    AttachStdin.Equals(input.AttachStdin)
                ) &&
                (
                    AttachStdout == input.AttachStdout ||
                    AttachStdout != null &&
                    AttachStdout.Equals(input.AttachStdout)
                ) &&
                (
                    AttachStderr == input.AttachStderr ||
                    AttachStderr != null &&
                    AttachStderr.Equals(input.AttachStderr)
                ) &&
                (
                    ExposedPorts == input.ExposedPorts ||
                    ExposedPorts != null &&
                    input.ExposedPorts != null &&
                    ExposedPorts.SequenceEqual(input.ExposedPorts)
                ) &&
                (
                    Tty == input.Tty ||
                    Tty != null &&
                    Tty.Equals(input.Tty)
                ) &&
                (
                    OpenStdin == input.OpenStdin ||
                    OpenStdin != null &&
                    OpenStdin.Equals(input.OpenStdin)
                ) &&
                (
                    StdinOnce == input.StdinOnce ||
                    StdinOnce != null &&
                    StdinOnce.Equals(input.StdinOnce)
                ) &&
                (
                    Env == input.Env ||
                    Env != null &&
                    input.Env != null &&
                    Env.SequenceEqual(input.Env)
                ) &&
                (
                    Cmd == input.Cmd ||
                    Cmd != null &&
                    input.Cmd != null &&
                    Cmd.SequenceEqual(input.Cmd)
                ) &&
                (
                    Healthcheck == input.Healthcheck ||
                    Healthcheck != null &&
                    Healthcheck.Equals(input.Healthcheck)
                ) &&
                (
                    ArgsEscaped == input.ArgsEscaped ||
                    ArgsEscaped != null &&
                    ArgsEscaped.Equals(input.ArgsEscaped)
                ) &&
                (
                    Image == input.Image ||
                    Image != null &&
                    Image.Equals(input.Image)
                ) &&
                (
                    Volumes == input.Volumes ||
                    Volumes != null &&
                    input.Volumes != null &&
                    Volumes.SequenceEqual(input.Volumes)
                ) &&
                (
                    WorkingDir == input.WorkingDir ||
                    WorkingDir != null &&
                    WorkingDir.Equals(input.WorkingDir)
                ) &&
                (
                    Entrypoint == input.Entrypoint ||
                    Entrypoint != null &&
                    input.Entrypoint != null &&
                    Entrypoint.SequenceEqual(input.Entrypoint)
                ) &&
                (
                    NetworkDisabled == input.NetworkDisabled ||
                    NetworkDisabled != null &&
                    NetworkDisabled.Equals(input.NetworkDisabled)
                ) &&
                (
                    MacAddress == input.MacAddress ||
                    MacAddress != null &&
                    MacAddress.Equals(input.MacAddress)
                ) &&
                (
                    OnBuild == input.OnBuild ||
                    OnBuild != null &&
                    input.OnBuild != null &&
                    OnBuild.SequenceEqual(input.OnBuild)
                ) &&
                (
                    Labels == input.Labels ||
                    Labels != null &&
                    input.Labels != null &&
                    Labels.SequenceEqual(input.Labels)
                ) &&
                (
                    StopSignal == input.StopSignal ||
                    StopSignal != null &&
                    StopSignal.Equals(input.StopSignal)
                ) &&
                (
                    StopTimeout == input.StopTimeout ||
                    StopTimeout != null &&
                    StopTimeout.Equals(input.StopTimeout)
                ) &&
                (
                    Shell == input.Shell ||
                    Shell != null &&
                    input.Shell != null &&
                    Shell.SequenceEqual(input.Shell)
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
            sb.Append("class ContainerConfig {\n");
            sb.Append("  Hostname: ").Append(Hostname).Append("\n");
            sb.Append("  Domainname: ").Append(Domainname).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  AttachStdin: ").Append(AttachStdin).Append("\n");
            sb.Append("  AttachStdout: ").Append(AttachStdout).Append("\n");
            sb.Append("  AttachStderr: ").Append(AttachStderr).Append("\n");
            sb.Append("  ExposedPorts: ").Append(ExposedPorts).Append("\n");
            sb.Append("  Tty: ").Append(Tty).Append("\n");
            sb.Append("  OpenStdin: ").Append(OpenStdin).Append("\n");
            sb.Append("  StdinOnce: ").Append(StdinOnce).Append("\n");
            sb.Append("  Env: ").Append(Env).Append("\n");
            sb.Append("  Cmd: ").Append(Cmd).Append("\n");
            sb.Append("  Healthcheck: ").Append(Healthcheck).Append("\n");
            sb.Append("  ArgsEscaped: ").Append(ArgsEscaped).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Volumes: ").Append(Volumes).Append("\n");
            sb.Append("  WorkingDir: ").Append(WorkingDir).Append("\n");
            sb.Append("  Entrypoint: ").Append(Entrypoint).Append("\n");
            sb.Append("  NetworkDisabled: ").Append(NetworkDisabled).Append("\n");
            sb.Append("  MacAddress: ").Append(MacAddress).Append("\n");
            sb.Append("  OnBuild: ").Append(OnBuild).Append("\n");
            sb.Append("  Labels: ").Append(Labels).Append("\n");
            sb.Append("  StopSignal: ").Append(StopSignal).Append("\n");
            sb.Append("  StopTimeout: ").Append(StopTimeout).Append("\n");
            sb.Append("  Shell: ").Append(Shell).Append("\n");
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
            return Equals(input as ContainerConfig);
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
                if (Hostname != null)
                    hashCode = hashCode * 59 + Hostname.GetHashCode();
                if (Domainname != null)
                    hashCode = hashCode * 59 + Domainname.GetHashCode();
                if (User != null)
                    hashCode = hashCode * 59 + User.GetHashCode();
                if (AttachStdin != null)
                    hashCode = hashCode * 59 + AttachStdin.GetHashCode();
                if (AttachStdout != null)
                    hashCode = hashCode * 59 + AttachStdout.GetHashCode();
                if (AttachStderr != null)
                    hashCode = hashCode * 59 + AttachStderr.GetHashCode();
                if (ExposedPorts != null)
                    hashCode = hashCode * 59 + ExposedPorts.GetHashCode();
                if (Tty != null)
                    hashCode = hashCode * 59 + Tty.GetHashCode();
                if (OpenStdin != null)
                    hashCode = hashCode * 59 + OpenStdin.GetHashCode();
                if (StdinOnce != null)
                    hashCode = hashCode * 59 + StdinOnce.GetHashCode();
                if (Env != null)
                    hashCode = hashCode * 59 + Env.GetHashCode();
                if (Cmd != null)
                    hashCode = hashCode * 59 + Cmd.GetHashCode();
                if (Healthcheck != null)
                    hashCode = hashCode * 59 + Healthcheck.GetHashCode();
                if (ArgsEscaped != null)
                    hashCode = hashCode * 59 + ArgsEscaped.GetHashCode();
                if (Image != null)
                    hashCode = hashCode * 59 + Image.GetHashCode();
                if (Volumes != null)
                    hashCode = hashCode * 59 + Volumes.GetHashCode();
                if (WorkingDir != null)
                    hashCode = hashCode * 59 + WorkingDir.GetHashCode();
                if (Entrypoint != null)
                    hashCode = hashCode * 59 + Entrypoint.GetHashCode();
                if (NetworkDisabled != null)
                    hashCode = hashCode * 59 + NetworkDisabled.GetHashCode();
                if (MacAddress != null)
                    hashCode = hashCode * 59 + MacAddress.GetHashCode();
                if (OnBuild != null)
                    hashCode = hashCode * 59 + OnBuild.GetHashCode();
                if (Labels != null)
                    hashCode = hashCode * 59 + Labels.GetHashCode();
                if (StopSignal != null)
                    hashCode = hashCode * 59 + StopSignal.GetHashCode();
                if (StopTimeout != null)
                    hashCode = hashCode * 59 + StopTimeout.GetHashCode();
                if (Shell != null)
                    hashCode = hashCode * 59 + Shell.GetHashCode();
                return hashCode;
            }
        }
    }
}