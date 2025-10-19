namespace ApiConsumerProduct.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiConsumerProduct.Domain;
using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Domain.Roles;
using ApiConsumerProduct.Domain.Users.Models;

public sealed class FakeUserForUpdateDto : AutoFaker<UserForUpdateDto>
{
    public FakeUserForUpdateDto()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}