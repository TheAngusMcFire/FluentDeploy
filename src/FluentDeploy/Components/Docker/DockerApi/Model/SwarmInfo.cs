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
    ///     Represents generic information about swarm.
    /// </summary>
    [DataContract]
    public class SwarmInfo : IEquatable<SwarmInfo>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SwarmInfo" /> class.
        /// </summary>
        /// <param name="nodeID">Unique identifier of for this node in the swarm. (default to &quot;&quot;).</param>
        /// <param name="nodeAddr">
        ///     IP address at which this node can be reached by other nodes in the swarm.  (default to &quot;
        ///     &quot;).
        /// </param>
        /// <param name="localNodeState">localNodeState.</param>
        /// <param name="controlAvailable">controlAvailable (default to false).</param>
        /// <param name="error">error (default to &quot;&quot;).</param>
        /// <param name="remoteManagers">List of ID&#39;s and addresses of other managers in the swarm. .</param>
        /// <param name="nodes">Total number of nodes in the swarm..</param>
        /// <param name="managers">Total number of managers in the swarm..</param>
        /// <param name="cluster">cluster.</param>
        public SwarmInfo(string nodeID = "", string nodeAddr = "", LocalNodeState? localNodeState = default,
            bool controlAvailable = false, string error = "", List<PeerNode> remoteManagers = default,
            int? nodes = default, int? managers = default, ClusterInfo cluster = default)
        {
            RemoteManagers = remoteManagers;
            Nodes = nodes;
            Managers = managers;
            Cluster = cluster;
            // use default value if no "nodeID" provided
            if (nodeID == null)
                NodeID = "";
            else
                NodeID = nodeID;
            // use default value if no "nodeAddr" provided
            if (nodeAddr == null)
                NodeAddr = "";
            else
                NodeAddr = nodeAddr;
            LocalNodeState = localNodeState;
            // use default value if no "controlAvailable" provided
            if (controlAvailable == null)
                ControlAvailable = false;
            else
                ControlAvailable = controlAvailable;
            // use default value if no "error" provided
            if (error == null)
                Error = "";
            else
                Error = error;
            RemoteManagers = remoteManagers;
            Nodes = nodes;
            Managers = managers;
            Cluster = cluster;
        }

        /// <summary>
        ///     Gets or Sets LocalNodeState
        /// </summary>
        [DataMember(Name = "LocalNodeState", EmitDefaultValue = false)]
        public LocalNodeState? LocalNodeState { get; set; }

        /// <summary>
        ///     Unique identifier of for this node in the swarm.
        /// </summary>
        /// <value>Unique identifier of for this node in the swarm.</value>
        [DataMember(Name = "NodeID", EmitDefaultValue = false)]
        public string NodeID { get; set; }

        /// <summary>
        ///     IP address at which this node can be reached by other nodes in the swarm.
        /// </summary>
        /// <value>IP address at which this node can be reached by other nodes in the swarm. </value>
        [DataMember(Name = "NodeAddr", EmitDefaultValue = false)]
        public string NodeAddr { get; set; }


        /// <summary>
        ///     Gets or Sets ControlAvailable
        /// </summary>
        [DataMember(Name = "ControlAvailable", EmitDefaultValue = false)]
        public bool ControlAvailable { get; set; }

        /// <summary>
        ///     Gets or Sets Error
        /// </summary>
        [DataMember(Name = "Error", EmitDefaultValue = false)]
        public string Error { get; set; }

        /// <summary>
        ///     List of ID&#39;s and addresses of other managers in the swarm.
        /// </summary>
        /// <value>List of ID&#39;s and addresses of other managers in the swarm. </value>
        [DataMember(Name = "RemoteManagers", EmitDefaultValue = true)]
        public List<PeerNode> RemoteManagers { get; set; }

        /// <summary>
        ///     Total number of nodes in the swarm.
        /// </summary>
        /// <value>Total number of nodes in the swarm.</value>
        [DataMember(Name = "Nodes", EmitDefaultValue = true)]
        public int? Nodes { get; set; }

        /// <summary>
        ///     Total number of managers in the swarm.
        /// </summary>
        /// <value>Total number of managers in the swarm.</value>
        [DataMember(Name = "Managers", EmitDefaultValue = true)]
        public int? Managers { get; set; }

        /// <summary>
        ///     Gets or Sets Cluster
        /// </summary>
        [DataMember(Name = "Cluster", EmitDefaultValue = true)]
        public ClusterInfo Cluster { get; set; }

        /// <summary>
        ///     Returns true if SwarmInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of SwarmInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SwarmInfo input)
        {
            if (input == null)
                return false;

            return
                (
                    NodeID == input.NodeID ||
                    NodeID != null &&
                    NodeID.Equals(input.NodeID)
                ) &&
                (
                    NodeAddr == input.NodeAddr ||
                    NodeAddr != null &&
                    NodeAddr.Equals(input.NodeAddr)
                ) &&
                (
                    LocalNodeState == input.LocalNodeState ||
                    LocalNodeState != null &&
                    LocalNodeState.Equals(input.LocalNodeState)
                ) &&
                (
                    ControlAvailable == input.ControlAvailable ||
                    ControlAvailable != null &&
                    ControlAvailable.Equals(input.ControlAvailable)
                ) &&
                (
                    Error == input.Error ||
                    Error != null &&
                    Error.Equals(input.Error)
                ) &&
                (
                    RemoteManagers == input.RemoteManagers ||
                    RemoteManagers != null &&
                    input.RemoteManagers != null &&
                    RemoteManagers.SequenceEqual(input.RemoteManagers)
                ) &&
                (
                    Nodes == input.Nodes ||
                    Nodes != null &&
                    Nodes.Equals(input.Nodes)
                ) &&
                (
                    Managers == input.Managers ||
                    Managers != null &&
                    Managers.Equals(input.Managers)
                ) &&
                (
                    Cluster == input.Cluster ||
                    Cluster != null &&
                    Cluster.Equals(input.Cluster)
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
            sb.Append("class SwarmInfo {\n");
            sb.Append("  NodeID: ").Append(NodeID).Append("\n");
            sb.Append("  NodeAddr: ").Append(NodeAddr).Append("\n");
            sb.Append("  LocalNodeState: ").Append(LocalNodeState).Append("\n");
            sb.Append("  ControlAvailable: ").Append(ControlAvailable).Append("\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("  RemoteManagers: ").Append(RemoteManagers).Append("\n");
            sb.Append("  Nodes: ").Append(Nodes).Append("\n");
            sb.Append("  Managers: ").Append(Managers).Append("\n");
            sb.Append("  Cluster: ").Append(Cluster).Append("\n");
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
            return Equals(input as SwarmInfo);
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
                if (NodeID != null)
                    hashCode = hashCode * 59 + NodeID.GetHashCode();
                if (NodeAddr != null)
                    hashCode = hashCode * 59 + NodeAddr.GetHashCode();
                if (LocalNodeState != null)
                    hashCode = hashCode * 59 + LocalNodeState.GetHashCode();
                if (ControlAvailable != null)
                    hashCode = hashCode * 59 + ControlAvailable.GetHashCode();
                if (Error != null)
                    hashCode = hashCode * 59 + Error.GetHashCode();
                if (RemoteManagers != null)
                    hashCode = hashCode * 59 + RemoteManagers.GetHashCode();
                if (Nodes != null)
                    hashCode = hashCode * 59 + Nodes.GetHashCode();
                if (Managers != null)
                    hashCode = hashCode * 59 + Managers.GetHashCode();
                if (Cluster != null)
                    hashCode = hashCode * 59 + Cluster.GetHashCode();
                return hashCode;
            }
        }
    }
}