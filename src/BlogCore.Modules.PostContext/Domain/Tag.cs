using NetCoreKit.Domain;
using NetCoreKit.Utils.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Modules.PostContext.Domain
{
    public class Tag : EntityBase
    {
        private Tag()
        {
        }

        public Tag(string name, int frequency)
            : this(IdHelper.GenerateId(), name, frequency)
        {
        }

        public Tag(Guid postId, string name, int frequency) 
            : base(postId)
        {
            Name = name;
            Frequency = frequency;
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