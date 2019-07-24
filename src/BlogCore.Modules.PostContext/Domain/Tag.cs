using BlogCore.Shared.v1.Post;
using NetCoreKit.Domain;
using NetCoreKit.Utils.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Modules.PostContext.Domain
{
    public class Tag : AggregateRootWithIdBase<Guid>
    {
        private Tag()
        {
        }

        public static Tag CreateInstance(CreateTagRequest request)
        {
            return new Tag
            {
                Id = IdHelper.GenerateId(),
                Name = request.Name,
                Frequency = request.Frequency
            };
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        public int Frequency { get; private set; }

        public Tag IncreaseFrequency()
        {
            Frequency++;
            return this;
        }

        public Tag DecreaseFrequency()
        {
            Frequency--;
            return this;
        }
    }
}