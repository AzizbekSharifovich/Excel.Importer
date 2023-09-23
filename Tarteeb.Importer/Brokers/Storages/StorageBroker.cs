//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.importer.Brockers.Storages;

public class StorageBroker : EFxceptionsContext
{
    public DbSet<Client> Clients { get; set; }
    public StorageBroker() =>
        this.Database.EnsureCreated();

    public async Task<Client> InsertClientAsync(Client client)
    {
        await this.Clients.AddAsync(client);
        await this.SaveChangesAsync();

        return client;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source = ..\\..\\..\\ Tarteeb.db";
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseSqlite(connectionString);
    }

    public async Task<Client> SelectClientByIdAsync(Guid id) =>
         await this.Clients.FindAsync(id);

    public IQueryable<Client> SelectAllClients() =>
         this.Clients.AsQueryable();

}