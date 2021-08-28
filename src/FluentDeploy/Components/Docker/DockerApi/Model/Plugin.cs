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
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     A plugin for the Engine API
    /// </summary>
    [DataContract]
    public class Plugin : IEquatable<Plugin>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Plugin" /> class.
        /// </summary>
        [JsonConstructor]
        protected Plugin()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Plugin" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="name">name (required).</param>
        /// <param name="enabled">True if the plugin is running. False if the plugin is not running, only installed. (required).</param>
        /// <param name="settings">settings (required).</param>
        /// <param name="pluginReference">plugin remote reference used to push/pull the plugin.</param>
        /// <param name="config">config (required).</param>
        public Plugin(string id = default, string name = default, bool enabled = default,
            PluginSettings settings = default, string pluginReference = default, PluginConfig config = default)
        {
            // to ensure "name" is required (not null)
            if (name == null)
                throw new InvalidDataException("name is a required property for Plugin and cannot be null");
            Name = name;

            // to ensure "enabled" is required (not null)
            if (enabled == null)
                throw new InvalidDataException("enabled is a required property for Plugin and cannot be null");
            Enabled = enabled;

            // to ensure "settings" is required (not null)
            if (settings == null)
                throw new InvalidDataException("settings is a required property for Plugin and cannot be null");
            Settings = settings;

            // to ensure "config" is required (not null)
            if (config == null)
                throw new InvalidDataException("config is a required property for Plugin and cannot be null");
            Config = config;

            Id = id;
            PluginReference = pluginReference;
        }

        /// <summary>
        ///     Gets or Sets Id
        /// </summary>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or Sets Name
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        ///     True if the plugin is running. False if the plugin is not running, only installed.
        /// </summary>
        /// <value>True if the plugin is running. False if the plugin is not running, only installed.</value>
        [DataMember(Name = "Enabled", EmitDefaultValue = true)]
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets or Sets Settings
        /// </summary>
        [DataMember(Name = "Settings", EmitDefaultValue = true)]
        public PluginSettings Settings { get; set; }

        /// <summary>
        ///     plugin remote reference used to push/pull the plugin
        /// </summary>
        /// <value>plugin remote reference used to push/pull the plugin</value>
        [DataMember(Name = "PluginReference", EmitDefaultValue = false)]
        public string PluginReference { get; set; }

        /// <summary>
        ///     Gets or Sets Config
        /// </summary>
        [DataMember(Name = "Config", EmitDefaultValue = true)]
        public PluginConfig Config { get; set; }

        /// <summary>
        ///     Returns true if Plugin instances are equal
        /// </summary>
        /// <param name="input">Instance of Plugin to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Plugin input)
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
                    Name == input.Name ||
                    Name != null &&
                    Name.Equals(input.Name)
                ) &&
                (
                    Enabled == input.Enabled ||
                    Enabled != null &&
                    Enabled.Equals(input.Enabled)
                ) &&
                (
                    Settings == input.Settings ||
                    Settings != null &&
                    Settings.Equals(input.Settings)
                ) &&
                (
                    PluginReference == input.PluginReference ||
                    PluginReference != null &&
                    PluginReference.Equals(input.PluginReference)
                ) &&
                (
                    Config == input.Config ||
                    Config != null &&
                    Config.Equals(input.Config)
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
            sb.Append("class Plugin {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Enabled: ").Append(Enabled).Append("\n");
            sb.Append("  Settings: ").Append(Settings).Append("\n");
            sb.Append("  PluginReference: ").Append(PluginReference).Append("\n");
            sb.Append("  Config: ").Append(Config).Append("\n");
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
            return Equals(input as Plugin);
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
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Enabled != null)
                    hashCode = hashCode * 59 + Enabled.GetHashCode();
                if (Settings != null)
                    hashCode = hashCode * 59 + Settings.GetHashCode();
                if (PluginReference != null)
                    hashCode = hashCode * 59 + PluginReference.GetHashCode();
                if (Config != null)
                    hashCode = hashCode * 59 + Config.GetHashCode();
                return hashCode;
            }
        }
    }
}