namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using Newtonsoft.Json;
    using Simplets.Webapp.Tools.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    [JsonConverter(typeof (WithIdSerializer))]
    public class WithId<TEntityId, TEntity>
    {
        public WithId(TEntityId id, TEntity entity)
        {
            this.Id = id;
            this.Entity = entity;
        }

        /// <summary>
        /// The dto id.
        /// </summary>
        public TEntityId Id { get; private set; }

        /// <summary>
        /// The dto object.
        /// </summary>
        public TEntity Entity { get; private set; }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class WithIdSerializer : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var withId = value as dynamic;
            var toSerialize = new {Id = withId.Id}.ToDynamic();
            DynamicExtensions.Include(toSerialize, withId.Entity);
            serializer.Serialize(writer, toSerialize);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof (WithId<,>).IsAssignableFrom(objectType);
        }
    }
}