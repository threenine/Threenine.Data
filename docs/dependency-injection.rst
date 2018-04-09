Dependency Injection
====================

What is Dependency Injection
****************************
Dependency injection is basically providing the objects that an object needs (its dependencies) instead of having it construct them itself.  It's a very useful technique for testing, since it allows dependencies to be mocked or stubbed out.

Dependencies can be injected into objects by many means (such as constructor injection or setter injection).  One can even use specialized dependency injection frameworks (e.g. SimpleInjector, Autofac, StructureMap) to do that, but they certainly aren't required. You don't need those frameworks to have dependency injection, the .net core framework comes with a built in DI container.
We will use this DI container to illustrate how to configure and inject Threenine.Data.



