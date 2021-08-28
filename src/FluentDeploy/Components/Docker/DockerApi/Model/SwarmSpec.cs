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
    ///     User modifiable swarm configuration.
    /// </summary>
    [DataContract]
    public class SwarmSpec : IEquatable<SwarmSpec>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SwarmSpec" /> class.
        /// </summary>
        /// <param name="name">Name of the swarm..</param>
        /// <param name="labels">User-defined key/value metadata..</param>
        /// <param name="orchestration">orchestration.</param>
        /// <param name="raft">raft.</param>
        /// <param name="dispatcher">dispatcher.</param>
        /// <param name="cAConfig">cAConfig.</param>
        /// <param name="encryptionConfig">encryptionConfig.</param>
        /// <param name="taskDefaults">taskDefaults.</param>
        public SwarmSpec(string name = default, Dictionary<string, string> labels = default,
            SwarmSpecOrchestration orchestration = default, SwarmSpecRaft raft = default,
            SwarmSpecDispatcher dispatcher = default, SwarmSpecCAConfig cAConfig = default,
            SwarmSpecEncryptionConfig encryptionConfig = default, SwarmSpecTaskDefaults taskDefaults = default)
        {
            Orchestration = orchestration;
            Dispatcher = dispatcher;
            CAConfig = cAConfig;
            Name = name;
            Labels = labels;
            Orchestration = orchestration;
            Raft = raft;
            Dispatcher = dispatcher;
            CAConfig = cAConfig;
            EncryptionConfig = encryptionConfig;
            TaskDefaults = taskDefaults;
        }

        /// <summary>
        ///     Name of the swarm.
        /// </summary>
        /// <value>Name of the swarm.</value>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     User-defined key/value metadata.
        /// </summary>
        /// <value>User-defined key/value metadata.</value>
        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public Dictionary<string, string> Labels { get; set; }

        /// <summary>
        ///     Gets or Sets Orchestration
        /// </summary>
        [DataMember(Name = "Orchestration", EmitDefaultValue = true)]
        public SwarmSpecOrchestration Orchestration { get; set; }

        /// <summary>
        ///     Gets or Sets Raft
        /// </summary>
        [DataMember(Name = "Raft", EmitDefaultValue = false)]
        public SwarmSpecRaft Raft { get; set; }

        /// <summary>
        ///     Gets or Sets Dispatcher
        /// </summary>
        [DataMember(Name = "Dispatcher", EmitDefaultValue = true)]
        public SwarmSpecDispatcher Dispatcher { get; set; }

        /// <summary>
        ///     Gets or Sets CAConfig
        /// </summary>
        [DataMember(Name = "CAConfig", EmitDefaultValue = true)]
        public SwarmSpecCAConfig CAConfig { get; set; }

        /// <summary>
        ///     Gets or Sets EncryptionConfig
        /// </summary>
        [DataMember(Name = "EncryptionConfig", EmitDefaultValue = false)]
        public SwarmSpecEncryptionConfig EncryptionConfig { get; set; }

        /// <summary>
        ///     Gets or Sets TaskDefaults
        /// </summary>
        [DataMember(Name = "TaskDefaults", EmitDefaultValue = false)]
        public SwarmSpecTaskDefaults TaskDefaults { get; set; }

        /// <summary>
        ///     Returns true if SwarmSpec instances are equal
        /// </summary>
        /// <param name="input">Instance of SwarmSpec to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SwarmSpec input)
        {
            if (input == null)
                return false;

            return
                (
                    Name == input.Name ||
                    Name != null &&
                    Name.Equals(input.Name)
                ) &&
                (
                    Labels == input.Labels ||
                    Labels != null &&
                    input.Labels != null &&
                    Labels.SequenceEqual(input.Labels)
                ) &&
                (
                    Orchestration == input.Orchestration ||
                    Orchestration != null &&
                    Orchestration.Equals(input.Orchestration)
                ) &&
                (
                    Raft == input.Raft ||
                    Raft != null &&
                    Raft.Equals(input.Raft)
                ) &&
                (
                    Dispatcher == input.Dispatcher ||
                    Dispatcher != null &&
                    Dispatcher.Equals(input.Dispatcher)
                ) &&
                (
                    CAConfig == input.CAConfig ||
                    CAConfig != null &&
                    CAConfig.Equals(input.CAConfig)
                ) &&
                (
                    EncryptionConfig == input.EncryptionConfig ||
                    EncryptionConfig != null &&
                    EncryptionConfig.Equals(input.EncryptionConfig)
                ) &&
                (
                    TaskDefaults == input.TaskDefaults ||
                    TaskDefaults != null &&
                    TaskDefaults.Equals(input.TaskDefaults)
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
            sb.Append("class SwarmSpec {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Labels: ").Append(Labels).Append("\n");
            sb.Append("  Orchestration: ").Append(Orchestration).Append("\n");
            sb.Append("  Raft: ").Append(Raft).Append("\n");
            sb.Append("  Dispatcher: ").Append(Dispatcher).Append("\n");
            sb.Append("  CAConfig: ").Append(CAConfig).Append("\n");
            sb.Append("  EncryptionConfig: ").Append(EncryptionConfig).Append("\n");
            sb.Append("  TaskDefaults: ").Append(TaskDefaults).Append("\n");
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
            return Equals(input as SwarmSpec);
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
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Labels != null)
                    hashCode = hashCode * 59 + Labels.GetHashCode();
                if (Orchestration != null)
                    hashCode = hashCode * 59 + Orchestration.GetHashCode();
                if (Raft != null)
                    hashCode = hashCode * 59 + Raft.GetHashCode();
                if (Dispatcher != null)
                    hashCode = hashCode * 59 + Dispatcher.GetHashCode();
                if (CAConfig != null)
                    hashCode = hashCode * 59 + CAConfig.GetHashCode();
                if (EncryptionConfig != null)
                    hashCode = hashCode * 59 + EncryptionConfig.GetHashCode();
                if (TaskDefaults != null)
                    hashCode = hashCode * 59 + TaskDefaults.GetHashCode();
                return hashCode;
            }
        }
    }
}