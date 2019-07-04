using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.ValidationModel;
using BlogCore.Shared.v1.Validators;
using Google.Protobuf;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BlogCore.Hosts.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Can_Instance_And_Parse_Message()
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

        [Fact]
        public async Task Can_Validate_Model_With_Errors()
        {
            var message = new GetMyBlogsRequest();
            var validator = new GetMyBlogsRequestValidator();
            var result = await validator.ValidateAsync(message);
            Assert.False(result.IsValid);

            var validationModel = result.ToValidationResultModel();
            Assert.True(validationModel.Errors.Count == 2);
        }

        [Fact]
        public async Task Can_Validate_Without_Error()
        {
            var message = new GetMyBlogsRequest
            {
                Page = 1,
                Username = "test"
            };

            var validator = new GetMyBlogsRequestValidator();
            var result = await validator.ValidateAsync(message);
            Assert.True(result.IsValid);

            var validationModel = result.ToValidationResultModel();
            Assert.True(validationModel.Errors.Count <= 0);
        }
    }
}
