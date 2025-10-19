namespace ApiConsumerProduct.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using ApiConsumerProduct.Domain;
using ApiConsumerProduct.Domain.RolePermissions.Dtos;
using ApiConsumerProduct.Domain.Roles;
using ApiConsumerProduct.Domain.RolePermissions.Models;

public sealed class FakeRolePermissionForUpdateDto : AutoFaker<RolePermissionForUpdateDto>
{
    public FakeRolePermissionForUpdateDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Role.ListNames()));
    }
}