﻿using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace IWantApp.Domain.Product;
public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; }

    public Category(string name )
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name");
        AddNotifications(contract);
        Name = name;
        Active = true;
    }
}
