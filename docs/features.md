
# Using Threenine.Data

Threenine.Data supports a number of different repository patterns which enable retrieving, creating and editing data using  Entity Framework:

- ReadOnly Repository
- ReadOnlyAsync 
- Repository
- RepositoryAsync

## Tracking changes
Tracking behavior controls if Entity Framework Core will keep information about an entity instance in its change tracker. If an entity is tracked, 
any changes detected in the entity will be persisted to the database during `SaveChanges()`. EF Core will also fix up navigation properties between 
the entities in a tracking query result and the entities that are in the change tracker.

## No-tracking queries
No tracking queries are useful when the results are used in a read-only scenario. They're quicker to execute because there's no need to set up the change 
tracking information. If you don't need to update the entities retrieved from the database, then a no-tracking query should be used. You can swap an individual 
query to be no-tracking.

Threenine.Data provides a `ReadOnlyRepository` which is preconfigured to provide *No Tracking* queries.  

## Readonly Repository.

- `SingleOrDefault()`
- `GetList()`

#### SingleOrDefault

Use the `SingleOrDefault` method to get a single matching entity if one exists.
 
 `SingleOrDefault` returns the default value for the entity, returning a single matching element, or the default value if no element is found.

```c#
   var product = uow.GetRepository<TestProduct>().SingleOrDefault(x => x.Id == 1);
```
*Tracking is disabled by default* when making use of the `SingleOrDefault` select method. This is generally useful in Web API type projects when implement `GET` because the chances of requiring transaction to change data is minimal in such circumstances

If you would like ot use `SingleOrDefault` with tracking enabled then simply pass in the optional parameter `enableTracking: true`

```c#
   var product = uow.GetRepository<TestProduct>().SingleOrDefault(x => x.Id == 1, enableTracking: true);
```


 `SingleOrDefault` also enables the functionality to Order and Include before making the selection
 
```c#
var product = uow.GetRepository<TestProduct>().SingleOrDefault(orderBy: x => x.OrderBy(x => x.Name), include: x => x.Include(x => x.Category));
```

#### Projection 
The ability to only select the columns you need from the data is provided by the selector 

```c#
var projection = _unitOfWork.GetRepository<TesProduct>().SingleOrDefault(b => new { Name = b.Name }, predicate: x => x.Title.Contains(term));

```
 
#### GetList

Get list returns a **paginated list** of the items by default.
 
 The really useful aspect of the `GetList` is that it comes with a built in pagination functionality, which can be customised for your specific purposes but is instantiated with intuitive defaults.
 
 The default size settings for items returned by `GetList` method is supplied as 20.  However this setting can be overridden in number of ways.  
 
 If you are unsure of the number records you want retrieved and would like the repository to return as many as possible you can simply pass the `size: int.MaxValue` setting
 
```c#
var repo = uow.GetRepository<SomeEntity>().GetList(size: int.MaxValue).Items;
```

You can also provide a predicate containing the where clause you want to extract records on and supply a size counter for the number of records you want to appear on each page

```c#
  var items = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1 ).Items

// or 
var result = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1 );

var theItems = result.Items

```

### How to retrieve all values in one page
If in some circumstances you would not like to return a paginated list of items and would prefer to retrieve all items then just ensure you to provide and `int.MaxValue` to the `Size` parameter.
 
```c#
   var items = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1 ).Items
 // or 
var result = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1, size: int.MaxValue );

var theItems = result.Items

```

### How to include linked entities.

In order to include linked entities in your Paginated List you can simply make use of the `include` parameter. which is a

```c#
  var items = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1 ).Items

// or 
var result = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1, include: inc => inc.Include(x => x.Category);

var theItems = result.Items

```

### How to order lists

To add ordering to your lists the same can be applied
```c#
  var items = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1 ).Items

// or 
var result = uow.GetRepository<SomeEntity>().GetList(x => x.CategoryId == 1, orderBy: ord => ord.OrderBy(x => x.Name);

var theItems = result.Items

```


### Readonly Repository Async

The ReadOnlyAsync Repository has the same features as above only with the benefit of being Asynchronous!

