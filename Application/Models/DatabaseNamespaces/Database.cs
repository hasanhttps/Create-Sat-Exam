using System;
using System.Linq;
using Database.DbContexts;
using System.Collections.Generic;
using Database.Entities.Concretes;

namespace Application.Models.DatabaseNamespace;

public static class Database {

    // Properties

    public static SatExaminationDbContext DbContext { get; set; }

    // Constructor

    static Database() {
        
    }
}