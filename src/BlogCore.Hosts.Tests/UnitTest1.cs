using BlogCore.Shared.v1.Blog;
using Google.Protobuf;
using System;
using Xunit;

namespace BlogCore.Hosts.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var msg = new RetrieveBlogResponse();
            msg.Blog = new BlogDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = "My blog",
                Description = "This is my blog",
                Image = "/images/my-blog.png",
                Theme = 1
            };
            var parser = new MessageParser<RetrieveBlogResponse>(() => new RetrieveBlogResponse());
            var abc = parser.ParseFrom(msg.ToByteString().ToByteArray());
            Assert.NotNull(abc);
        }
    }
}
