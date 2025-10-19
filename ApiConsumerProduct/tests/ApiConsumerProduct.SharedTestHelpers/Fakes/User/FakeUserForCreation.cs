namespace ApiConsumerProduct.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiConsumerProduct.Domain;
using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Domain.Roles;
using ApiConsumerProduct.Domain.Users.Models;

public sealed class FakeUserForCreation : AutoFaker<UserForCreation>
{
    public FakeUserForCreation()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}