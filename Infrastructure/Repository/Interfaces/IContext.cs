﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IDBContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

    }
}
