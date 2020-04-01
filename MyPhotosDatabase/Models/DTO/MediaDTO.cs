using MyPhotosDatabase.Enums;
using System;
using System.Runtime.Serialization;

namespace MyPhotosDatabase.Models.DTO
{
    [DataContract(IsReference = true)]
    public class MediaDTO
    {
        private MediaDTO() { }
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public MediaType Type { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string Event { get; set; }
        [DataMember]
        public string People { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        public static MediaDTO Create(string name, string path, string format, MediaType type, DateTime createdDate, string _event = "", string people = "", string description = "", string tags = "", string location = "")
        {
            return new MediaDTO()
            {
                Name = name,
                Path = path,
                Format = format,
                Type = type,
                CreatedDate = createdDate,
                Event = _event,
                People = people,
                Description = description,
                Tags = tags,
                Location = location,
                Deleted = false
            };
        }

    }
}
