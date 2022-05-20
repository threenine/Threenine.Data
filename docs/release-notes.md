# Release Notes

## Maintenance team

The current members of the maintenance team 

 * [@garywoodfine](https://github.com/garywoodfine)

### Version 3.2.0 (20 May 2022)
#### Feature upgrades
* After a long period of no updates two in as many days!
* Implemented override methods on `GetList` and `GetListAsync` for the same reasons as `SingleOrDefault` and `SingleOrDefaultAsync` below
* Added more Unit test coverage to ensure things work as expected
* Updated the Documentation again! Hoepfully this will continue to improve and feedback and questions welcome!


### Version 3.1.0 (19 May 2022)
#### Feature upgrades
  * Implemented override methods on `SingleOrDefault` and `SingleOrDefaultAsync` to enable easier Mocking in unit tests. The flexibility of the optional parameters on these methods enabled great flexibility during development, however they added unintentional overhead when writing unit tests especially if using [Moq](https://github.com/moq/moq4 "Moq - The most popular and friendly mocking library for .NET") because all optional parameters need to be defined with `default` in order for Mock values to be returned  
  * Fixed bug in Pagination whereby values were not being set correctly!
  * Updated documentation after a long period of no updates!
  * We moved our CI/CD process to make Github Actions 

