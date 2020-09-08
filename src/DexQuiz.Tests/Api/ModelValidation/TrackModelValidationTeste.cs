using AutoFixture;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Validations;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DexQuiz.Tests.Api.ModelValidation
{
    public sealed class TrackModelValidationTeste
    {
        private readonly TrackModelValidation _validator;
        private readonly Fixture _fixture;

        public TrackModelValidationTeste()
        {
            _fixture = new Fixture();
            _validator = new TrackModelValidation();
        }

        public void ShouldNotHaveErrors()
        {
            var trackModel = _fixture.Create<TrackModel>();

            var result = _validator.TestValidate(trackModel);

            result.ShouldNotHaveValidationErrorFor(x => x.Available);
            result.ShouldNotHaveValidationErrorFor(x => x.Awards);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.ImageUrl);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Should_Validate_NotEmptyValidator()
        {
            var model = new TrackModel();

            _validator.ShouldNotHaveValidationErrorFor(x => x.Available, model);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Awards, model);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, model);
            _validator.ShouldHaveValidationErrorFor(x => x.ImageUrl, model).WithErrorCode("NotEmptyValidator");
            _validator.ShouldHaveValidationErrorFor(x => x.Name, model).WithErrorCode("NotEmptyValidator");
        }

        [Test]
        public void Should_Validate_Caracters_Count_Validator()
        {
            var model = _fixture
                .Build<TrackModel>()
                .With(x => x.ImageUrl, "12")
                .With(x => x.Name, "1")
                .Create();

            _validator.ShouldNotHaveValidationErrorFor(x => x.Available, model);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Awards, model);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, model);
            _validator.ShouldHaveValidationErrorFor(x => x.ImageUrl, model).WithErrorCode("MinimumLengthValidator");
            _validator.ShouldHaveValidationErrorFor(x => x.Name, model).WithErrorCode("MinimumLengthValidator");
        }
    }
}
