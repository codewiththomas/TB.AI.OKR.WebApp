// using ML to label the existing data

using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Contexts;

using var applicationDbContext = new ApplicationDbContext();

var rules = await applicationDbContext.OkrRules.ToListAsync();

//label okr sets

