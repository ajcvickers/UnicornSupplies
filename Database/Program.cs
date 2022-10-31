using UnicornSupplies;

await using var context = new UnicornsContext();

// await context.Database.EnsureDeletedAsync();
// await context.Database.EnsureCreatedAsync();
//
// var categories = TestData.CreateProducts().ToList();
// await context.AddRangeAsync(categories);
// await context.AddRangeAsync(TestData.CreateCustomers(categories));
//
// await context.SaveChangesAsync();
